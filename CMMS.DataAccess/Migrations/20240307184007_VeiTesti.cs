using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CMMS.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class VeiTesti : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseDate",
                table: "Materials",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "MaintenanceTasks",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsPlanned",
                table: "MaintenanceTasks",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Employees",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchaseDate",
                table: "Materials");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "MaintenanceTasks");

            migrationBuilder.DropColumn(
                name: "IsPlanned",
                table: "MaintenanceTasks");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Employees");
        }
    }
}
