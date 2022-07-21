using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Persistence.Migrations
{
    public partial class AddFieldQuestion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Result",
                table: "Questions",
                newName: "Image");

            migrationBuilder.AddColumn<string>(
                name: "AnswerA",
                table: "Questions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AnswerB",
                table: "Questions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AnswerC",
                table: "Questions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "AnswerD",
                table: "Questions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsTrueA",
                table: "Questions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTrueB",
                table: "Questions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTrueC",
                table: "Questions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsTrueD",
                table: "Questions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Point",
                table: "Questions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Time",
                table: "Questions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AnswerA",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "AnswerB",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "AnswerC",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "AnswerD",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "IsTrueA",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "IsTrueB",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "IsTrueC",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "IsTrueD",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Point",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "Questions");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Questions",
                newName: "Result");
        }
    }
}
