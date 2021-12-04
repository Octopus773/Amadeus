using Microsoft.EntityFrameworkCore.Migrations;

namespace Amadeus.Server.Migrations
{
    public partial class anilistID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AnilistID",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Users_AnilistID",
                table: "Users",
                column: "AnilistID",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_AnilistID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AnilistID",
                table: "Users");
        }
    }
}
