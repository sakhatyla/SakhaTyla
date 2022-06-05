using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SakhaTyla.Data.Migrations
{
    public partial class AddPageCommentContainer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentContainerId",
                table: "Pages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pages_CommentContainerId",
                table: "Pages",
                column: "CommentContainerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pages_CommentContainers_CommentContainerId",
                table: "Pages",
                column: "CommentContainerId",
                principalTable: "CommentContainers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pages_CommentContainers_CommentContainerId",
                table: "Pages");

            migrationBuilder.DropIndex(
                name: "IX_Pages_CommentContainerId",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "CommentContainerId",
                table: "Pages");
        }
    }
}
