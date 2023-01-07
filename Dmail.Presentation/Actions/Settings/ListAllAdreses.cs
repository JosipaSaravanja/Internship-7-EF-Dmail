using Dmail.Data.Entitets.Models;
using Dmail.Domain.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Presentation.Actions.Settings
{
    public class ListAllAdreses : IAction
    {
        private readonly MailRepository _mailRepository;
        private readonly User _authenticatedUser;
        private readonly SpammersRepository _spammersRepository;

        public ListAllAdreses(
            MailRepository mailRepository,
            User authenticatedUser,
            SpammersRepository spammersRepository)
        {
            _mailRepository = mailRepository;
            _authenticatedUser = authenticatedUser;
            _spammersRepository = spammersRepository;
        }

        public int Index { get; set; }
        public string Name { get; set; } = "Ispis svih adresa koji su vam slali poruke";

        public void Open()
        {
            IList<Mail> allMailsA = _mailRepository
                .GetWhereReciever(_authenticatedUser.Id)
                .ToList();

            IList<Mail> allMailsB = _mailRepository
                .GetWhereSender(_authenticatedUser.Id)
                .ToList();

            List<User> lista = new List<User>();
            foreach (var el in allMailsA)
            {
                lista.Add(el.Sender);
            }
            foreach (var el in allMailsB)
            {
                lista.Add(el.Sender);
            }
            lista = lista.Distinct().ToList();
            var index = 0;
            foreach (var el in lista)
            {
                Console.WriteLine($"{++index}. - {el.Email}");
            }
        }
    }
}