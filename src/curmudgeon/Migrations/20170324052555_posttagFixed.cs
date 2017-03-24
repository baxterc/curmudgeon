using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace curmudgeon.Migrations
{
    public partial class posttagFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_Tags_TagId1",
                table: "PostTag");

            migrationBuilder.DropIndex(
                name: "IX_PostTag_TagId1",
                table: "PostTag");

            migrationBuilder.DropColumn(
                name: "TagId1",
                table: "PostTag");

            migrationBuilder.AlterColumn<int>(
                name: "TagId",
                table: "PostTag",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_PostTag_TagId",
                table: "PostTag",
                column: "TagId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Tags_TagId",
                table: "PostTag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_Tags_TagId",
                table: "PostTag");

            migrationBuilder.DropIndex(
                name: "IX_PostTag_TagId",
                table: "PostTag");

            migrationBuilder.AddColumn<int>(
                name: "TagId1",
                table: "PostTag",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TagId",
                table: "PostTag",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostTag_TagId1",
                table: "PostTag",
                column: "TagId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Tags_TagId1",
                table: "PostTag",
                column: "TagId1",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
