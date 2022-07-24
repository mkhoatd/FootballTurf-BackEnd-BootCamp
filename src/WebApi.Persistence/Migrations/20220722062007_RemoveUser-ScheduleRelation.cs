using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Persistence.Migrations
{
    public partial class RemoveUserScheduleRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedules_Users_CustomerId",
                table: "Schedules");

            migrationBuilder.DropIndex(
                name: "IX_Schedules_CustomerId",
                table: "Schedules");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Schedules");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                table: "Schedules",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_CustomerId",
                table: "Schedules",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedules_Users_CustomerId",
                table: "Schedules",
                column: "CustomerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
