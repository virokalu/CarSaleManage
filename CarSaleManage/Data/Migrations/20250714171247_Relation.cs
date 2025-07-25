using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarSaleManage.Data.Migrations
{
    /// <inheritdoc />
    public partial class Relation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "Vehicle",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Images",
                table: "Vehicle",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");


            migrationBuilder.CreateIndex(
                name: "IX_Vehicle_AppUserId",
                table: "Vehicle",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehicle_AspNetUsers_AppUserId",
                table: "Vehicle",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehicle_AspNetUsers_AppUserId",
                table: "Vehicle");

            migrationBuilder.DropIndex(
                name: "IX_Vehicle_AppUserId",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Vehicle");

            migrationBuilder.DropColumn(
                name: "Images",
                table: "Vehicle");
        }
    }
}
