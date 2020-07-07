using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectRestaurant.Data.Migrations
{
    public partial class USmigration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Menu_ProductTypeId",
                table: "Menu");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_ProductTypeId",
                table: "Menu",
                column: "ProductTypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Menu_ProductTypeId",
                table: "Menu");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_ProductTypeId",
                table: "Menu",
                column: "ProductTypeId",
                unique: true);
        }
    }
}
