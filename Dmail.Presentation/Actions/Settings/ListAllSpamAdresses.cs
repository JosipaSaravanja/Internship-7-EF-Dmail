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
    public class ListAllSpamAdresses : IAction
    {

        private readonly MailRepository _mailRepository;
        private readonly User _authenticatedUser;
        private readonly SpammersRepository _spammersRepository;

        public ListAllSpamAdresses(
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
            IList<Mail> allMailsA = _mailRepository
                .GetWhereReciever(_authenticatedUser.Id)
                .ToList();

            IList<Mail> allMailsB = _mailRepository
                .GetWhereSender(_authenticatedUser.Id)
                .ToList();

            IList<Spammers> spammers = _spammersRepository
                .GetSpamFlagsForUser(_authenticatedUser.Id)
                .ToList();

            IList<Mail> spammedMailsA = allMailsA
                .Where(m => spammers
                .Select(sf => sf.SpammerId)
                .Contains(m.SenderId))
                .ToList();

            IList<Mail> spammedMailsB = allMailsB
                .Where(m => spammers
                .Select(sf => sf.SpammerId)
                .Contains(m.SenderId))
                .ToList();

            var index = 0;
            List<User> lista = new List<User>();
            foreach (var el in spammedMailsA)
            {
                lista.Add(el.Sender);
            }
            foreach (var el in spammedMailsB)
            {
                lista.Add(el.Sender);
            }
            lista = lista.Distinct().ToList();
            foreach (var el in lista)
            {
                Console.WriteLine($"{++index}. - {el.Email}");
            }

            Console.WriteLine("Želite li izbrisati nekoga sa spam liste (DA/NE)?");
            var choice = Console.ReadLine();
            if (choice == "Da")
            {
                Console.WriteLine("Unesite redni broj");
                choice = Console.ReadLine();
                var s = int.TryParse(choice, out index);
                if (s != false && index<lista.Count() && index >= 0)
                {
                    _spammersRepository.RemoveAsSpam(_authenticatedUser.Id, lista[index-1].Id);
                }
                
            }
            else
            {
                RegistredMenuFactory
                .CreateActions()
                .PrintActionsAndOpen();
            }

        }
    }
}
