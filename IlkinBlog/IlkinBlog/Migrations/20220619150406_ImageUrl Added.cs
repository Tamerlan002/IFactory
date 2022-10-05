using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IlkinBlog.Migrations
{
    public partial class ImageUrlAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "blogs",
                newName: "ImagePath");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "blogs",
                newName: "Image");
        }
    }
}
