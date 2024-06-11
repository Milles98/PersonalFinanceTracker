using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinanceTracker.Migrations
{
    /// <inheritdoc />
    public partial class updatedmodel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncomeExpenseEntries_Categories_CategoryId",
                table: "IncomeExpenseEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IncomeExpenseEntries",
                table: "IncomeExpenseEntries");

            migrationBuilder.RenameTable(
                name: "IncomeExpenseEntries",
                newName: "IncomeEntries");

            migrationBuilder.RenameIndex(
                name: "IX_IncomeExpenseEntries_CategoryId",
                table: "IncomeEntries",
                newName: "IX_IncomeEntries_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IncomeEntries",
                table: "IncomeEntries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeEntries_Categories_CategoryId",
                table: "IncomeEntries",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncomeEntries_Categories_CategoryId",
                table: "IncomeEntries");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IncomeEntries",
                table: "IncomeEntries");

            migrationBuilder.RenameTable(
                name: "IncomeEntries",
                newName: "IncomeExpenseEntries");

            migrationBuilder.RenameIndex(
                name: "IX_IncomeEntries_CategoryId",
                table: "IncomeExpenseEntries",
                newName: "IX_IncomeExpenseEntries_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IncomeExpenseEntries",
                table: "IncomeExpenseEntries",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeExpenseEntries_Categories_CategoryId",
                table: "IncomeExpenseEntries",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
