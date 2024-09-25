using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciamentoDeNotas.Migrations
{
    /// <inheritdoc />
    public partial class alteracaoEmNotas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TurmaEditarDto");

            migrationBuilder.RenameColumn(
                name: "Nota",
                table: "Notas",
                newName: "Trabalho");

            migrationBuilder.AddColumn<double>(
                name: "Prova1",
                table: "Notas",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Prova2",
                table: "Notas",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Prova3",
                table: "Notas",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Prova1",
                table: "Notas");

            migrationBuilder.DropColumn(
                name: "Prova2",
                table: "Notas");

            migrationBuilder.DropColumn(
                name: "Prova3",
                table: "Notas");

            migrationBuilder.RenameColumn(
                name: "Trabalho",
                table: "Notas",
                newName: "Nota");

            migrationBuilder.CreateTable(
                name: "TurmaEditarDto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataDeInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurmaEditarDto", x => x.Id);
                });
        }
    }
}
