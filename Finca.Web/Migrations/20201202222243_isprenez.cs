using Microsoft.EntityFrameworkCore.Migrations;

namespace Finca.Web.Migrations
{
    public partial class isprenez : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPrenez",
                table: "Animals",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrenez",
                table: "Animals");
        }
    }
}
