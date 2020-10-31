using Microsoft.EntityFrameworkCore.Migrations;

namespace Finca.Web.Data.Migrations
{
    public partial class Cambios3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_TypeAnimals_TypeAnimalId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "TypeID",
                table: "Animals");

            migrationBuilder.AlterColumn<int>(
                name: "TypeAnimalId",
                table: "Animals",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_TypeAnimals_TypeAnimalId",
                table: "Animals",
                column: "TypeAnimalId",
                principalTable: "TypeAnimals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_TypeAnimals_TypeAnimalId",
                table: "Animals");

            migrationBuilder.AlterColumn<int>(
                name: "TypeAnimalId",
                table: "Animals",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "TypeID",
                table: "Animals",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_TypeAnimals_TypeAnimalId",
                table: "Animals",
                column: "TypeAnimalId",
                principalTable: "TypeAnimals",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
