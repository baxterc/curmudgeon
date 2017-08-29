using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace curmudgeon.Migrations
{
    public partial class UserColorIcon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserColors",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Posts",
                maxLength: 64,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserColors",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Posts",
                nullable: true);
        }
    }
}
