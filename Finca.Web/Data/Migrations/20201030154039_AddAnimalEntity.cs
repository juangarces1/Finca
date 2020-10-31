using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Finca.Web.Data.Migrations
{
    public partial class AddAnimalEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    FechaNacimiento = table.Column<DateTime>(nullable: false),
                    PesoNacimiento = table.Column<decimal>(nullable: false),
                    Padre = table.Column<int>(nullable: false),
                    Madre = table.Column<int>(nullable: false),
                    NumeroFinca = table.Column<int>(nullable: false),
                    MarcaInterno = table.Column<int>(nullable: false),
                    Tatuaje = table.Column<string>(nullable: true),
                    Muesca = table.Column<string>(nullable: true),
                    Chapeta = table.Column<string>(nullable: true),
                    Arete = table.Column<string>(nullable: true),
                    Marca = table.Column<string>(nullable: true),
                    Chip = table.Column<string>(nullable: true),
                    Observaciones = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animals");
        }
    }
}
