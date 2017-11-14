using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DFlow.Budget.Migrations
{
    public partial class BaseAmountsMigration_BudgetDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                schema: "Budget",
                table: "BudgetItems");

            migrationBuilder.AddColumn<decimal>(
                name: "BaseAmount",
                schema: "Budget",
                table: "BudgetItems",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Percent",
                schema: "Budget",
                table: "BudgetItems",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "BaseTotal",
                schema: "Budget",
                table: "BudgetClasses",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BaseAmount",
                schema: "Budget",
                table: "BudgetItems");

            migrationBuilder.DropColumn(
                name: "Percent",
                schema: "Budget",
                table: "BudgetItems");

            migrationBuilder.DropColumn(
                name: "BaseTotal",
                schema: "Budget",
                table: "BudgetClasses");

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                schema: "Budget",
                table: "BudgetItems",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
