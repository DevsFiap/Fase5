using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fase5.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class CriarFuncionarioAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Funcionarios",
                columns: new[] { "Id", "Cargo", "Email", "Nome", "Perfil", "Senha" },
                values: new object[] { 2, 2, "admin@fasttech.com", "Administrador", 1, "AQAAAAIAAYagAAAAEKtcXtkr3bHKt32KXMnaCWyEFmBrXEoWj2oSNOATCjBnHpidrxtvDHWANLCXtAsyYw==" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Funcionarios",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
