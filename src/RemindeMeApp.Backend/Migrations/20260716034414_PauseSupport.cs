using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RemindeMeApp.Backend.Migrations
{
    /// <inheritdoc />
    public partial class PauseSupport : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPaused",
                table: "TimerSessions",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PausedElapsedSeconds",
                table: "TimerSessions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataHoraFinalizacao",
                table: "TaskItems",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaused",
                table: "TimerSessions");

            migrationBuilder.DropColumn(
                name: "PausedElapsedSeconds",
                table: "TimerSessions");

            migrationBuilder.DropColumn(
                name: "DataHoraFinalizacao",
                table: "TaskItems");
        }
    }
}
