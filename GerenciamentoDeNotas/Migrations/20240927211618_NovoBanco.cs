using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciamentoDeNotas.Migrations
{
    /// <inheritdoc />
    public partial class NovoBanco : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "UsuarioId", "Cpf", "DataAtualizacao", "DataCadastro", "Email", "Login", "Nome", "Perfil", "Senha" },
                values: new object[] { 1, "123456789-11", null, new DateTime(2024, 9, 27, 18, 16, 17, 528, DateTimeKind.Local).AddTicks(3109), "admin@exemplo.com", "admin", "Admin Padrão", 1, "d033e22ae348aeb5660fc2140aec35850c4da997" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "UsuarioId",
                keyValue: 1);
        }
    }
}
