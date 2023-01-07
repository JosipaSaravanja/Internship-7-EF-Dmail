using Dmail.Domain.Factories;
using Dmail.Domain.Repositories;
using Dmail.Presentation.Actions.Main;
using Dmail.Presentation.Actions;
using System;
using Dmail.Domain.Repositories;
using Dmail.Presentation.Actions.Inbox;
using Dmail.Data.Entitets.Models;

namespace Dmail.Presentation.Factories
{
    public class IndividualMailMenuFactory
    {
        public static IList<IAction> CreateActions(Mail selected)
        {
            var actions = new List<IAction>
                {
                new ExitAction(),
                new MarkAsUnreadActtion(
                    RepositoryFactory.Create<MailRepository>(),
                    selected,
                    LogInAction.GetCurrentlyAuthenticatedUser()!),
                new MarkAsSpam(
                    RepositoryFactory.Create<SpammersRepository>(),
                    selected,
                    LogInAction.GetCurrentlyAuthenticatedUser()!),
                new DeleteMailAction(
                    RepositoryFactory.Create<MailRepository>(),
                    selected,
                    LogInAction.GetCurrentlyAuthenticatedUser()!),
                new ReplyToMailAction(
                    RepositoryFactory.Create<MailRepository>(),
                    selected,
                    LogInAction.GetCurrentlyAuthenticatedUser()!),
                };

            actions.SetActionIndexes();
            return actions;
        }
    }
}
