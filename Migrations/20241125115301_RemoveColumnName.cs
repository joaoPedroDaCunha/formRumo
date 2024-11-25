using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Rumo.Migrations
{
    /// <inheritdoc />
    public partial class RemoveColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Exercice",
                table: "Vehicles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Exercice",
                table: "Vehicles",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
