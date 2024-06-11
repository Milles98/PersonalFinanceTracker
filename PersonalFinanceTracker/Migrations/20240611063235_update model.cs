using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinanceTracker.Migrations
{
    /// <inheritdoc />
    public partial class updatemodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncomeEntries_Categories_CategoryId",
                table: "IncomeEntries");

            migrationBuilder.DropIndex(
                name: "IX_IncomeEntries_CategoryId",
                table: "IncomeEntries");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "IncomeEntries");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "IncomeEntries");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "IncomeEntries",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "IncomeEntries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeEntries_CategoryId",
                table: "IncomeEntries",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeEntries_Categories_CategoryId",
                table: "IncomeEntries",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
