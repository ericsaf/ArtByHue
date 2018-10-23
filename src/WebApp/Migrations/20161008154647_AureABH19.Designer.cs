using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WebApp.Models;

namespace WebApp.Migrations
{
    [DbContext(typeof(ArtByHueContext))]
    [Migration("20161008154647_AureABH19")]
    partial class AureABH19
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApp.Models.ArtDotComRawData", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("DateProcessed");

                    b.Property<int?>("ProductID");

                    b.Property<string>("buyurl");

                    b.Property<string>("imageurl");

                    b.Property<string>("keywords");

                    b.Property<DateTime?>("lastupdated");

                    b.Property<string>("name");

                    b.Property<decimal>("price");

                    b.Property<decimal>("retailprice");

                    b.Property<decimal>("saleprice");

                    b.HasKey("ID");

                    b.ToTable("ArtDotComRawData");
                });

            modelBuilder.Entity("WebApp.Models.Artist", b =>
                {
                    b.Property<int>("ArtistID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("URL");

                    b.HasKey("ArtistID");

                    b.HasIndex("Name");

                    b.ToTable("Artists");
                });

            modelBuilder.Entity("WebApp.Models.Category", b =>
                {
                    b.Property<int>("CategoryID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Count");

                    b.Property<bool>("Display");

                    b.Property<string>("Name");

                    b.HasKey("CategoryID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("WebApp.Models.Color", b =>
                {
                    b.Property<int>("ColorID")
                        .ValueGeneratedOnAdd();

                    b.Property<byte>("B");

                    b.Property<int>("Density");

                    b.Property<byte>("G");

                    b.Property<int>("ProductID");

                    b.Property<byte>("R");

                    b.HasKey("ColorID");

                    b.HasIndex("ProductID");

                    b.HasIndex("R", "B", "G");

                    b.ToTable("Color");
                });

            modelBuilder.Entity("WebApp.Models.ColorSearchMeta", b =>
                {
                    b.Property<int>("ResultCount")
                        .ValueGeneratedOnAdd();

                    b.HasKey("ResultCount");

                    b.ToTable("ColorSearchMeta");
                });

            modelBuilder.Entity("WebApp.Models.Image", b =>
                {
                    b.Property<int>("ImageID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsPrimary");

                    b.Property<int>("ProductID");

                    b.Property<string>("SourceURLLarge");

                    b.Property<string>("SourceURLSmall");

                    b.HasKey("ImageID");

                    b.HasIndex("ProductID");

                    b.ToTable("Image");
                });

            modelBuilder.Entity("WebApp.Models.Product", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AffiliateURL");

                    b.Property<int?>("ArtistID");

                    b.Property<DateTime>("DateAdded");

                    b.Property<DateTime>("DateUpdated");

                    b.Property<string>("Description");

                    b.Property<bool>("ImageError");

                    b.Property<bool>("IndependentArtist");

                    b.Property<string>("Keywords");

                    b.Property<decimal>("Price");

                    b.Property<int>("SupplierID");

                    b.Property<string>("Title");

                    b.HasKey("ProductID");

                    b.HasIndex("ArtistID");

                    b.HasIndex("SupplierID");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("WebApp.Models.ProductSearchResult", b =>
                {
                    b.Property<int>("ProductID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AffiliateURL");

                    b.Property<string>("Artist");

                    b.Property<int?>("ArtistID");

                    b.Property<bool>("IndependentArtist");

                    b.Property<string>("LargeImageURL");

                    b.Property<string>("Logo");

                    b.Property<int>("MatchingColorDensity");

                    b.Property<double?>("MatchingTermsScore");

                    b.Property<decimal>("Price");

                    b.Property<string>("SmallImageURL");

                    b.Property<string>("Supplier");

                    b.Property<string>("Title");

                    b.HasKey("ProductID");

                    b.ToTable("SearchResults");
                });

            modelBuilder.Entity("WebApp.Models.ProductTag", b =>
                {
                    b.Property<int>("ProductID");

                    b.Property<int>("TagID");

                    b.HasKey("ProductID", "TagID");

                    b.HasIndex("ProductID");

                    b.HasIndex("TagID");

                    b.ToTable("ProductTag");
                });

            modelBuilder.Entity("WebApp.Models.Supplier", b =>
                {
                    b.Property<int>("SupplierID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AffiliateID");

                    b.Property<string>("Logo");

                    b.Property<string>("Name");

                    b.HasKey("SupplierID");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("WebApp.Models.Tag", b =>
                {
                    b.Property<int>("TagID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Text");

                    b.HasKey("TagID");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("WebApp.Models.WebReference", b =>
                {
                    b.Property<int>("WebReferenceID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Comments");

                    b.Property<DateTime>("EntryDate");

                    b.Property<string>("HatTipLink");

                    b.Property<string>("HatTipName");

                    b.Property<string>("HatTipSourceLink");

                    b.Property<string>("HatTipSourceName");

                    b.Property<string>("SourceLink");

                    b.Property<string>("SourceViaText");

                    b.Property<string>("Title")
                        .IsRequired();

                    b.HasKey("WebReferenceID");

                    b.ToTable("WebReferences");
                });

            modelBuilder.Entity("WebApp.Models.WebReferenceImage", b =>
                {
                    b.Property<int>("WebReferenceImageID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AltText");

                    b.Property<string>("ImageURL");

                    b.Property<int>("WebReferenceID");

                    b.HasKey("WebReferenceImageID");

                    b.HasIndex("WebReferenceID");

                    b.ToTable("WebReferenceImage");
                });

            modelBuilder.Entity("WebApp.Models.WebReferenceSimilarProduct", b =>
                {
                    b.Property<int>("WebReferenceSimilarProductID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PhotoCreditName");

                    b.Property<string>("PhotoCreditURL");

                    b.Property<int>("ProductID");

                    b.Property<int>("WebReferenceID");

                    b.HasKey("WebReferenceSimilarProductID");

                    b.HasIndex("ProductID");

                    b.HasIndex("WebReferenceID");

                    b.ToTable("WebReferenceSimilarProduct");
                });

            modelBuilder.Entity("WebApp.Models.Color", b =>
                {
                    b.HasOne("WebApp.Models.Product", "Product")
                        .WithMany("Colors")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApp.Models.Image", b =>
                {
                    b.HasOne("WebApp.Models.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApp.Models.Product", b =>
                {
                    b.HasOne("WebApp.Models.Artist", "Artist")
                        .WithMany()
                        .HasForeignKey("ArtistID");

                    b.HasOne("WebApp.Models.Supplier", "Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApp.Models.ProductTag", b =>
                {
                    b.HasOne("WebApp.Models.Product", "Product")
                        .WithMany("ProductTags")
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApp.Models.Tag", "Tag")
                        .WithMany("ProductTags")
                        .HasForeignKey("TagID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApp.Models.WebReferenceImage", b =>
                {
                    b.HasOne("WebApp.Models.WebReference", "WebReference")
                        .WithMany("Pictures")
                        .HasForeignKey("WebReferenceID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApp.Models.WebReferenceSimilarProduct", b =>
                {
                    b.HasOne("WebApp.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApp.Models.WebReference", "WebReference")
                        .WithMany("SimilarProducts")
                        .HasForeignKey("WebReferenceID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
