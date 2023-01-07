using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dmail.Data.Entitets.Models;
using Dmail.Presentation.Actions;
using Dmail.Presentation.Factories;

namespace Dmail.Presentation.Actions.Main
{
    public class Registrate : IAction
    {
        public int Index { get; set; }
        public string Name { get; set; } = "Registracija";

        public void Open()
        {
        }
    }
}