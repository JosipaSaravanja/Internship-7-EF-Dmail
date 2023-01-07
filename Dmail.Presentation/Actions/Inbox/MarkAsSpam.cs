using Dmail.Data.Entitets.Models;
using Dmail.Domain.Repositories;
using Dmail.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dmail.Domain.Repositories;
using Dmail.Domain.Repositories;

namespace Dmail.Presentation.Actions.Inbox
{
    public class MarkAsSpam : IAction
    {
        public int Index { get; set; }
        public string Name { get; set; } = "Označi kao nepročitano";

        private readonly SpammersRepository _spammersRepository;
        private readonly Mail _selected;
        private readonly User _authenticatedUser;

        public MarkAsSpam(
            SpammersRepository spammersRepository,
            Mail mail,
            User authenticatedUser)
        {
            _spammersRepository = spammersRepository;
            _selected = mail;
            _authenticatedUser = authenticatedUser;
        }

        public void Open()
        {
            Console.Clear();

            ResponseResultType response = _spammersRepository.MarkAsSpam(
                _authenticatedUser.Id,
                _selected.SenderId);

            Console.WriteLine(response);
        }
    }
}
