using Dmail.Data.Entitets.Models;
using Dmail.Data.Enums;
using Dmail.Data.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Dmail.Data.Context
{
    public class DmailDBContext : DbContext
    {
        public DmailDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User>? Users => Set<User>();
        public DbSet<Mail>? Mails => Set<Mail>();
        public DbSet<Recipient>? Recipients => Set<Recipient>();
        public DbSet<Spammers>? Spammers => Set<Spammers>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().
                Property(u => u.Email).
                IsRequired();

            builder.Entity<User>().
                HasIndex(u => u.Email).
                IsUnique();

            builder.Entity<User>().
                Property(u => u.Password).
                IsRequired();

            builder.Entity<Mail>().
                Property(m => m.Title).
                IsRequired();

            builder.Entity<Mail>().
                Property(m => m.TimeOfCreation).
                IsRequired().
                HasDefaultValueSql("timezone('utc', now())");

            builder.Entity<Mail>().
                Property(m => m.Hide).
                IsRequired().
                HasDefaultValue(false);

            builder.Entity<Mail>().
                Property(m => m.Format).
                IsRequired();


            builder.Entity<Spammers>().
                HasKey(sf => new { sf.UserId, sf.SpammerId });

            builder.Entity<Spammers>().
                HasOne(u=>u.User).
                WithMany(z => z.Spammers).
                HasForeignKey(sf => sf.UserId);


            builder.Entity<Recipient>().
                HasKey(r => new { r.MailId, r.UserId });

            builder.Entity<Recipient>().
                Property(r => r.MailStatus).
                HasDefaultValue(MailStatus.Unread);

            DMailSeeder.Seed(builder);

            base.OnModelCreating(builder);
        }
    }
    public class DmailDBContextFactory : IDesignTimeDbContextFactory<DmailDBContext>
    {
        public DmailDBContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddXmlFile("App.config")
                .Build();

            config.Providers
                .First()
                .TryGet("connectionStrings:add:DmailApp:connectionString", out var connectionString);

            var options = new DbContextOptionsBuilder<DmailDBContext>()
                .UseNpgsql(connectionString)
                .Options;

            return new DmailDBContext(options);
        }
    }
}