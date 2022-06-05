using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SakhaTyla.Data.Migrations
{
    public partial class UpdatePageCommentContainer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Pages_CommentContainerId",
                table: "Pages");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_CommentContainerId",
                table: "Pages",
                column: "CommentContainerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Pages_CommentContainerId",
                table: "Pages");

            migrationBuilder.CreateIndex(
                name: "IX_Pages_CommentContainerId",
                table: "Pages",
                column: "CommentContainerId");
        }
    }
}
