using Dmail.Data.Entitets.Models;
using Dmail.Data.Enums;
using Dmail.Domain.Factories;
using Dmail.Domain.Repositories;
using Dmail.Presentation.Actions.Main;
using Dmail.Presentation.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Presentation.Actions.Settings
{
    public class ListAllNonSpamAdresses : IAction
    {

        private readonly MailRepository _mailRepository;
        private readonly User _authenticatedUser;
        private readonly SpammersRepository _spammersRepository;

        public ListAllNonSpamAdresses(
            MailRepository mailRepository,
            User authenticatedUser,
            SpammersRepository spammersRepository)
        {
            _mailRepository = mailRepository;
            _authenticatedUser = authenticatedUser;
            _spammersRepository = spammersRepository;
        }

        public int Index { get; set; }
        public string Name { get; set; } = "Ispis svih spam adresa";

        public void Open()
        {

            IList<Mail> allMails = _mailRepository
                .GetWhereReciever(_authenticatedUser.Id)
                .ToList();

            IList<Spammers> spammers = _spammersRepository
                .GetSpamFlagsForUser(_authenticatedUser.Id)
                .ToList();

            IList<Mail> spammedMails = allMails
                .Where(m => !spammers
                .Select(sf => sf.SpammerId)
                .Contains(m.SenderId))
                .ToList();

            var index = 0;
            List<User> lista = new List<User>();
            foreach (var el in spammedMails)
            {
                lista.Add(el.Sender);
            }
            lista = lista.Distinct().ToList();
            foreach (var el in lista)
            {
                Console.WriteLine($"{++index}. - {el.Email}");
            }

            Console.WriteLine("Želite li dopisati nekoga na spam listu (DA/NE)?");
            var choice = Console.ReadLine();
            if (choice == "Da")
            {
                Console.WriteLine("Unesite redni broj");
                choice = Console.ReadLine();
                var s = int.TryParse(choice, out index);
                if (s != false && index<lista.Count() && index >= 0)
                    _spammersRepository.MarkAsSpam(_authenticatedUser.Id, lista[index-1].Id);
            }
            else
            {
                RegistredMenuFactory.CreateActions().PrintActionsAndOpen();
            }

        }
    }
}
