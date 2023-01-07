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
    public class DeleteMailAction : IAction
    {
        public int Index { get; set; }
        public string Name { get; set; } = "Označi kao nepročitano";

        private readonly MailRepository _mailRepository;
        private readonly Mail _selected;
        private readonly User _authenticatedUser;

        public DeleteMailAction(
            MailRepository mailRepository,
            Mail selected,
            User authenticatedUser)
        {
            _mailRepository = mailRepository;
            _selected = selected;
            _authenticatedUser = authenticatedUser;
        }

        public void Open()
        {
            Console.Clear();

            if (_selected.Format == Data.Enums.Format.Event)
                Console.WriteLine("Brisanje ovod eventa će vas ukloniti s liste pozvanika.");
            Console.WriteLine("Jeste li sigurni da želite izvršiti ovu radnju (DA/NE)?");
            var choice = Console.ReadLine();
            if (choice!="DA")
            {
                Console.WriteLine("Radnja je obustavljena");
                return;
            }

            ResponseResultType response = _mailRepository.RemoveFromInbox(_selected.Id,
                _authenticatedUser.Id);

            Console.WriteLine(response);
        }
    }
}
