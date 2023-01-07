using Dmail.Data.Entitets.Models;
using Dmail.Domain.Repositories;
using Dmail.Presentation.Actions;
using Dmail.Data.Entitets.Models;
using Dmail.Data.Enums;
using Dmail.Domain.Enums;
using Dmail.Domain.Repositories;
using System.Globalization;

namespace Dmail.Presentation.Actions.AuthenticatedUserActions
{
    public class SendNewEventAction : IAction
    {
        private readonly MailRepository _mailRepository;
        private readonly UserRepository _userRepository;
        private readonly User _authenticatedUser;

        public SendNewEventAction(
            MailRepository mailRepository,
            UserRepository userRepository,
            User authenticatedUser)
        {
            _mailRepository = mailRepository;
            _userRepository = userRepository;
            _authenticatedUser = authenticatedUser;
        }

        public int Index { get; set; }
        public string Name { get; set; } = "Novi event";

        public void Open()
        {
            Console.Clear();
            Mail newMail = new Mail()
            {
                SenderId = _authenticatedUser.Id,
                Format = Format.Event,
            };

            IList<string>? userInput = Extand.ReadRecipients();

            if (userInput == null)
            {
                Console.WriteLine("Nevaljan unos");
                Console.ReadLine();
                return;
            }

            if (userInput.Contains(_authenticatedUser.Email))
            {
                Console.WriteLine("NE možete pozvati sami sebe");
                userInput.Remove(_authenticatedUser.Email);
            }

            List<User> recipientsUsers = new List<User>();

            foreach(var ui in userInput){
                User toAdd = _userRepository.GetByEmail(ui);

                if (toAdd != null && !recipientsUsers.Contains(toAdd))
                    recipientsUsers.Add(toAdd);
            };

            if (!recipientsUsers.Any())
            {
                Console.WriteLine("Nijedan od unesenih emailova nije valjan");
                Console.ReadLine();
                return;
            }


            Console.WriteLine("Unesite naslov: ");
            string title = Console.ReadLine();
            newMail.Title = title;

            Console.WriteLine("Unesite sadržaj: ");
            string con = Console.ReadLine();
            newMail.Contents = con;

            Console.WriteLine("Unesite početak eventa: ");
            string input = Console.ReadLine();
            string format = "yyyy.MM.dd HH:mm:ss:ffff";
            DateTime time = DateTime.ParseExact(input, format, CultureInfo.InvariantCulture);
            if (time == null)
            {
                return;
            }
            newMail.StartOfEvent = time;
            if (newMail.StartOfEvent - DateTime.UtcNow <= TimeSpan.Zero)
            {
                Console.WriteLine("Event ne može započeti u prošlosti");
                Console.ReadLine();
                return;
            }

            Console.WriteLine("Unesite kraj eventa: ");
            input = Console.ReadLine();
            DateTime end= DateTime.ParseExact(input, format, CultureInfo.InvariantCulture);
            
            if (newMail.LastingOfEvent == null)
            {
                return;
            }
            newMail.LastingOfEvent = end-time;

            newMail.Recipients = new List<Recipient>();

            recipientsUsers.ForEach((u) =>
            {
                newMail.Recipients.Add(new Recipient()
                {
                    EventStatus = Data.Enums.EventStatus.NoResponse,
                });
            });

            ResponseResultType response = _mailRepository.Add(newMail);

            Console.WriteLine(response);
        }
    }
}