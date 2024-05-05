using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class commentchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogsHistories_Blogs_blogFKId",
                table: "BlogsHistories");

            migrationBuilder.DropIndex(
                name: "IX_BlogsHistories_blogFKId",
                table: "BlogsHistories");

            migrationBuilder.DropColumn(
                name: "blogFKId",
                table: "BlogsHistories");

            migrationBuilder.CreateTable(
                name: "CommentHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CommentContentPrevious = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommentCreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CommwntModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Comments = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentHistories_Comments_Comments",
                        column: x => x.Comments,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlogsHistories_Blog",
                table: "BlogsHistories",
                column: "Blog");

            migrationBuilder.CreateIndex(
                name: "IX_CommentHistories_Comments",
                table: "CommentHistories",
                column: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogsHistories_Blogs_Blog",
                table: "BlogsHistories",
                column: "Blog",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlogsHistories_Blogs_Blog",
                table: "BlogsHistories");

            migrationBuilder.DropTable(
                name: "CommentHistories");

            migrationBuilder.DropIndex(
                name: "IX_BlogsHistories_Blog",
                table: "BlogsHistories");

            migrationBuilder.AddColumn<Guid>(
                name: "blogFKId",
                table: "BlogsHistories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlogsHistories_blogFKId",
                table: "BlogsHistories",
                column: "blogFKId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlogsHistories_Blogs_blogFKId",
                table: "BlogsHistories",
                column: "blogFKId",
                principalTable: "Blogs",
                principalColumn: "Id");
        }
    }
}
