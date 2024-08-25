using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Meeting.Migrations
{
    public partial class CreateDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Corporates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    M_Place = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Clint = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Side = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descussion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Decion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CustId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Corporates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cutomers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cutomers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    ExperienceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CorporateId = table.Column<int>(type: "int", nullable: false),
                    Service = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantiy = table.Column<int>(type: "int", nullable: false),
                    Unit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.ExperienceId);
                    table.ForeignKey(
                        name: "FK_Experiences_Corporates_CorporateId",
                        column: x => x.CorporateId,
                        principalTable: "Corporates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_CorporateId",
                table: "Experiences",
                column: "CorporateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cutomers");

            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropTable(
                name: "Corporates");
        }
    }
}
