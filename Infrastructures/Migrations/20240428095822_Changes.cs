using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class Changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Blogs_blogFKId",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_blogFKId",
                table: "Likes");

            migrationBuilder.DropColumn(
                name: "blogFKId",
                table: "Likes");

            migrationBuilder.CreateIndex(
                name: "IX_Likes_Blog",
                table: "Likes",
                column: "Blog");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Blogs_Blog",
                table: "Likes",
                column: "Blog",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Likes_Blogs_Blog",
                table: "Likes");

            migrationBuilder.DropIndex(
                name: "IX_Likes_Blog",
                table: "Likes");

            migrationBuilder.AddColumn<Guid>(
                name: "blogFKId",
                table: "Likes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Likes_blogFKId",
                table: "Likes",
                column: "blogFKId");

            migrationBuilder.AddForeignKey(
                name: "FK_Likes_Blogs_blogFKId",
                table: "Likes",
                column: "blogFKId",
                principalTable: "Blogs",
                principalColumn: "Id");
        }
    }
}
