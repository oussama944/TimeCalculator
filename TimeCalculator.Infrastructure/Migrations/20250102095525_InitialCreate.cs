using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimeCalculator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TimeEntries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MorningStart = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    MorningEnd = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    AfternoonStart = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    AfternoonEnd = table.Column<TimeSpan>(type: "TEXT", nullable: false),
                    MinimumLunchBreak = table.Column<TimeSpan>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeEntries", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TimeEntries_Date",
                table: "TimeEntries",
                column: "Date",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeEntries");
        }
    }
}
