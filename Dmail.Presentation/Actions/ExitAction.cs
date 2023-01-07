using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dmail.Presentation.Actions;

namespace Dmail.Presentation.Actions
{
    public class ExitAction : IAction
    {
        public int Index { get; set; }
        public virtual string Name { get; set; } = "Izlaz";

        public ExitAction()
        {
        }

        public void Open()
        {
        }
    }
}
