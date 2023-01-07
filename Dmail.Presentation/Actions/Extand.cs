using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dmail.Data.Entitets.Models;
using Dmail.Presentation.Actions;

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

                loop=action is ExitAction;
                
            } while (loop!=true);
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

        


    }
}
