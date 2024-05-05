using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class AddBlogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlogTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogContent = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    User = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LikeCount = table.Column<int>(type: "int", nullable: false),
                    DislikeCount = table.Column<int>(type: "int", nullable: false),
                    Popularity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blogs_AspNetUsers_User",
                        column: x => x.User,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BlogsHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BlogTitlePrevious = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogContentPrevious = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BlogImageNamePrevious = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BlogCreatedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BlogModifiedDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Blog = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    blogFKId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlogsHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlogsHistories_Blogs_blogFKId",
                        column: x => x.blogFKId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_User",
                table: "Blogs",
                column: "User");

            migrationBuilder.CreateIndex(
                name: "IX_BlogsHistories_blogFKId",
                table: "BlogsHistories",
                column: "blogFKId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlogsHistories");

            migrationBuilder.DropTable(
                name: "Blogs");
        }
    }
}
