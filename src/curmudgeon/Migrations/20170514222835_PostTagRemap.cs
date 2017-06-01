using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace curmudgeon.Migrations
{
    public partial class PostTagRemap : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_Posts_PostId",
                table: "PostTag");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_Tags_TagId",
                table: "PostTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostTag",
                table: "PostTag");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostTags",
                table: "PostTag",
                column: "PostTagId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostTags_Posts_PostId",
                table: "PostTag",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTags_Tags_TagId",
                table: "PostTag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.RenameIndex(
                name: "IX_PostTag_TagId",
                table: "PostTag",
                newName: "IX_PostTags_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_PostTag_PostId",
                table: "PostTag",
                newName: "IX_PostTags_PostId");

            migrationBuilder.RenameTable(
                name: "PostTag",
                newName: "PostTags");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTags_Posts_PostId",
                table: "PostTags");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTags_Tags_TagId",
                table: "PostTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostTags",
                table: "PostTags");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostTag",
                table: "PostTags",
                column: "PostTagId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Posts_PostId",
                table: "PostTags",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "PostId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Tags_TagId",
                table: "PostTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "TagId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.RenameIndex(
                name: "IX_PostTags_TagId",
                table: "PostTags",
                newName: "IX_PostTag_TagId");

            migrationBuilder.RenameIndex(
                name: "IX_PostTags_PostId",
                table: "PostTags",
                newName: "IX_PostTag_PostId");

            migrationBuilder.RenameTable(
                name: "PostTags",
                newName: "PostTag");
        }
    }
}
