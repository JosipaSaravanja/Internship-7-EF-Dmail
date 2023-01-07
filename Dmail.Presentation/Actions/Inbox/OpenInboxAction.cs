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
using Internship_7_EF_Dmail.Domain.Repositories;
using Dmail.Presentation.Factories;

namespace Dmail.Presentation.Actions
{
    public class OpenInboxAction : IAction
    {

        public int Index { get; set; }
        public string Name { get; set; } = "Inbox";


        public void Open()
        {
            InboxMenuFactory
                .CreateActions()
                .PrintActionsAndOpen();
        }
    }
}
