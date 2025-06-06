using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Personal_Investment_App.Migrations
{
    /// <inheritdoc />
    public partial class MockPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "MockPrice",
                table: "Investments",
                type: "decimal(18,2)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MockPrice",
                table: "Investments");
        }
    }
}
