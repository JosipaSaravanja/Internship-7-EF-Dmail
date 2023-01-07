using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Dmail.Data.Entitets.Models;
using Dmail.Data.Enums;
using Dmail.Domain.Enums;
using Dmail.Domain.Factories;
using Dmail.Presentation.Actions;
using Dmail.Presentation.Factories;
using Dmail.Domain.Repositories;

namespace Dmail.Presentation.Actions
{
    public static class Extand
    {
        public static void PrintActionsAndOpen(this IList<IAction> actions)
        {
            const string INVALID_ACTION_MSG = "Odaberite posojeću akciju!";

            var loop = false;
            do
            {
                foreach (var ac in actions)
                {
                    Console.WriteLine($"{ac.Index} - {ac.Name}");
                }

                var isInputValid = int.TryParse(Console.ReadLine(), out var actionIndex);
                if (!isInputValid)
                {
                    PrintErrorMessage(INVALID_ACTION_MSG);
                    continue;
                }

                var action = actions.FirstOrDefault(a => a.Index == actionIndex);
                if (action is null)
                {
                    PrintErrorMessage(INVALID_ACTION_MSG);
                    continue;
                }


                action.Open();

                loop = action is ExitAction;

            } while (loop != true);
        }
        public static void SelectMailByIndex(IList<Mail> final, User _authenticatedUser)
        {

            Console.WriteLine("Unesite rb maila");
            bool suc = int.TryParse(Console.ReadLine(), out int choice);
            if (suc == false || choice >= final.Count() || choice<0)
            {
                Console.WriteLine("Netočan unos");
                return;
            }

            Mail Mail = final[choice-1];
            if (Mail.Format == Format.Email)
            {
                Console.WriteLine($"Title: {Mail.Title}");
                Console.WriteLine($"Datum i vrijeme: {Mail.TimeOfCreation}");
                Console.WriteLine($"Posiljatelj: {Mail.Sender.Email}");
                Console.WriteLine($"Content: {Mail.Contents}");
                ResponseResultType response = RepositoryFactory.Create<MailRepository>().UpdateMailStatus(
               Mail.Id,
               _authenticatedUser.Id,
               Data.Enums.MailStatus.Unread);
            }
            else if (Mail.Format == Format.Event)
            {
                Console.WriteLine($"Title: {Mail.Title}");
                Console.WriteLine($"Datum i vrijeme: {Mail.StartOfEvent}");
                Console.WriteLine($"Posiljatelj: {Mail.Sender.Email}");
                Console.WriteLine($"Pozvani korisnici: ");
                foreach (var person in Mail.Recipients)
                {
                    Console.WriteLine(person.User.Email);
                }
                Console.WriteLine($"prihvaćen/odbijen poziv n a događaj: {Mail.Recipients.FirstOrDefault(u => u.UserId == _authenticatedUser.Id).EventStatus}");
            }
            if (_authenticatedUser != Mail.Sender)
            {
                IndividualOutboxMailMenuFactory
                .CreateActions(Mail)
                .PrintActionsAndOpen();
            }
            else
            {
                IndividualMailMenuFactory
                    .CreateActions(Mail)
                    .PrintActionsAndOpen();
            }
        }
        private static void PrintActions(IList<IAction> actions)
        {
            foreach (var action in actions)
            {
                Console.WriteLine($"{action.Index}. {action.Name}");
            }
        }
        private static void PrintErrorMessage(string message)
        {
            Console.WriteLine(message);
            Console.ReadLine();
            Console.Clear();
        }
        public static void WriteListOfMails(IList<Mail> mails)
        {
            Console.WriteLine(" Rb. | Naslov                                            | Pošiljatelj");
            if (!mails.Any())
                return;
            int rb = 0;
            foreach(var mail in mails)
            {
                Console.WriteLine($"[{++rb}] [{mail.Title}] [{mail.Sender.Email}]");
            }

        }
        public static IList<Mail> FilterByFormat(ICollection<Mail> input)
        {
            Console.Clear();
            Console.WriteLine("Želite li ih usput i filtrirasti po eventovima i mailovima? (DA/NE)");
            var ch = Console.ReadLine();
            if (ch!="DA")
            {
                Console.Clear();
                return input.ToList();
            }
            var loop = true;
            do
            {
                Console.Clear();
                Console.WriteLine("1 - Mailovi");
                Console.WriteLine("2 - Eventovi");
                ch = Console.ReadLine();
                switch (ch)
                {
                    case "1":
                        loop = false;
                        break;
                    case "2":
                        loop = false;
                        break;
                    default:
                        Console.WriteLine("Netočan unos");
                        break;
                }
            } while (loop == true);
            return input.ToList();
        }
        public static void SetActionIndexes(this IList<IAction> actions)
        {
            var index = -1;
            foreach (var action in actions)
            {
                action.Index = ++index;
            }
        }
        public static void SelectMailByIndexFromOutbox(IList<Mail> final, User _authenticatedUser)
        {
            Console.WriteLine("Unesite rb maila");
            bool suc = int.TryParse(Console.ReadLine(), out int choice);
            if (suc == false || choice >= final.Count() || choice < 0)
            {
                Console.WriteLine("Netočan unos");
                return;
            }

            Mail Mail = final[choice - 1];

            Console.WriteLine($"Title: {Mail.Title}");
            Console.WriteLine($"Datum i vrijeme: {Mail.TimeOfCreation}");
            Console.WriteLine($"Poslano na adrese: ");
            foreach (var person in Mail.Recipients)
            {
                Console.WriteLine(person.User.Email);
            }
            Console.WriteLine($"Content: {Mail.Contents}");
            IndividualMailMenuFactory.CreateActions(Mail);
        }

        public static IList<string>? ReadRecipients()
        {
            Console.WriteLine("Unesite primatelje");
            string userInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(userInput))
            {
                return null;
            }

            IList<string> userInputSplit = userInput.Split(',').ToList();

            for (int i = 0; i < userInputSplit.Count; i++)
                userInputSplit[i] = userInputSplit[i].ToLower().Trim();

            return userInputSplit;
        }
    }
}
