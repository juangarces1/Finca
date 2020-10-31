using Microsoft.EntityFrameworkCore.Migrations;

namespace Finca.Web.Data.Migrations
{
    public partial class ChangeAnimalsEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Lotes_LoteId",
                table: "Animals");

            migrationBuilder.AlterColumn<int>(
                name: "LoteId",
                table: "Animals",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeID",
                table: "Animals",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Lotes_LoteId",
                table: "Animals",
                column: "LoteId",
                principalTable: "Lotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Animals_Lotes_LoteId",
                table: "Animals");

            migrationBuilder.DropColumn(
                name: "TypeID",
                table: "Animals");

            migrationBuilder.AlterColumn<int>(
                name: "LoteId",
                table: "Animals",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Animals_Lotes_LoteId",
                table: "Animals",
                column: "LoteId",
                principalTable: "Lotes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
