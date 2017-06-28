using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace curmudgeon.Migrations
{
    public partial class PrivateToIsPrivate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Private",
                table: "Posts");

            migrationBuilder.AddColumn<bool>(
                name: "IsPrivate",
                table: "Posts",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPrivate",
                table: "Posts");

            migrationBuilder.AddColumn<bool>(
                name: "Private",
                table: "Posts",
                nullable: false,
                defaultValue: false);
        }
    }
}
