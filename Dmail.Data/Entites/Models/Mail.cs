using Dmail.Data.Enums;
using Dmail.Data.Entitets.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Data.Entitets.Models
{

    public class Mail
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public DateTime TimeOfCreation { get; set; }
        public Format Format { get; set; }
        public bool IsHidden { get; set; } = false;
        public string? Contents { get; set; }
        public DateTime? StartOfEvent { get; set; }
        public TimeSpan? LastingOfEvent { get; set; }
        public int SenderId { get; set; }
        public User Sender { get; set; } = null!;
        public ICollection<Recipient> Recipients { get; set; } = new List<Recipient>();

    }
}
