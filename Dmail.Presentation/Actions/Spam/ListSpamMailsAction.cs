using Dmail.Data.Entitets.Models;
using Dmail.Data.Enums;
using Dmail.Domain.Repositories;
using Dmail.Presentation.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Presentation.Actions.Spam
{
    public class ListSpamMailsAction : IAction
    {

        public int Index { get; set; }
        public string Name { get; set; } = "Pročitani spam mailovi";


        private readonly MailRepository _mailRepository;
        private readonly SpammersRepository _spammersRepository;
        private readonly MailStatus _mailStatus;
        private readonly User _authenticatedUser;

        public ListSpamMailsAction(
            MailRepository mailRepository,
            SpammersRepository spamFlagRepository,
            MailStatus mailStatus,
            User authenticatedUser)
        {
            _mailRepository = mailRepository;
            _spammersRepository = spamFlagRepository;
            _mailStatus = mailStatus;
            _authenticatedUser = authenticatedUser;
        }
        public void Open()
        {
            IList<Mail> allMails = _mailRepository
                .GetWhereRecieverAndStatus(
                _authenticatedUser.Id,
                _mailStatus)
                .ToList();

            IList<Spammers> spammers = _spammersRepository
                .GetSpamFlagsForUser(_authenticatedUser.Id)
                .ToList();

            IList<Mail> spammedMails = allMails
                .Where(m => spammers
                .Select(sf => sf.SpammerId)
                .Contains(m.SenderId))
                .ToList();

            Console.Clear();


            IList<Mail> final = Extand.FilterByFormat(spammedMails)
                .OrderByDescending(m => m.TimeOfCreation)
                .ToList();

            if (!final.Any())
            {
                Console.WriteLine("------------------Nemate poruka---------------------");
                Console.ReadLine();
                return;
            }

            Extand.WriteListOfMails(final);

            Extand.SelectMailByIndex(final, _authenticatedUser);

            RegistredMenuFactory
                .CreateActions()
                .PrintActionsAndOpen();
        }
    }
}
