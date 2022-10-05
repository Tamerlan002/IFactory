using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IlkinBlog.Migrations
{
    public partial class DateAndRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "blogs",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_blogs_categories_CategoryId",
                table: "blogs");

            migrationBuilder.DropIndex(
                name: "IX_blogs_CategoryId",
                table: "blogs");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "blogs");
        }
    }
}
