using System.Collections.Generic;
using Amadeus.Server.Models;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Amadeus.Server.Migrations
{
    public partial class jwt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Dictionary<string, JwtToken>>(
                name: "ExternalTokens",
                table: "Users",
                type: "jsonb",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalTokens",
                table: "Users");
        }
    }
}
