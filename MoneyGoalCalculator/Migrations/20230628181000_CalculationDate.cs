using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyGoalCalculator.Migrations
{
    /// <inheritdoc />
    public partial class CalculationDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CalculationsDate",
                table: "Calculations",
                newName: "CalculationDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CalculationDate",
                table: "Calculations",
                newName: "CalculationsDate");
        }
    }
}
