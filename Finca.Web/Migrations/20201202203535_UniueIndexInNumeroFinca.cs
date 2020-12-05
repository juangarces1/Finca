using Microsoft.EntityFrameworkCore.Migrations;

namespace Finca.Web.Migrations
{
    public partial class UniueIndexInNumeroFinca : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Peso",
                table: "Pesos",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.CreateIndex(
                name: "IX_Animals_NumeroFinca",
                table: "Animals",
                column: "NumeroFinca",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Animals_NumeroFinca",
                table: "Animals");

            migrationBuilder.AlterColumn<decimal>(
                name: "Peso",
                table: "Pesos",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }
    }
}
