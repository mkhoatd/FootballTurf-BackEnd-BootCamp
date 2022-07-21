using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Persistence.Migrations
{
    public partial class AddCheatCodeForUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CheatCode",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheatCode",
                table: "Users");
        }
    }
}
