using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Finca.Web.Data.Migrations
{
    public partial class changes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Animals");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeAnimalEntity",
                table: "TypeAnimalEntity");

            migrationBuilder.RenameTable(
                name: "TypeAnimalEntity",
                newName: "TypeAnimals");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeAnimals",
                table: "TypeAnimals",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Crias",
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
                    Observaciones = table.Column<string>(nullable: true),
                    FotoPath = table.Column<string>(nullable: true),
                    TypeAnimalId = table.Column<int>(nullable: true),
                    MyProperty = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Crias_TypeAnimals_TypeAnimalId",
                        column: x => x.TypeAnimalId,
                        principalTable: "TypeAnimals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VacaHorras",
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
                    Observaciones = table.Column<string>(nullable: true),
                    FotoPath = table.Column<string>(nullable: true),
                    TypeAnimalId = table.Column<int>(nullable: true),
                    nose = table.Column<int>(nullable: false),
                    MyProperty = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VacaHorras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VacaHorras_TypeAnimals_TypeAnimalId",
                        column: x => x.TypeAnimalId,
                        principalTable: "TypeAnimals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Crias_TypeAnimalId",
                table: "Crias",
                column: "TypeAnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_VacaHorras_TypeAnimalId",
                table: "VacaHorras",
                column: "TypeAnimalId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Crias");

            migrationBuilder.DropTable(
                name: "VacaHorras");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeAnimals",
                table: "TypeAnimals");

            migrationBuilder.RenameTable(
                name: "TypeAnimals",
                newName: "TypeAnimalEntity");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeAnimalEntity",
                table: "TypeAnimalEntity",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Animals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Arete = table.Column<string>(nullable: true),
                    Chapeta = table.Column<string>(nullable: true),
                    Chip = table.Column<string>(nullable: true),
                    FechaNacimiento = table.Column<DateTime>(nullable: false),
                    FotoPath = table.Column<string>(nullable: true),
                    Madre = table.Column<int>(nullable: false),
                    Marca = table.Column<string>(nullable: true),
                    MarcaInterno = table.Column<int>(nullable: false),
                    Muesca = table.Column<string>(nullable: true),
                    Nombre = table.Column<string>(nullable: true),
                    NumeroFinca = table.Column<int>(nullable: false),
                    Observaciones = table.Column<string>(nullable: true),
                    Padre = table.Column<int>(nullable: false),
                    PesoNacimiento = table.Column<decimal>(nullable: false),
                    Tatuaje = table.Column<string>(nullable: true),
                    TypeAnimalId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Animals_TypeAnimalEntity_TypeAnimalId",
                        column: x => x.TypeAnimalId,
                        principalTable: "TypeAnimalEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Animals_TypeAnimalId",
                table: "Animals",
                column: "TypeAnimalId");
        }
    }
}
