using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rumo.Migrations
{
    /// <inheritdoc />
    public partial class UniqueColunm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Renavam",
                table: "Vehicles",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Chassis",
                table: "Vehicles",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Chassis",
                table: "Vehicles",
                column: "Chassis",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_Renavam",
                table: "Vehicles",
                column: "Renavam",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vehicles_Chassis",
                table: "Vehicles");

            migrationBuilder.DropIndex(
                name: "IX_Vehicles_Renavam",
                table: "Vehicles");

            migrationBuilder.AlterColumn<string>(
                name: "Renavam",
                table: "Vehicles",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Chassis",
                table: "Vehicles",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);
        }
    }
}
