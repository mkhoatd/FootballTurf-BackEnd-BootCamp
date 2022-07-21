using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Persistence.Migrations
{
    public partial class UpdateCreatedUpdatedPropertiesName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Reactions",
                newName: "Updated");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Reactions",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Questions",
                newName: "Updated");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Questions",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "GameUsers",
                newName: "Updated");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "GameUsers",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Games",
                newName: "Updated");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Games",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Cheats",
                newName: "Updated");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Cheats",
                newName: "Created");

            migrationBuilder.RenameColumn(
                name: "UpdatedAt",
                table: "Answers",
                newName: "Updated");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "Answers",
                newName: "Created");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Updated",
                table: "Reactions",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Reactions",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "Updated",
                table: "Questions",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Questions",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "Updated",
                table: "GameUsers",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "GameUsers",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "Updated",
                table: "Games",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Games",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "Updated",
                table: "Cheats",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Cheats",
                newName: "CreatedAt");

            migrationBuilder.RenameColumn(
                name: "Updated",
                table: "Answers",
                newName: "UpdatedAt");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Answers",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Updated",
                table: "Users",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Users",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
