using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantsReservations.Migrations
{
    public partial class reservationParam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Meal",
                table: "Reservations",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Meal",
                table: "Reservations",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
