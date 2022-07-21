using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Persistence.Migrations
{
    public partial class UpdateUserReactionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reactions_Answers_AnswerId",
                table: "Reactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Reactions_Users_UserId",
                table: "Reactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reactions",
                table: "Reactions");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Reactions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "ReactedAnswerId",
                table: "Reactions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "SourceUserId",
                table: "Reactions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reactions",
                table: "Reactions",
                columns: new[] { "ReactedAnswerId", "SourceUserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Reactions_Answers_ReactedAnswerId",
                table: "Reactions",
                column: "ReactedAnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reactions_Users_UserId",
                table: "Reactions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reactions_Answers_ReactedAnswerId",
                table: "Reactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Reactions_Users_UserId",
                table: "Reactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reactions",
                table: "Reactions");

            migrationBuilder.DropColumn(
                name: "ReactedAnswerId",
                table: "Reactions");

            migrationBuilder.DropColumn(
                name: "SourceUserId",
                table: "Reactions");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Reactions",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reactions",
                table: "Reactions",
                columns: new[] { "AnswerId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Reactions_Answers_AnswerId",
                table: "Reactions",
                column: "AnswerId",
                principalTable: "Answers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reactions_Users_UserId",
                table: "Reactions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
