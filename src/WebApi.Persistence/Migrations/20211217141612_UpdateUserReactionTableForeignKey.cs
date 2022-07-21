using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Persistence.Migrations
{
    public partial class UpdateUserReactionTableForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reactions_Users_UserId",
                table: "Reactions");

            migrationBuilder.DropIndex(
                name: "IX_Reactions_UserId",
                table: "Reactions");

            migrationBuilder.DropColumn(
                name: "AnswerId",
                table: "Reactions");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Reactions");

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_SourceUserId",
                table: "Reactions",
                column: "SourceUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reactions_Users_SourceUserId",
                table: "Reactions",
                column: "SourceUserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reactions_Users_SourceUserId",
                table: "Reactions");

            migrationBuilder.DropIndex(
                name: "IX_Reactions_SourceUserId",
                table: "Reactions");

            migrationBuilder.AddColumn<string>(
                name: "AnswerId",
                table: "Reactions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Reactions",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_UserId",
                table: "Reactions",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reactions_Users_UserId",
                table: "Reactions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
