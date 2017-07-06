using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace curmudgeon.Migrations
{
    public partial class DraftDateForPosts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Posts");

            migrationBuilder.AddColumn<DateTime>(
                name: "DraftDate",
                table: "Posts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishDate",
                table: "Posts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DraftDate",
                table: "Posts");

            migrationBuilder.DropColumn(
                name: "PublishDate",
                table: "Posts");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Posts",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
