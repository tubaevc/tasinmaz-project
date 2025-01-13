using Microsoft.EntityFrameworkCore.Migrations;

namespace TasinmazProject.Migrations
{
    public partial class AddForeignKeyForLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Users_userId",
                table: "Logs");

            migrationBuilder.AlterColumn<int>(
                name: "userId",
                table: "Logs",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Users_userId",
                table: "Logs",
                column: "userId",
                principalTable: "Users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Logs_Users_userId",
                table: "Logs");

            migrationBuilder.AlterColumn<int>(
                name: "userId",
                table: "Logs",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Logs_Users_userId",
                table: "Logs",
                column: "userId",
                principalTable: "Users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
