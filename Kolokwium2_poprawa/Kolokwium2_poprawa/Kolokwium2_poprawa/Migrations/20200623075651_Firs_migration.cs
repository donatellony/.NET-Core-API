using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Kolokwium2_poprawa.Migrations
{
    public partial class Firs_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BreedTypes",
                columns: table => new
                {
                    IdBreedType = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("BreedType_pk", x => x.IdBreedType);
                });

            migrationBuilder.CreateTable(
                name: "Volunteers",
                columns: table => new
                {
                    IdVolunteer = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdSupervisor = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 80, nullable: false),
                    Surname = table.Column<string>(maxLength: 80, nullable: false),
                    Phone = table.Column<string>(maxLength: 30, nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: false),
                    Email = table.Column<string>(maxLength: 80, nullable: false),
                    StartingDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Volunteer_pk", x => x.IdVolunteer);
                    table.ForeignKey(
                        name: "Suprevisor",
                        column: x => x.IdSupervisor,
                        principalTable: "Volunteers",
                        principalColumn: "IdVolunteer",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    IdPet = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdBreedType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 80, nullable: false),
                    IsMale = table.Column<bool>(nullable: false),
                    DateRegistered = table.Column<DateTime>(nullable: false),
                    ApprocimateDateOfBirth = table.Column<DateTime>(nullable: false),
                    DateAdopted = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Pet_pk", x => x.IdPet);
                    table.ForeignKey(
                        name: "Pet_BreedType",
                        column: x => x.IdBreedType,
                        principalTable: "BreedTypes",
                        principalColumn: "IdBreedType",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Volunteer_Pet",
                columns: table => new
                {
                    IdVolunteer = table.Column<int>(nullable: false),
                    IdPet = table.Column<int>(nullable: false),
                    DateAccepted = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Volunteer_Pet_pk", x => new { x.IdVolunteer, x.IdPet });
                    table.ForeignKey(
                        name: "Volunteer_Pet_Pet",
                        column: x => x.IdPet,
                        principalTable: "Pets",
                        principalColumn: "IdPet",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "Volunteer_Pet_Volunteer",
                        column: x => x.IdVolunteer,
                        principalTable: "Volunteers",
                        principalColumn: "IdVolunteer",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pets_IdBreedType",
                table: "Pets",
                column: "IdBreedType");

            migrationBuilder.CreateIndex(
                name: "IX_Volunteer_Pet_IdPet",
                table: "Volunteer_Pet",
                column: "IdPet");

            migrationBuilder.CreateIndex(
                name: "IX_Volunteers_IdSupervisor",
                table: "Volunteers",
                column: "IdSupervisor");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Volunteer_Pet");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "Volunteers");

            migrationBuilder.DropTable(
                name: "BreedTypes");
        }
    }
}
