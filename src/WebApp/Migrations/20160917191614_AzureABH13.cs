using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebApp.Migrations
{
    public partial class AzureABH13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WebReference");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WebReference",
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
                    table.PrimaryKey("PK_WebReference", x => x.WebReferenceID);
                });
        }
    }
}
