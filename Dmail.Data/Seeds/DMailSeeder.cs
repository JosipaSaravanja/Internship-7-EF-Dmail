using System;
using Dmail.Data.Entitets.Models;
using Dmail.Data.Enums;
using Microsoft.EntityFrameworkCore;

namespace Dmail.Data.Seeds
{
	public static class DMailSeeder
	{
		public static void Seed(ModelBuilder builder)
		{
            //

            builder.Entity<User>()
                .HasData(new List<User>()
                {
                    new User()
                    {
                        Id = 1,
                        Email = "netkoprvi@dmail.hr",
                        Password = "^uPj226()y",
                    },
                    new User()
                    {
                        Id = 2,
                        Email = "netkodrugi@dmail.hr",
                        Password = "%4$sc9n&aS",
                    },
                    new User()
                    {
                        Id = 3,
                        Email = "netkotreci@dmail.hr",
                        Password = "v^L%9hByBb",
                    },
                });

            builder.Entity<Mail>()
                .HasData(new List<Mail>()
                {
                    new Mail()
                    {
                        Id = 1,
                        SenderId = 1,
                        Title = "prviMail",
                        TimeOfCreation = DateTime.UtcNow,
                        Format = Format.Email,
                        Contents = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum",
                    },
                    new Mail()
                    {
                        Id = 2,
                        SenderId = 2,
                        Title = "drugi mail",
                        TimeOfCreation = DateTime.UtcNow,
                        Format = Format.Event,
                        StartOfEvent = DateTime.UtcNow.AddMinutes(1),
                        LastingOfEvent = TimeSpan.FromMinutes(1),
                    },
                });

            builder.Entity<Recipient>()
                .HasData(new List<Recipient>()
                {
                    new Recipient()
                    {
                        MailId = 1,
                        UserId = 2,
                    },
                    new Recipient()
                    {
                        MailId = 1,
                        UserId = 3,
                    },
                    new Recipient()
                    {
                        MailId = 2,
                        UserId = 1,
                        EventStatus = EventStatus.NoResponse,
                    },
                    new Recipient()
                    {
                        MailId = 2,
                        UserId = 3,
                        EventStatus = EventStatus.NoResponse,
                    },
                });

            builder.Entity<Spammers>()
                .HasData(new List<Spammers>()
                {
                    new Spammers()
                    {
                        UserId = 1,
                        SpammerId = 2,
                    },
                    new Spammers()
                    {
                        UserId = 1,
                        SpammerId = 3,
                    },
                    new Spammers()
                    {
                        UserId = 2,
                        SpammerId = 3,
                    },
                });
            //
        }
	}
}
