using Microsoft.EntityFrameworkCore.Migrations;

namespace Finca.Web.Migrations
{
    public partial class AddFieldEstadoToPalpation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Peso",
                table: "Palpation");

            migrationBuilder.AddColumn<bool>(
                name: "Estado",
                table: "Palpation",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Estado",
                table: "Palpation");

            migrationBuilder.AddColumn<decimal>(
                name: "Peso",
                table: "Palpation",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
