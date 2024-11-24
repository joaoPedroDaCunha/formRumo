using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rumo.Migrations
{
    /// <inheritdoc />
    public partial class NomeDaMigracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Versers_Vehicles_VehicleId",
                table: "Versers");

            migrationBuilder.AlterColumn<string>(
                name: "VehicleId",
                table: "Versers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Sigla",
                table: "Aets",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddForeignKey(
                name: "FK_Versers_Vehicles_VehicleId",
                table: "Versers",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Plate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Versers_Vehicles_VehicleId",
                table: "Versers");

            migrationBuilder.AlterColumn<string>(
                name: "VehicleId",
                table: "Versers",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Sigla",
                table: "Aets",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Versers_Vehicles_VehicleId",
                table: "Versers",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Plate",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
