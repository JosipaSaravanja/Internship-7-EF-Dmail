using System;
using Dmail.Presentation.Actions;
using Dmail.Presentation.Actions.Main;
using Dmail.Domain.Repositories;
using Dmail.Domain.Factories;

namespace Dmail.Presentation.Factories
{
    public class MainMenuFactory
    {
            public static IList<IAction> CreateActions()
            {
                var actions = new List<IAction>
                {
                new ExitAction(),
                new LogInAction(RepositoryFactory.Create<UserRepository>()),
                new RegisterAction(RepositoryFactory.Create<UserRepository>()),
                };

                actions.SetActionIndexes();

                return actions;

            }
    }
}
