using Microsoft.EntityFrameworkCore.Migrations;

namespace Readdit.Infrastructure.Migrations
{
    public partial class addUserToSubReadditModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "SubReaddits",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubReaddits_UserId",
                table: "SubReaddits",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SubReaddits_AspNetUsers_UserId",
                table: "SubReaddits",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubReaddits_AspNetUsers_UserId",
                table: "SubReaddits");

            migrationBuilder.DropIndex(
                name: "IX_SubReaddits_UserId",
                table: "SubReaddits");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SubReaddits");
        }
    }
}
