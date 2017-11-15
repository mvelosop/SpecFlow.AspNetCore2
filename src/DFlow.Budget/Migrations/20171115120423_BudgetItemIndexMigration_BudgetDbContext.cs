using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DFlow.Budget.Migrations
{
    public partial class BudgetItemIndexMigration_BudgetDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BudgetItems_BudgetClass_Id",
                schema: "Budget",
                table: "BudgetItems");

            migrationBuilder.DropIndex(
                name: "IX_BudgetItems_Name",
                schema: "Budget",
                table: "BudgetItems");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetItems_BudgetClass_Id_Name",
                schema: "Budget",
                table: "BudgetItems",
                columns: new[] { "BudgetClass_Id", "Name" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BudgetItems_BudgetClass_Id_Name",
                schema: "Budget",
                table: "BudgetItems");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetItems_BudgetClass_Id",
                schema: "Budget",
                table: "BudgetItems",
                column: "BudgetClass_Id");

            migrationBuilder.CreateIndex(
                name: "IX_BudgetItems_Name",
                schema: "Budget",
                table: "BudgetItems",
                column: "Name",
                unique: true);
        }
    }
}
