using System;
using Dmail.Presentation.Actions;
using Dmail.Presentation.Actions.Main;
using Dmail.Presentation.Actions;
using Dmail.Domain.Repositories;
using Dmail.Domain.Factories;
using Dmail.Data.Enums;
using Dmail.Domain.Repositories;

namespace Dmail.Presentation.Factories
{
    public class RegistredMenuFactory
    {
            public static IList<IAction> CreateActions()
            {
            var actions = new List<IAction>
                {
                new ExitAction(),
                new OpenInboxAction(),
                //outbox
                //spam
                //pošalji movi mail
                //pošalji movi event
                //postavke profila
                new LogOutAction(LogInAction.Logout),
                };

                actions.SetActionIndexes();

                return actions;

            }
    }
}
