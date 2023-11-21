using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace El_Muro.Migrations
{
    public partial class SegundaMigracion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mensajes_Usuarios_UsuarioId",
                table: "Mensajes");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Mensajes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Mensajes_Usuarios_UsuarioId",
                table: "Mensajes",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Mensajes_Usuarios_UsuarioId",
                table: "Mensajes");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Mensajes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Mensajes_Usuarios_UsuarioId",
                table: "Mensajes",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId");
        }
    }
}
