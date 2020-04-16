using Microsoft.EntityFrameworkCore.Migrations;

namespace Readdit.Infrastructure.Migrations
{
    public partial class addSubReadditsToDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Links_SubReaddit_SubReadditId",
                table: "Links");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubReaddit",
                table: "SubReaddit");

            migrationBuilder.RenameTable(
                name: "SubReaddit",
                newName: "SubReaddits");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubReaddits",
                table: "SubReaddits",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Links_SubReaddits_SubReadditId",
                table: "Links",
                column: "SubReadditId",
                principalTable: "SubReaddits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Links_SubReaddits_SubReadditId",
                table: "Links");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SubReaddits",
                table: "SubReaddits");

            migrationBuilder.RenameTable(
                name: "SubReaddits",
                newName: "SubReaddit");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SubReaddit",
                table: "SubReaddit",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Links_SubReaddit_SubReadditId",
                table: "Links",
                column: "SubReadditId",
                principalTable: "SubReaddit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
