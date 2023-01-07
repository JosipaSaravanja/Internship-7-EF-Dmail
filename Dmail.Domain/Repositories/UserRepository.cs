using Dmail.Data.Context;
using Dmail.Data.Entitets.Models;
using Dmail.Domain.Repositories;
using Dmail.Data.Context;
using Dmail.Data.Entitets.Models;
using Dmail.Domain.Enums;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace Dmail.Domain.Repositories
{
    public class UserRepository : BaseRepository
    {
        public UserRepository(DmailDBContext context) : base(context)
        {
        }
        public User? GetById(int id) => context.Users.Find(id);
        public User? GetByEmail(string email) => GetAll().FirstOrDefault(u => u.Email == email);

        public bool EmailExists(string email) => GetAll().Any(u => u.Email == email);

        public ResponseResultType ValidateEmail(string email)
        {
            if (!Regex.IsMatch(email, "^([a-z A-Z 0-9 .]{1,})+@([a-z A-Z 0-9]{3,})+.+[a-z A-z]{2,}$")) //provjerava format emaila
                return ResponseResultType.ErrorInvalidFormat;
            return ResponseResultType.Succeeded;
        }

        public ICollection<User> GetAll() => context.Users.ToList();

        public ICollection<User> GetFlaggedUsers(int userId) => context.Spammers
                .Where(sf => sf.UserId == userId)
                .Join(context.Users,
                sf => sf.SpammerId,
                u => u.Id,
                (sf, u) => new { sf, u })
                .Select(a => a.u)
                .ToList();

        public ResponseResultType Add(User user)
        {
            if (ValidateEmail(user.Email) != ResponseResultType.Succeeded)
                return ResponseResultType.ErrorViolatesRequirements;

            if (EmailExists(user.Email))
                return ResponseResultType.ErrorViolatesUniqueConstraint;

            user.Email = user.Email.ToLower();

            context.Users.Add(user);
            return SaveChanges();
        }


        public ResponseResultType UpdateEmail(int userId, string email)
        {
            User? toUpdate = GetById(userId);

            if (toUpdate == null)
                return ResponseResultType.ErrorNotFound;
            if (ValidateEmail(email) != ResponseResultType.Succeeded)
                return ResponseResultType.ErrorViolatesRequirements;

            if (EmailExists(email))
                return ResponseResultType.ErrorViolatesUniqueConstraint;

            toUpdate.Email = email.ToLower();

            return SaveChanges();
        }

        public ResponseResultType UpdatePassword(int userId, string password)
        {
            User? toUpdate = GetById(userId);

            if (toUpdate == null)
                return ResponseResultType.ErrorNotFound;

            toUpdate.Password =password;

            return SaveChanges();
        }

        public ResponseResultType Delete(int userId)
        {
            User? toDelete = GetById(userId);

            if (toDelete == null)
                return ResponseResultType.ErrorNotFound;

            context.Users.Remove(toDelete);
            return SaveChanges();
        }

        public ResponseResultType LogIn(string email, string password)
        {
            User? toAuth = context.Users.FirstOrDefault(u => u.Email == email);

            if (toAuth == null)
                return ResponseResultType.ErrorNotFound;

            if ((DateTime.UtcNow - toAuth.LastFailedLogin) < TimeSpan.FromSeconds(30))
                return ResponseResultType.ErrorViolatesRequirements;

            if (password!=toAuth.Password)
            {
                toAuth.LastFailedLogin = DateTime.UtcNow;
                context.SaveChanges();
                return ResponseResultType.ErrorInvalidPassword;
            }
            return ResponseResultType.Succeeded;

        }

        public ICollection<User> GetEmailContains(string query) => GetAll()
            .Where(u => u.Email.Contains(query))
            .ToList();
    }
}