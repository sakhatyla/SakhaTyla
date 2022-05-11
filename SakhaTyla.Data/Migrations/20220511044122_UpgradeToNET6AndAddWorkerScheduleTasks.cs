using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SakhaTyla.Data.Migrations
{
    public partial class UpgradeToNET6AndAddWorkerScheduleTasks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Keys",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Use = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Algorithm = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsX509Certificate = table.Column<bool>(type: "bit", nullable: false),
                    DataProtected = table.Column<bool>(type: "bit", nullable: false),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Keys", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkerScheduleTasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WorkerInfoId = table.Column<int>(type: "int", nullable: false),
                    Seconds = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Minutes = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Hours = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DayOfMonth = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Month = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    DayOfWeek = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Year = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationUserId = table.Column<int>(type: "int", nullable: true),
                    ModificationUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerScheduleTasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkerScheduleTasks_AspNetUsers_CreationUserId",
                        column: x => x.CreationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkerScheduleTasks_AspNetUsers_ModificationUserId",
                        column: x => x.ModificationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkerScheduleTasks_WorkerInfos_WorkerInfoId",
                        column: x => x.WorkerInfoId,
                        principalTable: "WorkerInfos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PersistedGrants_ConsumedTime",
                table: "PersistedGrants",
                column: "ConsumedTime");

            migrationBuilder.CreateIndex(
                name: "IX_Keys_Use",
                table: "Keys",
                column: "Use");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerScheduleTasks_CreationUserId",
                table: "WorkerScheduleTasks",
                column: "CreationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerScheduleTasks_ModificationUserId",
                table: "WorkerScheduleTasks",
                column: "ModificationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerScheduleTasks_WorkerInfoId",
                table: "WorkerScheduleTasks",
                column: "WorkerInfoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Keys");

            migrationBuilder.DropTable(
                name: "WorkerScheduleTasks");

            migrationBuilder.DropIndex(
                name: "IX_PersistedGrants_ConsumedTime",
                table: "PersistedGrants");
        }
    }
}
