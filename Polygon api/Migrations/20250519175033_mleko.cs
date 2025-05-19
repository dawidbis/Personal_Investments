using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Polygon_api.Migrations
{
    /// <inheritdoc />
    public partial class mleko : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlphaVantageCandles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ticker = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    O = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    H = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    L = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    C = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    V = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlphaVantageCandles", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlphaVantageCandles_Ticker_Date",
                table: "AlphaVantageCandles",
                columns: new[] { "Ticker", "Date" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlphaVantageCandles");
        }
    }
}
