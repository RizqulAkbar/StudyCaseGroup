using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PenggunaService.Migrations
{
    public partial class CreateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverId = table.Column<int>(type: "int", nullable: true),
                    PenggunaId = table.Column<int>(type: "int", nullable: false),
                    LatPengguna = table.Column<double>(type: "float", nullable: false),
                    LongPengguna = table.Column<double>(type: "float", nullable: false),
                    LatDriver = table.Column<double>(type: "float", nullable: true),
                    LongDriver = table.Column<double>(type: "float", nullable: true),
                    LatTujuan = table.Column<double>(type: "float", nullable: false),
                    LongTujuan = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<float>(type: "real", nullable: false),
                    Status = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "Penggunas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Firstname = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Lastname = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "ntext", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false),
                    isLocked = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Penggunas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PricePerKm = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "ntext", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: false),
                    isLocked = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserDriver",
                columns: table => new
                {
                    DriverId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Firstname = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Lastname = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Username = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "ntext", nullable: false),
                    LatDriver = table.Column<double>(type: "float", nullable: false),
                    LongDriver = table.Column<double>(type: "float", nullable: false),
                    Lock = table.Column<bool>(type: "bit", nullable: false),
                    Approved = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    Updated = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDriver", x => x.DriverId);
                });

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

            migrationBuilder.CreateTable(
                name: "SaldoDriver",
                columns: table => new
                {
                    SaldoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    TotalSaldo = table.Column<double>(type: "float", nullable: false),
                    MutasiSaldo = table.Column<double>(type: "float", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaldoDriver", x => x.SaldoId);
                    table.ForeignKey(
                        name: "FK_SaldoDriver_UserDriver",
                        column: x => x.DriverId,
                        principalTable: "UserDriver",
                        principalColumn: "DriverId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaldoDriver_DriverId",
                table: "SaldoDriver",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_SaldoPengguna_PenggunaId",
                table: "SaldoPengguna",
                column: "PenggunaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropTable(
                name: "SaldoDriver");

            migrationBuilder.DropTable(
                name: "SaldoPengguna");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserDriver");

            migrationBuilder.DropTable(
                name: "Penggunas");
        }
    }
}
