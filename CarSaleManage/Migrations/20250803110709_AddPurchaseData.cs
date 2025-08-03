using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarSaleManage.Migrations
{
    /// <inheritdoc />
    public partial class AddPurchaseData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseDate",
                table: "Vehicle",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchaseDate",
                table: "Vehicle");
        }
    }
}
