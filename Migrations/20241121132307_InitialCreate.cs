using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rumo.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Plate = table.Column<string>(type: "text", nullable: false),
                    Renavam = table.Column<string>(type: "text", nullable: false),
                    Chassis = table.Column<string>(type: "text", nullable: false),
                    Exercice = table.Column<int>(type: "integer", nullable: false),
                    Mark = table.Column<string>(type: "text", nullable: false),
                    Version = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false),
                    DuoDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Situation = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Plate);
                });

            migrationBuilder.CreateTable(
                name: "Aets",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    Date = table.Column<DateOnly>(type: "date", nullable: false),
                    VehicleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aets", x => x.id);
                    table.ForeignKey(
                        name: "FK_Aets_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Plate",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Versers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    AetId = table.Column<Guid>(type: "uuid", nullable: false),
                    VehicleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Versers", x => x.id);
                    table.ForeignKey(
                        name: "FK_Versers_Aets_AetId",
                        column: x => x.AetId,
                        principalTable: "Aets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Versers_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Plate",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aets_VehicleId",
                table: "Aets",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Versers_AetId",
                table: "Versers",
                column: "AetId");

            migrationBuilder.CreateIndex(
                name: "IX_Versers_VehicleId",
                table: "Versers",
                column: "VehicleId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Versers");

            migrationBuilder.DropTable(
                name: "Aets");

            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
