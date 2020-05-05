using Microsoft.EntityFrameworkCore.Migrations;

namespace Readdit.Infrastructure.Migrations
{
    public partial class AddVotesToLink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Links_LinkId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Links_LinkId",
                table: "Votes");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Links_LinkId",
                table: "Comments",
                column: "LinkId",
                principalTable: "Links",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Links_LinkId",
                table: "Votes",
                column: "LinkId",
                principalTable: "Links",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Links_LinkId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Links_LinkId",
                table: "Votes");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Links_LinkId",
                table: "Comments",
                column: "LinkId",
                principalTable: "Links",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Links_LinkId",
                table: "Votes",
                column: "LinkId",
                principalTable: "Links",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
