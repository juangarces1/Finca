using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Finca.Web.Migrations
{
    public partial class Cambios : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Veterinario",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Direccion = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veterinario", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Palpation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Prenez = table.Column<TimeSpan>(nullable: false),
                    Peso = table.Column<decimal>(nullable: false),
                    AnimalId = table.Column<int>(nullable: true),
                    VeterinarioId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Palpation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Palpation_Animals_AnimalId",
                        column: x => x.AnimalId,
                        principalTable: "Animals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Palpation_Veterinario_VeterinarioId",
                        column: x => x.VeterinarioId,
                        principalTable: "Veterinario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Palpation_AnimalId",
                table: "Palpation",
                column: "AnimalId");

            migrationBuilder.CreateIndex(
                name: "IX_Palpation_VeterinarioId",
                table: "Palpation",
                column: "VeterinarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Palpation");

            migrationBuilder.DropTable(
                name: "Veterinario");
        }
    }
}
