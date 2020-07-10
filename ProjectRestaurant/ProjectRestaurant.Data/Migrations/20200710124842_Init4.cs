using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectRestaurant.Data.Migrations
{
    public partial class Init4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "ProductType");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "ProductType",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
