using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TurboKart.Infrastructure.Persistence.EfContexts.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBookingProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DriverCount",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DriverCount",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Bookings");
        }
    }
}
