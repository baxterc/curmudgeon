using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace curmudgeon.Migrations
{
    public partial class RemoveChildComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_CommentId1",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CommentId1",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CommentId1",
                table: "Comments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentId1",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentId1",
                table: "Comments",
                column: "CommentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_CommentId1",
                table: "Comments",
                column: "CommentId1",
                principalTable: "Comments",
                principalColumn: "CommentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
