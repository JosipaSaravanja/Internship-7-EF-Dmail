using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dmail.Data.Entitets.Models;
using Dmail.Domain.Repositories;
using Dmail.Presentation.Factories;
using Dmail.Presentation.Actions;
using Dmail.Domain.Repositories;

namespace Dmail.Presentation.Actions.Inbox
{
    public class SearchSpamMail : IAction
    {

        public int Index { get; set; }
        public string Name { get; set; } = "Pretraga spama po imenu";

        private readonly MailRepository _mailRepository;
        private readonly SpammersRepository _spammersRepository;
        private readonly UserRepository _userRepository;
        private readonly User _authenticatedUser;

        public SearchSpamMail(
            MailRepository mailRepository,
            SpammersRepository spammerRepository,
            UserRepository userRepository,
            User authenticatedUser)
        {
            _mailRepository = mailRepository;
            _spammersRepository = spammerRepository;
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

            IList<Spammers>? spammers = _spammersRepository
                .GetSpamFlagsForUser(_authenticatedUser.Id)
                .ToList();

            ICollection<User> senders = _userRepository.GetEmailContains(query);

            List<Mail> mailsWhereSender = new List<Mail>();

            foreach (var u in senders)
            {
                mailsWhereSender.AddRange(_mailRepository.GetWhereSenderAndRecipient(u.Id,
                    _authenticatedUser.Id));
            };

            List<Mail> recieved = mailsWhereSender
                .Where(m => !spammers
                .Select(sf => sf.SpammerId)
                .Contains(m.SenderId))
                .ToList();

            IList<Mail> lis = recieved
                .Where(m => m.SenderId != _authenticatedUser.Id)
                .OrderByDescending(m => m.TimeOfCreation)
                .ToList();

            IList<Mail> final = Extand.FilterByFormat(lis);

            if (!final.Any())
            {
                Console.WriteLine("Nismo pronašli niti jedan mail");
                Console.ReadLine();
                return;
            }

            Console.Clear();
            Extand.WriteListOfMails(final);
            Extand.SelectMailByIndex(final, _authenticatedUser);

            RegistredMenuFactory.CreateActions().PrintActionsAndOpen();
        }
    }
}
