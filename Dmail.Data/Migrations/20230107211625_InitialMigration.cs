using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Dmail.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    LastFailedLogin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Mails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    TimeOfCreation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "timezone('utc', now())"),
                    Format = table.Column<int>(type: "integer", nullable: false),
                    IsHidden = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    Contents = table.Column<string>(type: "text", nullable: true),
                    StartOfEvent = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastingOfEvent = table.Column<TimeSpan>(type: "interval", nullable: true),
                    SenderId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Mails_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Spammers",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    SpammerId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spammers", x => new { x.UserId, x.SpammerId });
                    table.ForeignKey(
                        name: "FK_Spammers_Users_SpammerId",
                        column: x => x.SpammerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Spammers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recipients",
                columns: table => new
                {
                    MailId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    MailStatus = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    EventStatus = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipients", x => new { x.MailId, x.UserId });
                    table.ForeignKey(
                        name: "FK_Recipients_Mails_MailId",
                        column: x => x.MailId,
                        principalTable: "Mails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Recipients_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "LastFailedLogin", "Password" },
                values: new object[,]
                {
                    { 1, "netkoprvi@dmail.hr", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "^uPj226()y" },
                    { 2, "netkodrugi@dmail.hr", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "%4$sc9n&aS" },
                    { 3, "netkotreci@dmail.hr", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "v^L%9hByBb" }
                });

            migrationBuilder.InsertData(
                table: "Mails",
                columns: new[] { "Id", "Contents", "Format", "LastingOfEvent", "SenderId", "StartOfEvent", "TimeOfCreation", "Title" },
                values: new object[,]
                {
                    { 1, "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum", 0, null, 1, null, new DateTime(2023, 1, 7, 21, 16, 25, 325, DateTimeKind.Utc).AddTicks(417), "prviMail" },
                    { 2, null, 1, new TimeSpan(0, 0, 1, 0, 0), 2, new DateTime(2023, 1, 7, 21, 17, 25, 325, DateTimeKind.Utc).AddTicks(423), new DateTime(2023, 1, 7, 21, 16, 25, 325, DateTimeKind.Utc).AddTicks(422), "drugi mail" }
                });

            migrationBuilder.InsertData(
                table: "Spammers",
                columns: new[] { "SpammerId", "UserId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 3, 1 },
                    { 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "Recipients",
                columns: new[] { "MailId", "UserId", "EventStatus" },
                values: new object[,]
                {
                    { 1, 2, null },
                    { 1, 3, null },
                    { 2, 1, 0 },
                    { 2, 3, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mails_SenderId",
                table: "Mails",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipients_UserId",
                table: "Recipients",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Spammers_SpammerId",
                table: "Spammers",
                column: "SpammerId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Recipients");

            migrationBuilder.DropTable(
                name: "Spammers");

            migrationBuilder.DropTable(
                name: "Mails");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
