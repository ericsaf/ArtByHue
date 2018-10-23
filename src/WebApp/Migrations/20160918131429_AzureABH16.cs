using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class AzureABH16 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WebReferenceImage_Image_PictureImageID",
                table: "WebReferenceImage");

            migrationBuilder.DropIndex(
                name: "IX_WebReferenceImage_PictureImageID",
                table: "WebReferenceImage");

            migrationBuilder.DropColumn(
                name: "PictureImageID",
                table: "WebReferenceImage");

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "WebReferenceImage",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "WebReferences",
                nullable: false);

            migrationBuilder.RenameColumn(
                name: "EntryDate",
                table: "WebReferences",
                newName: "EntryDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "WebReferenceImage");

            migrationBuilder.AddColumn<int>(
                name: "PictureImageID",
                table: "WebReferenceImage",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WebReferenceImage_PictureImageID",
                table: "WebReferenceImage",
                column: "PictureImageID");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "WebReferences",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WebReferenceImage_Image_PictureImageID",
                table: "WebReferenceImage",
                column: "PictureImageID",
                principalTable: "Image",
                principalColumn: "ImageID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.RenameColumn(
                name: "EntryDate",
                table: "WebReferences",
                newName: "EntryDate");
        }
    }
}
