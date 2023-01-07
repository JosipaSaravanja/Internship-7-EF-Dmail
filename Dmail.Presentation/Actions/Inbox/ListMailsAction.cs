﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dmail.Data.Entitets.Models;
using Dmail.Data.Enums;
using Dmail.Domain.Repositories;
using Dmail.Presentation.Actions;
using Dmail.Domain.Repositories;
using Dmail.Domain.Repositories;
using Internship_7_EF_Dmail.Domain.Repositories;

namespace Dmail.Presentation.Actions
{
    public class ListMailsAction : IAction
    {

        public int Index { get; set; }
        public string Name { get; set; } = "Pročitani mailovi";


        private readonly MailRepository _mailRepository;
        private readonly SpammersRepository _spammersRepository;
        private readonly MailStatus _mailStatus;
        private readonly User _authenticatedUser;

        public ListMailsAction(
            MailRepository mailRepository,
            SpammersRepository spamFlagRepository,
            MailStatus mailStatus,
            User authenticatedUser)
        {
            Name = mailStatus switch
            {
                MailStatus.Read => "Pročitane poruke",
                MailStatus.Unread => "Nepročitane poruke",
                _ => throw new NotSupportedException(),
            };
            _mailRepository = mailRepository;
            _spammersRepository = spamFlagRepository;
            _mailStatus = mailStatus;
            _authenticatedUser = authenticatedUser;
        }
        public void Open()
        {
            IList<Mail> allMails = _mailRepository
                .GetWhereRecieverAndStatus(
                _authenticatedUser.Id,
                _mailStatus)
                .ToList();

            IList<Spammers> spammers = _spammersRepository
                .GetSpamFlagsForUser(_authenticatedUser.Id)
                .ToList();

            IList<Mail> noSpammedMails = allMails
                .Where(m => !spammers
                .Select(sf => sf.SpammerId)
                .Contains(m.SenderId))
                .ToList();

            Console.Clear();

            Console.WriteLine("1 - Filtriraj na mailove i evente");
            Console.WriteLine("2 - Prikaži mail");
            bool suc= int.TryParse(Console.ReadLine(), out int choice);
            
            IList<Mail> final = Extand.FilterByFormat(noSpammedMails)
                .OrderByDescending(m => m.TimeOfCreation)
                .ToList();

            if (!final.Any())
            {
                Console.WriteLine("------------------Nemate poruka---------------------");
                Console.ReadLine();
                return;
            }

            Extand.WriteListOfMails(final);
            
        }
    }
}