using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dmail.Data.Entitets.Models;
using Dmail.Domain.Enums;
using Dmail.Domain.Repositories;
using Dmail.Presentation.Actions;
using Dmail.Presentation.Factories;
using Dmail.Domain.Repositories;

namespace Dmail.Presentation.Actions.Main
{
    public class LogInAction : IAction
    {
        private static User? _currentlyAuthenticatedUser = null;
        public static void Logout() => _currentlyAuthenticatedUser = null;
        public static User? GetCurrentlyAuthenticatedUser()
        {
            if (_currentlyAuthenticatedUser == null)
                return null;
            return new User
            {
                Id = _currentlyAuthenticatedUser.Id,
                Email = _currentlyAuthenticatedUser.Email,
            };
        }
        private static void SetAuthenticatedUser(User user)
        {
            _currentlyAuthenticatedUser = user;
        }

        private readonly UserRepository _userRepository;

        public LogInAction(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public int Index { get; set; }
        public string Name { get; set; } = "Log in";

        public void Open()
        {
            Console.Clear();
            Console.WriteLine("Email:");

            string email = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Morate unjeti stvarne podatke");
                return;
            }

            Console.WriteLine("Lozinka:");
            string password = Console.ReadLine();

            ResponseResultType response = _userRepository.LogIn(email, password);

            switch (response)
            {
                case ResponseResultType.ErrorViolatesRequirements:
                    Console.WriteLine("Niste pričekali 30s od zadnjeg pokušaja prijave. Molimo pričekajte.");
                    Console.ReadLine();
                    return;
                case ResponseResultType.ErrorInvalidPassword:
                    Console.WriteLine("Ne točna lozinka. Morat ćete pričekat 30s prije sljedećeg pokušaja prijave.");
                    Console.ReadLine();
                    return;
                case ResponseResultType.ErrorNotFound:
                    Console.WriteLine("Račun nije pronađen");
                    Console.ReadLine();
                    return;
                default:
                    if (response != ResponseResultType.Succeeded)
                    {
                        Console.WriteLine("Došlo je do greške");
                        Console.ReadLine();
                        return;
                    }
                    break;
            }

            Console.WriteLine($"Authenticated as {email.ToLower()}.");
            SetAuthenticatedUser(_userRepository.GetByEmail(email)!);
            Console.ReadLine();

            Console.Clear();
            RegistredMenuFactory
                .CreateActions()
                .PrintActionsAndOpen();
        }
    }
}
