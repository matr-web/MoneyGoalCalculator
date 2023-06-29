using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyGoalCalculator.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Calculations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MoneyAmount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MoneyInstallment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CalculationsDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calculations", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calculations");
        }
    }
}
