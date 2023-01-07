using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dmail.Data.Entitets.Models;
using Dmail.Domain.Enums;
using Dmail.Domain.Repositories;

namespace Dmail.Presentation.Actions.Main
{
    public class RegisterAction : IAction
    {
        private readonly UserRepository _userRepository;

        public RegisterAction(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public int Index { get; set; }
        public string Name { get; set; } = "Registracija";

        public void Open()
        {
            Console.Clear();
            Console.WriteLine("Email: ");

            string email = Console.ReadLine();

            if (_userRepository.EmailExists(email))
            {
                Console.WriteLine("Email je već upotrijebljen");
                Console.ReadLine();
                return;
            }

            ResponseResultType emailValidationResponse = _userRepository.ValidateEmail(email);

            if (emailValidationResponse == ResponseResultType.ErrorInvalidFormat)
            {
                Console.WriteLine("Email je u krivom obliku");
                Console.ReadLine();
                return;
            }
            if (emailValidationResponse != ResponseResultType.Succeeded)
            {
                Console.WriteLine("Došlo je do greške");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("Lozinka: ");
            string password = Console.ReadLine();

            if (string.IsNullOrEmpty(password))
            {
                Console.WriteLine("Morate imati lozinku radi sigurnosti.");
                Console.ReadLine();
                return;

            }
            Console.WriteLine("Ponovite lozinku: ");

            string confirmPassword = Console.ReadLine();

            if (password != confirmPassword)
            {
                Console.WriteLine("Lozinke se ne podudaraju");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Prepišite sljedeću riječ:");
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            Console.WriteLine(finalString);

            if (finalString!=Console.ReadLine())
             {
                 Console.WriteLine("Niste prošli bot provjeru");
                 Console.ReadLine();
                 return;
             }

            Console.Clear();

            User user = new User()
            {
                Email = email,
                Password = password,
            };

            _userRepository.Add(user);

            Console.WriteLine("Uspješno ste se prijevili");
            Console.ReadLine();
        }
    }
}
