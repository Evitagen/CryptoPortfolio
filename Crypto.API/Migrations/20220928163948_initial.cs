using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Crypto.API.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CoinNames",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Coinid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoinName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinNames", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PriceHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    coinid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PriceUSD = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    priceBTC = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    priceETH = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceHistory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Portfolio",
                columns: table => new
                {
                    PortfolioID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PortfolioName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolio", x => x.PortfolioID);
                    table.ForeignKey(
                        name: "FK_Portfolio_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CoinsHodle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantity = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    coinID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PortfolioID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoinsHodle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CoinsHodle_Portfolio_PortfolioID",
                        column: x => x.PortfolioID,
                        principalTable: "Portfolio",
                        principalColumn: "PortfolioID");
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AmountBuy = table.Column<double>(type: "float", nullable: false),
                    AmountSell = table.Column<double>(type: "float", nullable: false),
                    Coinid = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    fee = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    priceWhenBoughtSold = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CoinsHodleId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_CoinsHodle_CoinsHodleId",
                        column: x => x.CoinsHodleId,
                        principalTable: "CoinsHodle",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoinsHodle_PortfolioID",
                table: "CoinsHodle",
                column: "PortfolioID");

            migrationBuilder.CreateIndex(
                name: "IX_Portfolio_UserId",
                table: "Portfolio",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CoinsHodleId",
                table: "Transactions",
                column: "CoinsHodleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoinNames");

            migrationBuilder.DropTable(
                name: "PriceHistory");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "CoinsHodle");

            migrationBuilder.DropTable(
                name: "Portfolio");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
