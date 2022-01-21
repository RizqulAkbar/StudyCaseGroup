using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DriverService.Migrations
{
    public partial class Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Saldo");

            migrationBuilder.RenameColumn(
                name: "PenggunaID",
                table: "Orders",
                newName: "PenggunaId");

            migrationBuilder.RenameColumn(
                name: "DriverID",
                table: "Orders",
                newName: "DriverId");

            migrationBuilder.RenameColumn(
                name: "OrderID",
                table: "Orders",
                newName: "OrderId");

            migrationBuilder.AlterColumn<double>(
                name: "PricePerKm",
                table: "Prices",
                type: "float",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nchar(10)",
                oldFixedLength: true,
                oldMaxLength: 10);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Orders",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<double>(
                name: "LongDriver",
                table: "Orders",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<double>(
                name: "LatDriver",
                table: "Orders",
                type: "float",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<int>(
                name: "DriverId",
                table: "Orders",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "SaldoPengguna",
                columns: table => new
                {
                    SaldoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PenggunaId = table.Column<int>(type: "int", nullable: false),
                    TotalSaldo = table.Column<float>(type: "real", nullable: false),
                    MutasiSaldo = table.Column<float>(type: "real", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Saldo", x => x.SaldoId);
                    table.ForeignKey(
                        name: "FK_Saldo_Penggunas",
                        column: x => x.PenggunaId,
                        principalTable: "Penggunas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaldoPengguna_PenggunaId",
                table: "SaldoPengguna",
                column: "PenggunaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaldoPengguna");

            migrationBuilder.RenameColumn(
                name: "PenggunaId",
                table: "Orders",
                newName: "PenggunaID");

            migrationBuilder.RenameColumn(
                name: "DriverId",
                table: "Orders",
                newName: "DriverID");

            migrationBuilder.RenameColumn(
                name: "OrderId",
                table: "Orders",
                newName: "OrderID");

            migrationBuilder.AlterColumn<string>(
                name: "PricePerKm",
                table: "Prices",
                type: "nchar(10)",
                fixedLength: true,
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Orders",
                type: "varchar(50)",
                unicode: false,
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldUnicode: false,
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "LongDriver",
                table: "Orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "LatDriver",
                table: "Orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "DriverID",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Saldo",
                columns: table => new
                {
                    SaldoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    MutasiSaldo = table.Column<double>(type: "float", nullable: false),
                    PenggunaId = table.Column<int>(type: "int", nullable: false),
                    TotalSaldo = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Saldo", x => x.SaldoId);
                    table.ForeignKey(
                        name: "FK_Saldo_Penggunas",
                        column: x => x.PenggunaId,
                        principalTable: "Penggunas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Saldo_PenggunaId",
                table: "Saldo",
                column: "PenggunaId");
        }
    }
}
