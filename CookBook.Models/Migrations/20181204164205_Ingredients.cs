using Microsoft.EntityFrameworkCore.Migrations;

namespace CookBook.Models.Migrations
{
    public partial class Ingredients : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Ingredients",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsChecked",
                table: "Ingredients",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Ingredients");

            migrationBuilder.DropColumn(
                name: "IsChecked",
                table: "Ingredients");
        }
    }
}
