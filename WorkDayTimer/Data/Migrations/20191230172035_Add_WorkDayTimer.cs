using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WorkDayTimerWebApp.Data.Migrations
{
    public partial class Add_WorkDayTimer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkDayTimers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    WorkDayHours = table.Column<int>(nullable: false),
                    TimerTime = table.Column<DateTime>(nullable: false),
                    IsRunning = table.Column<bool>(nullable: false),
                    MaxTimerCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkDayTimers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkDayTimers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkDayTimers_UserId",
                table: "WorkDayTimers",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkDayTimers");
        }
    }
}
