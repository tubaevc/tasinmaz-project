using Microsoft.EntityFrameworkCore.Migrations;

namespace TasinmazProject.Migrations
{
    public partial class UpdateForTasinmaz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "Tasinmazlar",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Tasinmazlar_userId",
                table: "Tasinmazlar",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasinmazlar_Users_userId",
                table: "Tasinmazlar",
                column: "userId",
                principalTable: "Users",
                principalColumn: "userId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasinmazlar_Users_userId",
                table: "Tasinmazlar");

            migrationBuilder.DropIndex(
                name: "IX_Tasinmazlar_userId",
                table: "Tasinmazlar");

            migrationBuilder.DropColumn(
                name: "userId",
                table: "Tasinmazlar");
        }
    }
}
