using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApp.Migrations
{
    public partial class AureABH18 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WebReferenceSimilarProduct",
                columns: table => new
                {
                    WebReferenceSimilarProductID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProductID = table.Column<int>(nullable: false),
                    WebReferenceID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebReferenceSimilarProduct", x => x.WebReferenceSimilarProductID);
                    table.ForeignKey(
                        name: "FK_WebReferenceSimilarProduct_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WebReferenceSimilarProduct_WebReferences_WebReferenceID",
                        column: x => x.WebReferenceID,
                        principalTable: "WebReferences",
                        principalColumn: "WebReferenceID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WebReferenceSimilarProduct_ProductID",
                table: "WebReferenceSimilarProduct",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_WebReferenceSimilarProduct_WebReferenceID",
                table: "WebReferenceSimilarProduct",
                column: "WebReferenceID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebReferenceSimilarProduct");
        }
    }
}
