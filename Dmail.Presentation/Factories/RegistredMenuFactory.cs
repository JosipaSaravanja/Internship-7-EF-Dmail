using System;
using Dmail.Presentation.Actions;
using Dmail.Presentation.Actions.Main;
using Dmail.Domain.Repositories;
using Dmail.Domain.Factories;

namespace Dmail.Presentation.Factories
{
    public class RegistredMenuFactory
    {
            public static IList<IAction> CreateActions()
            {
                var actions = new List<IAction>
                {
                new ExitAction(),
                //Ulazna pošta
                //izlazna pošta
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
