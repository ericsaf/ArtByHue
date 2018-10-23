using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Services.Utilities;
using WebApp.Models;
using Microsoft.AspNetCore.Http;

namespace WebApp.Models
{
    public class ArtByHueContext : DbContext
    {
        public ArtByHueContext(DbContextOptions<ArtByHueContext> options)
            : base(options)
        { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Artist> Artists { get; set; }

        public DbSet<ProductSearchResult> SearchResults { get; set; }
        public DbSet<ColorSearchMeta> ColorSearchMeta { get; set; }

        public DbSet<ArtDotComRawData> ArtDotComRawData { get; set; }

        public DbSet<WebReference> WebReferences { get; set; }

        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>()
                .HasIndex(x => new { x.Name });

            modelBuilder.Entity<Color>()
                .HasIndex(x => new { x.R, x.B, x.G });

            modelBuilder.Entity<ProductTag>()
                .HasKey(t => new { t.ProductID, t.TagID });

            modelBuilder.Entity<ProductTag>()
                .HasOne(pt => pt.Product)
                .WithMany(p => p.ProductTags)
                .HasForeignKey(pt => pt.ProductID);

        }

        public DbSet<WebReferenceImage> WebReferenceImage { get; set; }
    }

    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProductID { get; set; }
        public int SupplierID { get; set; }
        public Supplier Supplier { get; set; }
        public int? ArtistID { get; set; }
        public Artist Artist { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public string AffiliateURL { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public bool IndependentArtist { get; set; }
        public bool ImageError { get; set; }
        public bool CuratorPick { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateUpdated { get; set; }

        public List<Image> Images { get; set; }
        public List<Color> Colors { get; set; }
        public List<ProductTag> ProductTags { get; set; }

        
    }

    public class Supplier
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupplierID { get; set; }
        public string Name { get; set; }
        public string AffiliateID { get; set; }
        public string Logo { get; set; }
    }

    public class Artist
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ArtistID { get; set; }
        public string Name { get; set; }
        public string URL { get; set; }
        public string SlugURL
        {
            get
            {
                return "/artist/" + ArtistID.ToString() + "/" + Slug.Create(true, this.Name) + "/1";
            }
        }

    }

    public class Image
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ImageID { get; set; }
        public string SourceURLSmall { get; set; }
        public string SourceURLLarge { get; set; }

        public int ProductID { get; set; }
        public bool IsPrimary { get; set; }
        public Product Product { get; set; }
    }

    public class Color
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ColorID { get; set; }
        public byte R { get; set; }
        public byte B { get; set; }
        public byte G { get; set; }
        public int Density { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
    }

    public class Tag
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TagID { get; set; }
        public string Text { get; set; }
        public List<ProductTag> ProductTags { get; set; }

    }

    public class ProductTag
    {
        public int ProductID { get; set; }
        public Product Product { get; set; }

        public int TagID { get; set; }
        public Tag Tag { get; set; }
    }

    public class ArtDotComRawData
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime? lastupdated { get; set; }
        public string name { get; set; }
        public string keywords { get; set; }
        public decimal price { get; set; }
        public decimal retailprice { get; set; }
        public decimal saleprice { get; set; }
        public string buyurl { get; set; }
        public string imageurl { get; set; }

        public DateTime? DateProcessed { get; set; }
        public int? ProductID { get; set; }

    }

    public class ProductSearchResult
    {
        [Key]
        public int ProductID { get; set; }
        public string Title { get; set; }
        public string SmallImageURL { get; set; }
        public string LargeImageURL { get; set; }
        public decimal Price { get; set; }
        public string AffiliateURL { get; set; }
        public bool IndependentArtist { get; set; }
        public int? ArtistID { get; set; }
        public string Artist { get; set; }
        public string Supplier { get; set; }
        public string Logo { get; set; }
        public bool CuratorPick { get; set; }


        public int MatchingColorDensity { get; set; }
        public double? MatchingTermsScore { get; set; }

        public double TotalScore
        {
            get
            {
                return this.MatchingTermsScore.HasValue ? MatchingColorDensity * (double)MatchingTermsScore : MatchingColorDensity;
            }
        }
        private string _ArtistNameSlug { get; set; }
        public string ArtistNameSlug
        {
            get
            {
                if (String.IsNullOrEmpty(_ArtistNameSlug))
                {
                    _ArtistNameSlug = Slug.Create(true, this.Artist);
                }
                return _ArtistNameSlug;
            }
        }

        private string _ProductSlug { get; set; }
        public string ProductSlug
        {
            get
            {
                if (String.IsNullOrEmpty(_ProductSlug))
                {
                    var title = this.Title + " " + (!String.IsNullOrEmpty(this.Artist) ? this.Artist : "");
                    _ProductSlug = Slug.Create(true, title);
                }
                return _ProductSlug;
            }
        }
    }

    [NotMapped]
    public class ColorSearchMeta
    {
        [Key]
        public int ResultCount { get; set; }

    }

    public class WebReference
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WebReferenceID { get; set; }
        
        public string SourceViaText { get; set; }
        [Required]
        public string Title { get; set; }
        [Display(Name = "Source Link")]
        public string SourceLink { get; set; }
        [Display(Name = "Hat Tip")]
        public string HatTipName { get; set; }
        [Display(Name = "Hat Tip Link")]
        public string HatTipLink { get; set; }
        [Display(Name = "Hat Tip Source")]
        public string HatTipSourceName { get; set; }
        [Display(Name = "Hat Tip Source Link")]
        public string HatTipSourceLink { get; set; }
        public string Comments { get; set; }
        [Display(Name = "Entry Date")]
        [DataType(DataType.Date)]
        public DateTime EntryDate { get; set; }
        public List<WebReferenceImage> Pictures { get; set; }
        public List<WebReferenceSimilarProduct> SimilarProducts { get; set; }

        [NotMapped]
        public ICollection<IFormFile> UploadFiles { get; set; }

    }

    public class WebReferenceImage
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WebReferenceImageID { get; set; }
        public int WebReferenceID { get; set; }
        public WebReference WebReference { get; set; }
        public string AltText { get; set; }
        public string PhotoCreditURL { get; set; }
        public string PhotoCreditName { get; set; }
        public string ImageURL { get; set; }

    }

    public class WebReferenceSimilarProduct
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int WebReferenceSimilarProductID { get; set; }
        public int WebReferenceID { get; set; }
        public WebReference WebReference { get; set; }
        public int ProductID { get; set; }
        public Product Product { get; set; }
    }

    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public bool Display { get; set; }

    }
}
