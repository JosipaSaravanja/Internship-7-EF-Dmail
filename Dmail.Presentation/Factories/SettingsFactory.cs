using Dmail.Data.Enums;
using Dmail.Domain.Factories;
using Dmail.Domain.Repositories;
using Dmail.Presentation.Actions.Inbox;
using Dmail.Presentation.Actions.Main;
using Dmail.Presentation.Actions.Spam;
using Dmail.Presentation.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dmail.Presentation.Actions.Settings;

namespace Dmail.Presentation.Factories
{
    public class SettingsMenuFactory
    {
        public static IList<IAction> CreateActions()
        {
            var actions = new List<IAction>()
            {
                new ExitAction(),

                new RemoveSpammersAction(
                    RepositoryFactory.Create<UserRepository>(),
                    LogInAction.GetCurrentlyAuthenticatedUser()!,
                    RepositoryFactory.Create<SpammersRepository>()),
            };

            actions.PrintActionsAndOpen();

            return actions;
        }
    }
}