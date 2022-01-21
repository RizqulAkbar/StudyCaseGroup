using Microsoft.EntityFrameworkCore.Migrations;

namespace DriverService.Migrations
{
    public partial class Update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "PricePerKm",
                table: "Prices",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<float>(
                name: "Price",
                table: "Orders",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "PricePerKm",
                table: "Prices",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AlterColumn<double>(
                name: "Price",
                table: "Orders",
                type: "float",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
