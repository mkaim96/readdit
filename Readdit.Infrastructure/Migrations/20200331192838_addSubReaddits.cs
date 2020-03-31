using Microsoft.EntityFrameworkCore.Migrations;

namespace Readdit.Infrastructure.Migrations
{
    public partial class addSubReaddits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubReadditId",
                table: "Links",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SubReaddit",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubReaddit", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Links_SubReadditId",
                table: "Links",
                column: "SubReadditId");

            migrationBuilder.AddForeignKey(
                name: "FK_Links_SubReaddit_SubReadditId",
                table: "Links",
                column: "SubReadditId",
                principalTable: "SubReaddit",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Links_SubReaddit_SubReadditId",
                table: "Links");

            migrationBuilder.DropTable(
                name: "SubReaddit");

            migrationBuilder.DropIndex(
                name: "IX_Links_SubReadditId",
                table: "Links");

            migrationBuilder.DropColumn(
                name: "SubReadditId",
                table: "Links");
        }
    }
}
