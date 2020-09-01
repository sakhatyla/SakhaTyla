using Microsoft.EntityFrameworkCore.Migrations;

namespace SakhaTyla.Data.Migrations
{
    public partial class RestrictFileGroupDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_FileGroups_GroupId",
                table: "Files");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_FileGroups_GroupId",
                table: "Files",
                column: "GroupId",
                principalTable: "FileGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_FileGroups_GroupId",
                table: "Files");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_FileGroups_GroupId",
                table: "Files",
                column: "GroupId",
                principalTable: "FileGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
