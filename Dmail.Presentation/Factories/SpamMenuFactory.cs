using System;
using Dmail.Presentation.Actions;
using Dmail.Presentation.Actions.Main;
using Dmail.Domain.Repositories;
using Dmail.Domain.Factories;
using Dmail.Data.Enums;
using Dmail.Domain.Repositories;
using Dmail.Presentation.Actions.Inbox;
using Dmail.Presentation.Actions.Spam;

namespace Dmail.Presentation.Factories
{
    public class SpamMenuFactory
    {
        public static IList<IAction> CreateActions()
        {
            var actions = new List<IAction>
                {
                new ExitAction(),
                new ListSpamMailsAction(
                    RepositoryFactory.Create<MailRepository>(),
                    RepositoryFactory.Create<SpammersRepository>(),
                    MailStatus.Read,
                    LogInAction.GetCurrentlyAuthenticatedUser()!),
                new ListSpamMailsAction(
                    RepositoryFactory.Create<MailRepository>(),
                    RepositoryFactory.Create<SpammersRepository>(),
                    MailStatus.Unread,
                    LogInAction.GetCurrentlyAuthenticatedUser()!),
                new SearchSpamMail(
                    RepositoryFactory.Create<MailRepository>(),
                    RepositoryFactory.Create<SpammersRepository>(),
                    RepositoryFactory.Create<UserRepository>(),
                    LogInAction.GetCurrentlyAuthenticatedUser()!),
                };

            actions.SetActionIndexes();

            return actions;

        }
    }
}
