using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarcosAraujo.Migrations
{
    /// <inheritdoc />
    public partial class Atualizacao2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Vendedors",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Clientes",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Vendedors");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Clientes");
        }
    }
}
