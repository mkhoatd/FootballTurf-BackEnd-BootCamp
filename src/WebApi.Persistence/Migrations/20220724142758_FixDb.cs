using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Persistence.Migrations
{
    public partial class FixDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turfs_Users_OwnerId",
                table: "Turfs");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Turfs");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Turfs");

            migrationBuilder.RenameColumn(
                name: "OwnerId",
                table: "Turfs",
                newName: "MainTurfId");

            migrationBuilder.RenameIndex(
                name: "IX_Turfs_OwnerId",
                table: "Turfs",
                newName: "IX_Turfs_MainTurfId");

            migrationBuilder.RenameColumn(
                name: "ScheduleIndex",
                table: "Schedules",
                newName: "Status");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "Schedules",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Schedules",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "MainTurf",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainTurf", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MainTurf_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MainTurf_OwnerId",
                table: "MainTurf",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Turfs_MainTurf_MainTurfId",
                table: "Turfs",
                column: "MainTurfId",
                principalTable: "MainTurf",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turfs_MainTurf_MainTurfId",
                table: "Turfs");

            migrationBuilder.DropTable(
                name: "MainTurf");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Schedules");

            migrationBuilder.RenameColumn(
                name: "MainTurfId",
                table: "Turfs",
                newName: "OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Turfs_MainTurfId",
                table: "Turfs",
                newName: "IX_Turfs_OwnerId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Schedules",
                newName: "ScheduleIndex");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Turfs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Turfs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Turfs_Users_OwnerId",
                table: "Turfs",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
