using System;
using Dmail.Presentation.Actions;

namespace Dmail.Presentation.Factories
{
    public class MainMenuFactory
    {
            public static IList<IAction> CreateActions()
            {
                var actions = new List<IAction>
                {
                new ExitAction(),
                //new LogInAction(),
                //new RegisterAction(),
                };

                actions.SetActionIndexes();

                return actions;

            }
    }
}
