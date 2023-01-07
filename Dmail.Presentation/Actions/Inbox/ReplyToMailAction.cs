using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dmail.Data.Entitets.Models;
using Dmail.Data.Enums;
using Dmail.Domain.Repositories;
using Dmail.Domain.Enums;
using Dmail.Presentation.Actions;
using Dmail.Domain.Repositories;
using Dmail.Domain.Repositories;
using Dmail.Domain.Repositories;
using Dmail.Presentation.Factories;

namespace Dmail.Presentation.Actions.Inbox
{
    public class ReplyToMailAction : IAction
    {
        public int Index { get; set; }
        public string Name { get; set; } = "Odgovori na mail";

        private readonly MailRepository _mailRepository;
        private readonly Mail _selected;
        private readonly User _authenticatedUser;

        public ReplyToMailAction(
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

            Mail newMail = new Mail()
            {
                SenderId = _authenticatedUser.Id,
                Format = Format.Email,
                Recipients = new List<Recipient>()
                {
                    new Recipient(),
                }
            };

            Console.WriteLine("Unesite sadržaj: ");
            string con = Console.ReadLine();
            switch (_selected.Format)
            {
                case Format.Email:
                    newMail.Title = $"[Reply][{_selected.Title}]";
                    newMail.Contents = con;
                    break;

                case Data.Enums.Format.Event:
                    newMail.Title = $"[Response][{_selected.Title}]";
                    newMail.Contents = con + $"{_authenticatedUser.Email} odgovara na vaš poziv: ";
                    Console.WriteLine("1 - Accept + \n2 - Reject \n Vaš odabir: ");
                    var ch = int.TryParse(Console.ReadLine(), out var index);
                    if (ch != false && index > 0 && index <= 2)
                    {
                        _mailRepository.UpdateEventStatus(_selected.Id,
                            _authenticatedUser.Id,
                            index == 1 ? Data.Enums.EventStatus.Accepted : Data.Enums.EventStatus.Rejected);
                    }
                    break;
                default:
                    throw new Exception();
            }

            ResponseResultType response = _mailRepository.Add(newMail);

            Console.WriteLine(response);
        }
    }
}
