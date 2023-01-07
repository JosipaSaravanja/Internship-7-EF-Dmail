using Dmail.Data.Context;
using Dmail.Domain.Repositories;
using Dmail.Data.Context;
using Dmail.Data.Entitets.Models;
using Dmail.Domain.Enums;

namespace Dmail.Domain.Repositories
{
    public class SpammersRepository : BaseRepository
    {
        public SpammersRepository(DmailDBContext context) : base(context)
        {
        }
        public ICollection<Spammers> GetAll() => context.Spammers.ToList();

        public bool SpamFlagExists(int userId, int userToFlag) //promjeni ime
        {
            return GetAll().Any(sf => sf.UserId == userId && sf.SpammerId == userToFlag);
        }

        public ResponseResultType MarkAsSpam(int userId, int SpammerId)
        {

            if (SpamFlagExists(userId, SpammerId))
            {
                return ResponseResultType.NoChanges;
            }

            context.Spammers.Add(new Spammers()
            {
                UserId = userId,
                SpammerId = SpammerId,
            });

            return base.SaveChanges();
        }

        public ResponseResultType RemoveAsSpam(int userId, int userToUnflag)
        {
            context.Spammers.Remove(new Spammers()
            {
                UserId = userId,
                SpammerId = userToUnflag,
            });

            return base.SaveChanges();
        }

        public ICollection<Spammers> GetSpamFlagsForUser(int userId) => GetAll()
            .Where(sf => sf.UserId == userId)
            .ToList();

    }
}