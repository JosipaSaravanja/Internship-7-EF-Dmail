﻿// <auto-generated />
using System;
using Dmail.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dmail.Data.Migrations
{
    [DbContext(typeof(DmailDBContext))]
    partial class DmailDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Dmail.Data.Entitets.Models.Mail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Contents")
                        .HasColumnType("text");

                    b.Property<int>("Format")
                        .HasColumnType("integer");

                    b.Property<TimeSpan?>("LastingOfEvent")
                        .HasColumnType("interval");

                    b.Property<int>("SenderId")
                        .HasColumnType("integer");

                    b.Property<DateTime?>("StartOfEvent")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("TimeOfCreation")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp with time zone")
                        .HasDefaultValueSql("timezone('utc', now())");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("SenderId");

                    b.ToTable("Mails");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Contents = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum",
                            Format = 0,
                            SenderId = 1,
                            TimeOfCreation = new DateTime(2023, 1, 7, 16, 14, 14, 419, DateTimeKind.Utc).AddTicks(1130),
                            Title = "prviMail"
                        },
                        new
                        {
                            Id = 2,
                            Format = 1,
                            LastingOfEvent = new TimeSpan(0, 0, 1, 0, 0),
                            SenderId = 2,
                            StartOfEvent = new DateTime(2023, 1, 7, 16, 15, 14, 419, DateTimeKind.Utc).AddTicks(1134),
                            TimeOfCreation = new DateTime(2023, 1, 7, 16, 14, 14, 419, DateTimeKind.Utc).AddTicks(1133),
                            Title = "drugi mail"
                        });
                });

            modelBuilder.Entity("Dmail.Data.Entitets.Models.Recipient", b =>
                {
                    b.Property<int>("MailId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int?>("EventStatus")
                        .HasColumnType("integer");

                    b.Property<int>("MailStatus")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasDefaultValue(0);

                    b.HasKey("MailId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Recipients");

                    b.HasData(
                        new
                        {
                            MailId = 1,
                            UserId = 2,
                            MailStatus = 0
                        },
                        new
                        {
                            MailId = 1,
                            UserId = 3,
                            MailStatus = 0
                        },
                        new
                        {
                            MailId = 2,
                            UserId = 1,
                            EventStatus = 0,
                            MailStatus = 0
                        },
                        new
                        {
                            MailId = 2,
                            UserId = 3,
                            EventStatus = 0,
                            MailStatus = 0
                        });
                });

            modelBuilder.Entity("Dmail.Data.Entitets.Models.Spammers", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("SpammerId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "SpammerId");

                    b.HasIndex("SpammerId");

                    b.ToTable("Spammers");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            SpammerId = 2
                        },
                        new
                        {
                            UserId = 1,
                            SpammerId = 3
                        },
                        new
                        {
                            UserId = 2,
                            SpammerId = 3
                        });
                });

            modelBuilder.Entity("Dmail.Data.Entitets.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("LastFailedLogin")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "netkoprvi@dmail.hr",
                            LastFailedLogin = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Password = "^uPj226()y"
                        },
                        new
                        {
                            Id = 2,
                            Email = "netkodrugi@dmail.hr",
                            LastFailedLogin = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Password = "%4$sc9n&aS"
                        },
                        new
                        {
                            Id = 3,
                            Email = "netkotreci@dmail.hr",
                            LastFailedLogin = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Password = "v^L%9hByBb"
                        });
                });

            modelBuilder.Entity("Dmail.Data.Entitets.Models.Mail", b =>
                {
                    b.HasOne("Dmail.Data.Entitets.Models.User", "Sender")
                        .WithMany("Send")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Dmail.Data.Entitets.Models.Recipient", b =>
                {
                    b.HasOne("Dmail.Data.Entitets.Models.Mail", "Mail")
                        .WithMany("Recipients")
                        .HasForeignKey("MailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dmail.Data.Entitets.Models.User", "User")
                        .WithMany("Received")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Mail");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Dmail.Data.Entitets.Models.Spammers", b =>
                {
                    b.HasOne("Dmail.Data.Entitets.Models.User", "Spammer")
                        .WithMany()
                        .HasForeignKey("SpammerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Dmail.Data.Entitets.Models.User", "User")
                        .WithMany("Spammers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Spammer");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Dmail.Data.Entitets.Models.Mail", b =>
                {
                    b.Navigation("Recipients");
                });

            modelBuilder.Entity("Dmail.Data.Entitets.Models.User", b =>
                {
                    b.Navigation("Received");

                    b.Navigation("Send");

                    b.Navigation("Spammers");
                });
#pragma warning restore 612, 618
        }
    }
}
