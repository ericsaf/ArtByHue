using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using WebApp.Models;

namespace WebApp.Migrations
{
    [DbContext(typeof(ArtByHueContext))]
    [Migration("20160522232134_AzureABH3")]
    partial class AzureABH3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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

            modelBuilder.Entity("WebApp.Models.Image", b =>
                {
                    b.Property<int>("ImageID")
                        .ValueGeneratedOnAdd();

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

                    b.Property<bool>("IndependentArtist");

                    b.Property<decimal>("Price");

                    b.Property<int>("SupplierID");

                    b.Property<string>("Title");

                    b.HasKey("ProductID");

                    b.HasIndex("ArtistID");

                    b.HasIndex("SupplierID");

                    b.ToTable("Products");
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

            modelBuilder.Entity("WebApp.Models.Color", b =>
                {
                    b.HasOne("WebApp.Models.Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApp.Models.Image", b =>
                {
                    b.HasOne("WebApp.Models.Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApp.Models.Product", b =>
                {
                    b.HasOne("WebApp.Models.Artist")
                        .WithMany()
                        .HasForeignKey("ArtistID");

                    b.HasOne("WebApp.Models.Supplier")
                        .WithMany()
                        .HasForeignKey("SupplierID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("WebApp.Models.ProductTag", b =>
                {
                    b.HasOne("WebApp.Models.Product")
                        .WithMany()
                        .HasForeignKey("ProductID")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("WebApp.Models.Tag")
                        .WithMany()
                        .HasForeignKey("TagID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
