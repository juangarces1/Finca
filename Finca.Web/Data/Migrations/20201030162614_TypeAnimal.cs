using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Finca.Web.Data.Migrations
{
    public partial class TypeAnimal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TypeAnimalId",
                table: "Animals",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TypeAnimalEntity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeAnimalEntity", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animals_TypeAnimalId",
                table: "Animals",
                column: "TypeAnimalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_TypeAnimalEntity_TypeAnimalId",
                table: "Animals",
                column: "TypeAnimalId",
                principalTable: "TypeAnimalEntity",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_TypeAnimalEntity_TypeAnimalId",
                table: "Animals");

            migrationBuilder.DropTable(
                name: "TypeAnimalEntity");

            migrationBuilder.DropIndex(
                name: "IX_Animals_TypeAnimalId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "TypeAnimalId",
                table: "Animals");
        }
    }
}
