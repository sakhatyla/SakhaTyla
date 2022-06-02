using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SakhaTyla.Data.Migrations
{
    public partial class AddBookPages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookPages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreationUserId = table.Column<int>(type: "int", nullable: true),
                    ModificationUserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookPages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BookPages_AspNetUsers_CreationUserId",
                        column: x => x.CreationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookPages_AspNetUsers_ModificationUserId",
                        column: x => x.ModificationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BookPages_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookPages_BookId",
                table: "BookPages",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookPages_CreationUserId",
                table: "BookPages",
                column: "CreationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookPages_ModificationUserId",
                table: "BookPages",
                column: "ModificationUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookPages");
        }
    }
}
