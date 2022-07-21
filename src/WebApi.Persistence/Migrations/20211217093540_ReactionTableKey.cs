using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Persistence.Migrations
{
    public partial class ReactionTableKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Reactions",
                table: "Reactions");

            migrationBuilder.DropIndex(
                name: "IX_Reactions_AnswerId",
                table: "Reactions");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Reactions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reactions",
                table: "Reactions",
                columns: new[] { "AnswerId", "UserId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Reactions",
                table: "Reactions");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
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
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_AnswerId",
                table: "Reactions",
                column: "AnswerId");
        }
    }
}
