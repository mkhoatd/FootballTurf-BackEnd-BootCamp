using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Persistence.Migrations
{
    public partial class ChangeReactionTypeToEnum : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reactions_ReactTypes_ReactTypeId",
                table: "Reactions");

            migrationBuilder.DropTable(
                name: "ReactTypes");

            migrationBuilder.DropIndex(
                name: "IX_Reactions_ReactTypeId",
                table: "Reactions");

            migrationBuilder.DropColumn(
                name: "ReactTypeId",
                table: "Reactions");

            migrationBuilder.RenameColumn(
                name: "updatedAt",
                table: "Users",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "Users",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "updatedAt",
                table: "Reactions",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "Reactions",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "updatedAt",
                table: "Questions",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "Questions",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "updatedAt",
                table: "Games",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "Games",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "updatedAt",
                table: "Answers",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "createdAt",
                table: "Answers",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<int>(
                name: "ReactionTypeId",
                table: "Reactions",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReactionTypeId",
                table: "Reactions");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Users",
                newName: "updatedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Users",
                newName: "createdAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Reactions",
                newName: "updatedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Reactions",
                newName: "createdAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Questions",
                newName: "updatedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Questions",
                newName: "createdAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Games",
                newName: "updatedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Games",
                newName: "createdAt");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Answers",
                newName: "updatedAt");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Answers",
                newName: "createdAt");

            migrationBuilder.AddColumn<string>(
                name: "ReactTypeId",
                table: "Reactions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ReactTypes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Weight = table.Column<float>(type: "real", nullable: false),
                    createdAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReactTypes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reactions_ReactTypeId",
                table: "Reactions",
                column: "ReactTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reactions_ReactTypes_ReactTypeId",
                table: "Reactions",
                column: "ReactTypeId",
                principalTable: "ReactTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
