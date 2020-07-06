using Microsoft.EntityFrameworkCore.Migrations;

namespace Readdit.Infrastructure.Migrations
{
    public partial class changeTableNameFormSubReadditToCommunity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Links_SubReaddits_SubReadditId",
                table: "Links");

            migrationBuilder.DropTable(
                name: "SubReaddits");

            migrationBuilder.DropIndex(
                name: "IX_Links_SubReadditId",
                table: "Links");

            migrationBuilder.DropColumn(
                name: "SubReadditId",
                table: "Links");

            migrationBuilder.AddColumn<int>(
                name: "CommunityId",
                table: "Links",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Communities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Communities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Communities_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Links_CommunityId",
                table: "Links",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_Communities_UserId",
                table: "Communities",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Links_Communities_CommunityId",
                table: "Links",
                column: "CommunityId",
                principalTable: "Communities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Links_Communities_CommunityId",
                table: "Links");

            migrationBuilder.DropTable(
                name: "Communities");

            migrationBuilder.DropIndex(
                name: "IX_Links_CommunityId",
                table: "Links");

            migrationBuilder.DropColumn(
                name: "CommunityId",
                table: "Links");

            migrationBuilder.AddColumn<int>(
                name: "SubReadditId",
                table: "Links",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SubReaddits",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubReaddits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubReaddits_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Links_SubReadditId",
                table: "Links",
                column: "SubReadditId");

            migrationBuilder.CreateIndex(
                name: "IX_SubReaddits_UserId",
                table: "SubReaddits",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Links_SubReaddits_SubReadditId",
                table: "Links",
                column: "SubReadditId",
                principalTable: "SubReaddits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
