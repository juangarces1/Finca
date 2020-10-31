using Microsoft.EntityFrameworkCore.Migrations;

namespace Finca.Web.Data.Migrations
{
    public partial class ChangeAnimals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FotoPath",
                table: "Animals",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotoPath",
                table: "Animals");
        }
    }
}
