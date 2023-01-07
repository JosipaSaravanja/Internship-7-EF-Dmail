﻿using System;
using Dmail.Presentation.Actions;
using Dmail.Presentation.Actions.Main;
using Dmail.Domain.Repositories;
using Dmail.Domain.Factories;
using Dmail.Data.Enums;
using Internship_7_EF_Dmail.Domain.Repositories;
using Dmail.Presentation.Actions.Inbox;

namespace Dmail.Presentation.Factories
{
    public class InboxMenuFactory
    {
        public static IList<IAction> CreateActions()
        {
            var actions = new List<IAction>
                {
                new ExitAction(),
                new ListMailsAction(
                    RepositoryFactory.Create<MailRepository>(),
                    RepositoryFactory.Create<SpammersRepository>(),
                    MailStatus.Read,
                    LogInAction.GetCurrentlyAuthenticatedUser()!),
                new ListMailsAction(
                    RepositoryFactory.Create<MailRepository>(),
                    RepositoryFactory.Create<SpammersRepository>(),
                    MailStatus.Unread,
                    LogInAction.GetCurrentlyAuthenticatedUser()!),
                new SearchMail(),
                };

            actions.SetActionIndexes();

            return actions;

        }
    }
}
