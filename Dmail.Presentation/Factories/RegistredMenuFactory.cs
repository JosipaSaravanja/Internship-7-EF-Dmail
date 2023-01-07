using System;
using Dmail.Presentation.Actions;
using Dmail.Presentation.Actions.Main;
using Dmail.Presentation.Actions;
using Dmail.Domain.Repositories;
using Dmail.Domain.Factories;
using Dmail.Data.Enums;
using Internship_7_EF_Dmail.Domain.Repositories;

namespace Dmail.Presentation.Factories
{
    public class RegistredMenuFactory
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
                //spam
                //pošalji movi mail
                //pošalji movi event
                //postavke profila
                //odjava iz profila
                };

                actions.SetActionIndexes();

                return actions;

            }
    }
}
