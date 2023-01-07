using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Presentation.Actions
{
    public interface IAction
    {
        int Index { get; set; }
        string Name { get; set; }
        public void Open();
    }
}
