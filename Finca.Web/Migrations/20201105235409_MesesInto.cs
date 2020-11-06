using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Finca.Web.Migrations
{
    public partial class MesesInto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prenez",
                table: "Palpation");

            migrationBuilder.AddColumn<int>(
                name: "Meses",
                table: "Palpation",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Meses",
                table: "Palpation");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "Prenez",
                table: "Palpation",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }
    }
}
