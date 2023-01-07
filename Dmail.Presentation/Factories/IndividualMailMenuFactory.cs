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
                //new ReplayToMail()
                /*
            4.Odgovori na mail(ponaša se kao i pošalji novu poštu) ili u slučaju događaja 
            opcije da se potvrdi ili odbije dolazak na događaj(prilikom čega se automatski 
            šalje pošiljatelju generična obična pošta o tome što je odabrano sa naznakom da 
            kao pošiljatelj te pošte stoji korisnik koji je prihvatio ili odbio događaj)
            */
                };

            actions.SetActionIndexes();

            return actions;



        }
    }
}
