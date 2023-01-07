using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dmail.Data.Entitets.Models;
using Dmail.Data.Enums;
using Dmail.Domain.Repositories;
using Dmail.Presentation.Actions;
using Dmail.Presentation.Factories;
using Dmail.Domain.Repositories;
using Dmail.Domain.Repositories;
using Dmail.Domain.Repositories;
using Dmail.Presentation.Factories;

namespace Dmail.Presentation.Actions
{
    public class OutboxMenuAction : IAction
    {

        public int Index { get; set; }
        public string Name { get; set; } = "Izlazna pođta";


        public void Open()
        {
            OutboxMenuFactory
                .CreateActions()
                .PrintActionsAndOpen();
        }
    }
}
