using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurboKart.Infrastructure.Persistence.EfContexts.Migrations
{
    /// <inheritdoc />
    public partial class AddedBookingTimeProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Start",
                table: "Bookings",
                newName: "Time_Start");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Time_Duration",
                table: "Bookings",
                type: "time",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));

            migrationBuilder.AddColumn<DateTime>(
                name: "Time_End",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Time_Duration",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Time_End",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "Time_Start",
                table: "Bookings",
                newName: "Start");
        }
    }
}
