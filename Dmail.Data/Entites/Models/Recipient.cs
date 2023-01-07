using Dmail.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dmail.Data.Entitets.Models
{
    public class Recipient
    {
        public int MailId { get; set; }
        public Mail Mail { get; set; } = null!;
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public MailStatus MailStatus { get; set; } = Enums.MailStatus.Unread;
        public EventStatus? EventStatus { get; set; }
    }
}
