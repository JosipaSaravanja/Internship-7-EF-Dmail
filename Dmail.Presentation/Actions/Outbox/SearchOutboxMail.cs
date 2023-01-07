using Dmail.Data.Entitets.Models;
using Dmail.Domain.Repositories;
using Dmail.Presentation.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Presentation.Actions.Outbox
{
    public class SearchOutboxMail : IAction
    {
        public int Index { get; set; }
        public string Name { get; set; } = "Pretraga po imenu";

        private readonly MailRepository _mailRepository;
        private readonly UserRepository _userRepository;
        private readonly User _authenticatedUser;

        public SearchOutboxMail(
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
            Console.WriteLine("Unesite ime/dio imena");
            string query = Console.ReadLine();

            if (string.IsNullOrEmpty(query))
            {
                Console.WriteLine("Ne točan unos");
                Console.ReadLine();
                return;
            }

            ICollection<User> recievers = _userRepository.GetEmailContains(query);

            List<Mail> mailsWhereSender = new List<Mail>();

            foreach (var u in recievers)
            {
                mailsWhereSender.AddRange(_mailRepository.GetWhereSenderAndRecipient(_authenticatedUser.Id,
                    u.Id));
            };

            List<Mail> recieved = mailsWhereSender
                .Where(m => m.IsHidden == false)
                .OrderByDescending(m => m.TimeOfCreation)
                .ToList();

            IList<Mail> final = Extand.FilterByFormat(recieved);

            if (!final.Any())
            {
                Console.WriteLine("Nismo pronašli niti jedan mail");
                Console.ReadLine();
                return;
            }

            Console.Clear();
            Extand.WriteListOfMails(final);
            Extand.SelectMailByIndexFromOutbox(final, _authenticatedUser);

            RegistredMenuFactory.CreateActions().PrintActionsAndOpen();
        }
    }
}
