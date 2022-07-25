using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Persistence.Migrations
{
    public partial class FixDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Turfs_Users_OwnerId",
                table: "Turfs");

            migrationBuilder.DropTable(
                name: "TurfImages");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Turfs");

            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Turfs");

            migrationBuilder.DropColumn(
                name: "Longitude",
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

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Schedules",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "End",
                table: "Schedules",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Start",
                table: "Schedules",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<Guid>(
                name: "TurfId",
                table: "Images",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "MainTurfs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Address = table.Column<string>(type: "text", nullable: true),
                    OwnerId = table.Column<Guid>(type: "uuid", nullable: false),
                    Longitude = table.Column<string>(type: "text", nullable: true),
                    Latitude = table.Column<string>(type: "text", nullable: true),
                    Created = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    Updated = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainTurfs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MainTurfs_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_CustomerId",
                table: "Schedules",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_TurfId",
                table: "Images",
                column: "TurfId");

            migrationBuilder.CreateIndex(
                name: "IX_MainTurfs_OwnerId",
                table: "MainTurfs",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Turfs_TurfId",
                table: "Images",
                column: "TurfId",
                principalTable: "Turfs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Users_CustomerId",
                table: "Schedules",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Turfs_MainTurfs_MainTurfId",
                table: "Turfs",
                column: "MainTurfId",
                principalTable: "MainTurfs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Turfs_TurfId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Users_CustomerId",
                table: "Schedules");

            migrationBuilder.DropForeignKey(
                name: "FK_Turfs_MainTurfs_MainTurfId",
                table: "Turfs");

            migrationBuilder.DropTable(
                name: "MainTurfs");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_CustomerId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Images_TurfId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "End",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "Start",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "TurfId",
                table: "Images");

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

            migrationBuilder.AddColumn<string>(
                name: "Latitude",
                table: "Turfs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Longitude",
                table: "Turfs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Turfs",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TurfImages",
                columns: table => new
                {
                    TurfId = table.Column<Guid>(type: "uuid", nullable: false),
                    ImageId = table.Column<Guid>(type: "uuid", nullable: false),
                    Created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TurfImages", x => new { x.TurfId, x.ImageId });
                    table.ForeignKey(
                        name: "FK_TurfImages_Images_ImageId",
                        column: x => x.ImageId,
                        principalTable: "Images",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TurfImages_Turfs_TurfId",
                        column: x => x.TurfId,
                        principalTable: "Turfs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TurfImages_ImageId",
                table: "TurfImages",
                column: "ImageId");

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
