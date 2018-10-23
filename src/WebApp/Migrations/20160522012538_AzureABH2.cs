using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class AzureABH2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Artists_ArtistID",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "ArtistID",
                table: "Products",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Artists_ArtistID",
                table: "Products",
                column: "ArtistID",
                principalTable: "Artists",
                principalColumn: "ArtistID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Artists_ArtistID",
                table: "Products");

            migrationBuilder.AlterColumn<int>(
                name: "ArtistID",
                table: "Products",
                nullable: false);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Artists_ArtistID",
                table: "Products",
                column: "ArtistID",
                principalTable: "Artists",
                principalColumn: "ArtistID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
