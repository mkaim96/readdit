using Microsoft.EntityFrameworkCore.Migrations;

namespace Readdit.Infrastructure.Migrations
{
    public partial class addManyToManyRelationshipBetweenUserAndCommunity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserCommunity",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    CommunityId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCommunity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCommunity_Communities_CommunityId",
                        column: x => x.CommunityId,
                        principalTable: "Communities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_UserCommunity_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserCommunity_CommunityId",
                table: "UserCommunity",
                column: "CommunityId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCommunity_UserId",
                table: "UserCommunity",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserCommunity");
        }
    }
}
