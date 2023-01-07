using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dmail.Data.Migrations
{
    public partial class druga : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Mails",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeOfCreation",
                value: new DateTime(2023, 1, 7, 16, 14, 14, 419, DateTimeKind.Utc).AddTicks(1130));

            migrationBuilder.UpdateData(
                table: "Mails",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "StartOfEvent", "TimeOfCreation" },
                values: new object[] { new DateTime(2023, 1, 7, 16, 15, 14, 419, DateTimeKind.Utc).AddTicks(1134), new DateTime(2023, 1, 7, 16, 14, 14, 419, DateTimeKind.Utc).AddTicks(1133) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Mails",
                keyColumn: "Id",
                keyValue: 1,
                column: "TimeOfCreation",
                value: new DateTime(2023, 1, 7, 16, 1, 41, 338, DateTimeKind.Utc).AddTicks(1934));

            migrationBuilder.UpdateData(
                table: "Mails",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "StartOfEvent", "TimeOfCreation" },
                values: new object[] { new DateTime(2023, 1, 7, 16, 2, 41, 338, DateTimeKind.Utc).AddTicks(1937), new DateTime(2023, 1, 7, 16, 1, 41, 338, DateTimeKind.Utc).AddTicks(1937) });
        }
    }
}
