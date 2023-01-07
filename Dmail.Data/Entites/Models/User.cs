using Dmail.Data.Entitets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Data.Entitets.Models
{    public class User
    {

        public int Id { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public ICollection<Mail> Send { get; set; } = new List<Mail>();
        public DateTime LastFailedLogin { get; set; }
        public ICollection<Spammers> Spammers { get; set; } = new List<Spammers>();
        public ICollection<Recipient> Received { get; set; } = new List<Recipient>();
    }
}
