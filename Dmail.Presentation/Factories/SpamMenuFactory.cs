using System;
using Dmail.Presentation.Actions;
using Dmail.Presentation.Actions.Main;
using Dmail.Domain.Repositories;
using Dmail.Domain.Factories;
using Dmail.Data.Enums;
using Dmail.Domain.Repositories;
using Dmail.Presentation.Actions.Inbox;
using Dmail.Presentation.Actions.Spam;
using Dmail.Presentation.Actions.Settings;

namespace Dmail.Presentation.Factories
{
    public class SpamMenuFactory
    {
        public static IList<IAction> CreateActions()
        {
            var actions = new List<IAction>
                {
                new ExitAction(),
                new ListAllSpamAdresses(
                    RepositoryFactory.Create<MailRepository>(),
                    LogInAction.GetCurrentlyAuthenticatedUser()!,
                    RepositoryFactory.Create<SpammersRepository>()),

                new ListAllNonSpamAdresses(
                    RepositoryFactory.Create<MailRepository>(),
                    LogInAction.GetCurrentlyAuthenticatedUser()!,
                    RepositoryFactory.Create<SpammersRepository>()),

                new ListAllAdreses(
                    RepositoryFactory.Create<MailRepository>(),
                    LogInAction.GetCurrentlyAuthenticatedUser()!,
                    RepositoryFactory.Create<SpammersRepository>()),
                };

            actions.SetActionIndexes();

            return actions;

        }
    }
}
