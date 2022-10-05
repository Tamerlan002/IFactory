using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IlkinBlog.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_blogs_categories_CategoryId",
                table: "blogs");

            migrationBuilder.DropIndex(
                name: "IX_blogs_CategoryId",
                table: "blogs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_blogs_CategoryId",
                table: "blogs",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_blogs_categories_CategoryId",
                table: "blogs",
                column: "CategoryId",
                principalTable: "categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
