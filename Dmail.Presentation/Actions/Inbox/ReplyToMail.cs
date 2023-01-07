using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dmail.Data.Entitets.Models;
using Dmail.Domain.Repositories;
using Dmail.Presentation.Factories;
using Dmail.Presentation.Actions;
using Dmail.Domain.Repositories;

namespace Dmail.Presentation.Actions.Inbox
{
    public class ReplyToMail: IAction
    {

        public int Index { get; set; }
        public string Name { get; set; } = "Odgovori na mail";


        public void Open()
        {
        }
    }
}
