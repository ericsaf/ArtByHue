using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private ArtByHueContext _context;

        public HomeController(ArtByHueContext context)
        {
            _context = context;
        }
        [ResponseCache(Duration = 1800)]
        public IActionResult Index()
        {
            if (!String.IsNullOrEmpty(Request.Query["c"]))
            {
                return Redirect("color/" + Request.Query["c"].ToString() + "/custom/1");
            }
            return View();
        }
        [Route("company/about")]
        public IActionResult About()
        {
            return View();
        }

        [Route("company/terms")]
        public IActionResult Terms()
        {
            ViewData["Title"] = "Terms and Conditions";
            ViewData["PageTitle"] = "Terms and Conditions";
            ViewData["PageDesc"] = "Terms and conditions regarding the usage of this website";

            return View();
        }

        [Route("company/privacy")]
        public IActionResult Privacy()
        {
            ViewData["Title"] = "Privacy Policy";
            ViewData["PageTitle"] = "Privacy Policy";
            ViewData["PageDesc"] = "How this website views your privacy";

            return View();
        }

        [Route("articles/use-art-in-decor")]
        public IActionResult UtilizeArt()
        {
            return View();
        }

        [Route("articles/decor-inspiration")]
        public IActionResult DecorInspiration()
        {
            var refs = _context.WebReferences.Include(x => x.Pictures).Include(x => x.SimilarProducts).OrderByDescending(x => x.EntryDate).ToList();

            var prods = _context.SearchResults.FromSql(@"
                SELECT DISTINCT
                    pcs.ProductID
                    , Supplier
                    , Logo
	                , Title
	                , Price
	                , AffiliateURL
                    , IndependentArtist
	                , ArtistID
	                , Artist
	                , SmallImageURL
                    , LargeImageURL
	                , 0 AS MatchingColorDensity
                    , NULL AS MatchingTermsScore
                    , CuratorPick
                FROM 
	                ProductColorSearch pcs 
                    JOIN WebReferenceSimilarProduct p ON pcs.ProductID = p.ProductID
            ")
            .AsNoTracking()
            .ToList();

            ViewBag.SimilarProducts = prods;
            ViewBag.CanonicalURL = String.Format("{0}/{1}/{2}", Request.Scheme + "://" + Request.Host, "articles", "decor-inspiration");
            return View(refs);
        }

        [Route("categories/all")]
        public IActionResult Categories()
        {
            var categories = _context.Categories.Where(x => x.Display).OrderByDescending(x => x.Count).ToList();
            return View(categories);
        }

        //public ActionResult Index(string c)
        //{
        //    return Redirect("color/" + c + "/custom/1");
        //}

        [Route("use-art-in-decor")]
        public IActionResult UtilizeArtRedirect()
        {
            return Redirect("/articles/use-art-in-decor");
        }
        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
