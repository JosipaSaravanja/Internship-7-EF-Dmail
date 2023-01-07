using Dmail.Data.Entitets.Models;
using Dmail.Domain.Repositories;
using Dmail.Presentation.Actions;
using Dmail.Data.Enums;
using Dmail.Data.Entitets.Models;
using Dmail.Domain.Enums;
using Dmail.Domain.Repositories;
using System;

namespace Internship_7_EF_Dmail.Presentation.Actions.AuthenticatedUserActions
{
    public class SendNewEmailAction : IAction
    {
        private readonly MailRepository _mailRepository;
        private readonly UserRepository _userRepository;
        private readonly User _authenticatedUser;

        public int Index { get; set; }
        public string Name { get; set; } = "Novi Mail";
        public SendNewEmailAction
(
            MailRepository mailRepository,
            UserRepository userRepository,
            User authenticatedUser)
        {
            _mailRepository = mailRepository;
            _userRepository = userRepository;
            _authenticatedUser = authenticatedUser;
        }


        public void Open()
        {
            Console.Clear();

            Mail newMail = new Mail()
            {
                SenderId = _authenticatedUser.Id,
                Format = Format.Email,
            };

            IList<string>? userInput = Extand.ReadRecipients();

            if (userInput == null)
            {
                Console.WriteLine("Niste unjeli niti jednog primatelja");
                Console.ReadLine();
                return;
            }

            if (userInput.Contains(_authenticatedUser.Email))
            {
                Console.WriteLine("Ne možete sebe navesti kao pošiljatelja");
                userInput.Remove(_authenticatedUser.Email);
            }

            List<User> recipientsUsers = new List<User>();

            foreach(var ui in userInput)
            {
                User toAdd = _userRepository.GetByEmail(ui);

                if (toAdd != null && !recipientsUsers.Contains(toAdd))
                    recipientsUsers.Add(toAdd);
            }

            if (!recipientsUsers.Any())
            {
                Console.WriteLine("Svi navedeni mailovi su nevrijedeći");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("Unesite naslov: ");
            string title = Console.ReadLine();
            newMail.Title = title;

            Console.WriteLine("Unesite sadržaj: ");
            string con = Console.ReadLine();
            newMail.Contents = con;

            recipientsUsers.ForEach((u) =>
            {
                var rec = new Recipient();
                rec.UserId = u.Id;
                newMail.Recipients.Add(rec);
            });

        ResponseResultType response = _mailRepository.Add(newMail);
            Console.WriteLine(response);
        }
    }
}