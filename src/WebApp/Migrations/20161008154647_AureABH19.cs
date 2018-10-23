using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApp.Migrations
{
    public partial class AureABH19 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoCreditName",
                table: "WebReferenceSimilarProduct",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoCreditURL",
                table: "WebReferenceSimilarProduct",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoCreditName",
                table: "WebReferenceSimilarProduct");

            migrationBuilder.DropColumn(
                name: "PhotoCreditURL",
                table: "WebReferenceSimilarProduct");
        }
    }
}
