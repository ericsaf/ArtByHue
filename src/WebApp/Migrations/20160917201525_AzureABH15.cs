using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApp.Migrations
{
    public partial class AzureABH15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WebReferences",
                columns: table => new
                {
                    WebReferenceID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Comments = table.Column<string>(nullable: true),
                    EntryDate = table.Column<string>(nullable: true),
                    HatTipLink = table.Column<string>(nullable: true),
                    HatTipName = table.Column<string>(nullable: true),
                    HatTipSourceLink = table.Column<string>(nullable: true),
                    HatTipSourceName = table.Column<string>(nullable: true),
                    SourceLink = table.Column<string>(nullable: true),
                    SourceViaText = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebReferences", x => x.WebReferenceID);
                });

            migrationBuilder.CreateTable(
                name: "WebReferenceImage",
                columns: table => new
                {
                    WebReferenceImageID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AltText = table.Column<string>(nullable: true),
                    PictureImageID = table.Column<int>(nullable: true),
                    WebReferenceID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebReferenceImage", x => x.WebReferenceImageID);
                    table.ForeignKey(
                        name: "FK_WebReferenceImage_Image_PictureImageID",
                        column: x => x.PictureImageID,
                        principalTable: "Image",
                        principalColumn: "ImageID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WebReferenceImage_WebReferences_WebReferenceID",
                        column: x => x.WebReferenceID,
                        principalTable: "WebReferences",
                        principalColumn: "WebReferenceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WebReferenceImage_PictureImageID",
                table: "WebReferenceImage",
                column: "PictureImageID");

            migrationBuilder.CreateIndex(
                name: "IX_WebReferenceImage_WebReferenceID",
                table: "WebReferenceImage",
                column: "WebReferenceID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebReferenceImage");

            migrationBuilder.DropTable(
                name: "WebReferences");
        }
    }
}
