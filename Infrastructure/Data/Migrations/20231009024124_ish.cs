using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ish : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cliente_Ciudad_CiudadId",
                table: "Cliente");

            migrationBuilder.DropIndex(
                name: "IX_Cliente_CiudadId",
                table: "Cliente");

            migrationBuilder.DropColumn(
                name: "CiudadId",
                table: "Cliente");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CiudadId",
                table: "Cliente",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cliente_CiudadId",
                table: "Cliente",
                column: "CiudadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cliente_Ciudad_CiudadId",
                table: "Cliente",
                column: "CiudadId",
                principalTable: "Ciudad",
                principalColumn: "Id");
        }
    }
}
