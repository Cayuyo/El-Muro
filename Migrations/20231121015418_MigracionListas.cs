using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace El_Muro.Migrations
{
    public partial class MigracionListas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Contrasena",
                table: "Usuarios",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(45)",
                oldMaxLength: 45)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<int>(
                name: "ComentarioId1",
                table: "Comentarios",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comentarios_ComentarioId1",
                table: "Comentarios",
                column: "ComentarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Comentarios_Comentarios_ComentarioId1",
                table: "Comentarios",
                column: "ComentarioId1",
                principalTable: "Comentarios",
                principalColumn: "ComentarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comentarios_Comentarios_ComentarioId1",
                table: "Comentarios");

            migrationBuilder.DropIndex(
                name: "IX_Comentarios_ComentarioId1",
                table: "Comentarios");

            migrationBuilder.DropColumn(
                name: "ComentarioId1",
                table: "Comentarios");

            migrationBuilder.AlterColumn<string>(
                name: "Contrasena",
                table: "Usuarios",
                type: "varchar(45)",
                maxLength: 45,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
