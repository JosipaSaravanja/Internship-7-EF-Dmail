using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Data.Entitets.Models
{
    public class Spammers
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int SpammerId { get; set; }
        public User Spammer { get; set; } = null!;
    }
}
