using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Persistence.Migrations
{
    public partial class UpdateRefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_Users_AccountId",
                table: "RefreshToken");

            migrationBuilder.DropIndex(
                name: "IX_RefreshToken_AccountId",
                table: "RefreshToken");

            migrationBuilder.RenameColumn(
                name: "AccountId",
                table: "RefreshToken",
                newName: "ReasonRevoked");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "RefreshToken",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_AppUserId",
                table: "RefreshToken",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_Users_AppUserId",
                table: "RefreshToken",
                column: "AppUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RefreshToken_Users_AppUserId",
                table: "RefreshToken");

            migrationBuilder.DropIndex(
                name: "IX_RefreshToken_AppUserId",
                table: "RefreshToken");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "RefreshToken");

            migrationBuilder.RenameColumn(
                name: "ReasonRevoked",
                table: "RefreshToken",
                newName: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_AccountId",
                table: "RefreshToken",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_RefreshToken_Users_AccountId",
                table: "RefreshToken",
                column: "AccountId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
