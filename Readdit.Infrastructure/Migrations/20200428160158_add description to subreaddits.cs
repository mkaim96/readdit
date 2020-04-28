using Microsoft.EntityFrameworkCore.Migrations;

namespace Readdit.Infrastructure.Migrations
{
    public partial class adddescriptiontosubreaddits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "SubReaddits",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "SubReaddits");
        }
    }
}
