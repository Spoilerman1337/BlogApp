using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlogApp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class votecommentsupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoteComment_Comments_CommentId",
                table: "VoteComment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VoteComment",
                table: "VoteComment");

            migrationBuilder.RenameTable(
                name: "VoteComment",
                newName: "VoteComments");

            migrationBuilder.RenameIndex(
                name: "IX_VoteComment_CommentId",
                table: "VoteComments",
                newName: "IX_VoteComments_CommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VoteComments",
                table: "VoteComments",
                columns: new[] { "UserId", "CommentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_VoteComments_Comments_CommentId",
                table: "VoteComments",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoteComments_Comments_CommentId",
                table: "VoteComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VoteComments",
                table: "VoteComments");

            migrationBuilder.RenameTable(
                name: "VoteComments",
                newName: "VoteComment");

            migrationBuilder.RenameIndex(
                name: "IX_VoteComments_CommentId",
                table: "VoteComment",
                newName: "IX_VoteComment_CommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_VoteComment",
                table: "VoteComment",
                columns: new[] { "UserId", "CommentId" });

            migrationBuilder.AddForeignKey(
                name: "FK_VoteComment_Comments_CommentId",
                table: "VoteComment",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
