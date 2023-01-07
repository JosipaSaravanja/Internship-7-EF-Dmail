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
    public class MarkAsUnreadActtion : IAction
    {
        public int Index { get; set; }
        public string Name { get; set; } = "Označi kao nepročitano";

        private readonly MailRepository _mailRepository;
        private Mail _selected;
        private readonly User _authenticatedUser;

        public MarkAsUnreadActtion(
            MailRepository mailRepository,
            Mail selectedMail,
            User authenticatedUser)
        {
            _mailRepository = mailRepository;
            _selected = selectedMail;
            _authenticatedUser = authenticatedUser;
        }

        public void Open()
        {
            Console.Clear();

            ResponseResultType response = _mailRepository.UpdateMailStatus(
                _selected.Id,
                _authenticatedUser.Id,
                Data.Enums.MailStatus.Unread);

            Console.WriteLine(response);
        }
    }
}
