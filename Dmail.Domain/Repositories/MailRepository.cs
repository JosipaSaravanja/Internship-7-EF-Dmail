using Dmail.Data.Context;
using Dmail.Data.Entitets.Models;
using Dmail.Data.Enums;
using Dmail.Domain.Repositories;
using Dmail.Data.Enums;
using Dmail.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Dmail.Domain.Repositories
{
    public class MailRepository : BaseRepository
    {
        public MailRepository(DmailDBContext context) : base(context)
        {
        }

        public Mail? GetById(int id) => context.Mails.Find(id);

        public ICollection<Mail> GetAll() => context.Mails
            .Include(m => m.Sender)
            .Include(m => m.Recipients)
            .ToList();

        public ICollection<Mail> GetWhereSender(int senderId) => GetAll().Where(m => m.SenderId == senderId).ToList();

        public ICollection<Mail> GetWhereSenderAndRecipient(int senderId, int recipientId) => GetWhereSender(senderId)
            .Join(GetWhereReciever(recipientId),
            s => s.Id,
            r => r.Id,
            (s, r) => new { s, r })
            .Select(a => a.r)
            .ToList();

        public ICollection<Mail> GetWhereReciever(int recieverId) => GetAll()
            .Join(context.Recipients,
            m => m.Id,
            r => r.MailId,
            (m, r) => new { m, r })
            .Where(a => a.r.UserId == recieverId)
            .Select(a => a.m)
            .ToList();

        public ICollection<Mail> GetWhereRecieverAndStatus(int recieverId, MailStatus status) => GetAll()
            .Join(context.Recipients,
            m => m.Id,
            r => r.MailId,
            (m, r) => new { m, r })
            .Where(a => a.r.UserId == recieverId && a.r.MailStatus == status)
            .Select(a => a.m)
            .ToList();

        public ICollection<User> GetRecipients(int mailId) => context.Recipients.Where(r => r.MailId == mailId)
            .Join(context.Users,
            m => m.UserId,
            u => u.Id,
            (m, u) => new { m, u })
            .Select(a => a.u)
            .ToList();

        public ICollection<Mail> GetEmails() => GetAll().Where(m => m.Format == Format.Email).ToList();

        public ICollection<Mail> GetEvents() => GetAll().Where(m => m.Format == Format.Event).ToList();

        public ICollection<Mail> GetSpam(int recieverId) => GetWhereReciever(recieverId)
            .Join(context.Spammers,
            m => m.SenderId,
            sf => sf.UserId,
            (m, sf) => new { m, sf })
            .Select(a => a.m)
            .ToList();

        public ResponseResultType Add(Mail mail)
        {
            if (mail.Format == Format.Email && mail.Contents == null)
                return ResponseResultType.ErrorInvalidFormat;

            if (mail.Format == Format.Event &&
                (mail.StartOfEvent == null || mail.LastingOfEvent == null))
                return ResponseResultType.ErrorInvalidFormat;

            context.Mails.Add(mail);
            return SaveChanges();
        }


        public ResponseResultType UpdateMailStatus(int mailId, int userId, MailStatus status)
        {
            Mail? toChange = GetWhereReciever(userId).FirstOrDefault(m => m.Id == mailId);

            if (toChange == null)
                return ResponseResultType.ErrorNotFound;

            Recipient recipient = context.Recipients.Find(mailId, userId)!;

            recipient.MailStatus = status;

            return SaveChanges();
        }

        public ResponseResultType UpdateEventStatus(int mailId, int userId, EventStatus status)
        {
            Mail? toChange = GetWhereReciever(userId).FirstOrDefault(m => m.Id == mailId);

            if (toChange == null)
                return ResponseResultType.ErrorNotFound;

            if (toChange.Format !=Format.Event)
                return ResponseResultType.ErrorInvalidFormat;

            Recipient recipient = context.Recipients.Find(mailId, userId)!;

            recipient.EventStatus = status;

            return SaveChanges();
        }

        public ResponseResultType Delete(int mailId)
        {
            throw new NotSupportedException("Cannot delete a mail by id.");
        }

        public ResponseResultType RemoveFromInbox(int mailId, int userId)
        {
            Mail? toRemove = GetWhereReciever(userId).FirstOrDefault(m => m.Id == mailId);

            if (toRemove == null)
                return ResponseResultType.ErrorNotFound;

            Recipient recipient = context.Recipients.Find(mailId, userId)!;

            context.Recipients.Remove(recipient);

            return SaveChanges();
        }

        public ResponseResultType RemoveFromOutbox(int mailId)
        {
            Mail? toHide = GetById(mailId);

            if (toHide == null)
                return ResponseResultType.ErrorNotFound;


            return SaveChanges();
        }
    }

}