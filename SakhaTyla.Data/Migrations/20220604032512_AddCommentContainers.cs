using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SakhaTyla.Data.Migrations
{
    public partial class AddCommentContainers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CommentContainers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentCount = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationUserId = table.Column<int>(type: "int", nullable: true),
                    ModificationUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentContainers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentContainers_AspNetUsers_CreationUserId",
                        column: x => x.CreationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CommentContainers_AspNetUsers_ModificationUserId",
                        column: x => x.ModificationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentContainers_CreationUserId",
                table: "CommentContainers",
                column: "CreationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentContainers_ModificationUserId",
                table: "CommentContainers",
                column: "ModificationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentContainers");
        }
    }
}
