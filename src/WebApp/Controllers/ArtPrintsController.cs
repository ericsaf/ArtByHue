using ColorMine.ColorSpaces;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using WebApp.Models;
using WebApp.Services;
using WebApp.Services.Utilities;

namespace WebApp.Controllers
{
    public class ArtPrintsController : Controller
    {
        private ArtByHueContext _context;
        private TelemetryClient telemetry = new TelemetryClient();

        public ArtPrintsController(ArtByHueContext context)
        {
            _context = context;
        }

        [Route("{id:int}/{name?}")]
        public IActionResult Index(int id, string name = "", int searchPage = 0, string searchColor = "", string searchKeywords = "")
        {
            var prod = _context.Products.Include(x => x.Artist).Include(x => x.Supplier).Include(x => x.Images).Include(x => x.Colors).FirstOrDefault(x => x.ProductID == id);

            if (prod == null)
            {
                return View("CustomError", "That product appears to be missing.");
            }
            var paintMatches = new List<ColorLookup>();
            var paintMatchesRGB = new List<MatchedPaint>();

            var cie = new ColorMine.ColorSpaces.Comparisons.Cie1976Comparison();
            if (prod?.Colors != null)
            {
                foreach (var color in prod.Colors)
                {
                    foreach (var paint in ColorName.PaintColors.Where(x => !x.name.ToLower().Contains("pantone")))
                    {
                        var picColorRgb = new Rgb { R = color.R, G = color.G, B = color.B };
                        var picColorLab = picColorRgb.To<Lab>();

                        var paintColor = ProductSearch.ConvertHex(paint.hex.Replace("#", ""));

                        var paintColorRgb = new Rgb { R = paintColor.R, G = paintColor.G, B = paintColor.B };
                        var paintColorLab = paintColorRgb.To<Lab>();

                        var diff = cie.Compare(picColorLab, paintColorRgb);
                        if (diff < 2.2)
                        {
                            var manf = paint.name.Substring(paint.name.IndexOf('('), paint.name.IndexOf(')') - paint.name.IndexOf('('));
                            if (!paintMatchesRGB.Exists(x => x.Manufacturer == manf && x.Name == paint.name))
                            //(x.RGB.R == paintColorRgb.R) &&
                            //(x.RGB.G == paintColorRgb.G &&
                            //(x.RGB.B == paintColorRgb.B))))
                            {
                                paintMatchesRGB.Add(new Models.MatchedPaint { Manufacturer = manf, RGB = paintColorRgb, Name = paint.name });
                                paintMatches.Add(paint);
                            }
                        }
                    }
                }

            }
            var vm = new ProductViewModel
            {
                SearchPage = searchPage,
                SearchColor = searchColor,
                SearchKeywords = searchKeywords,
                Product = prod,
                MatchingPaints = paintMatches
            };

            var title = prod.Title + " " + (prod.Artist != null && !String.IsNullOrEmpty(prod.Artist.Name) ? prod.Artist.Name : "");
            ViewBag.CanonicalURL = String.Format("{0}/{1}/{2}", Request.Scheme + "://" + Request.Host, id, Slug.Create(true, title));

            ViewBag.IsCurator = Request.Cookies["ABHCurator"] != null ? Request.Cookies["ABHCurator"] : "0";

            return View(vm);
        }

        [HttpGet]
        [Produces("application/json")]
        public ActionResult ProductDetail(int id)
        {
            var prod = _context.Products.Include(x => x.Artist).Include(x => x.Supplier).Include(x => x.Images).Include(x => x.Colors).FirstOrDefault(x => x.ProductID == id);
            if (prod == null)
            {
                return NotFound();
            }

            //circular reference messing up json
            foreach (var i in prod.Images)
            {
                i.Product = null;
            }

            foreach (var c in prod.Colors)
            {
                c.Product = null;
            }

            var result = new JsonResult(prod);
            return result;
        }

        [Route("search/{color?}/{keywords?}/{page:int?}")]
        public IActionResult Search(string color = "", string keywords = "", int page = 1, string colorName = "")
        {
            page = page == 0 ? 1 : page;

            if (color?.Trim() != "" && (keywords == null || keywords?.Trim() == ""))
            {
                return Redirect(String.Format("/color/{0}/{1}/{2}", color, Slug.Create(true, colorName), page));
            }
            else if (!String.IsNullOrEmpty(keywords) && (String.IsNullOrEmpty(color) || color.Replace("#", "").ToLower() == "ffffff"))
            {
                return RedirectToAction("Category", new { name = Slug.Create(true, keywords) });
            }
            var prods = ProductSearch.GetForCriteria(_context, page - 1, keywords, color);
            prods.ColorName = colorName;
            prods.CurrentPage = page;
            prods.SearchColor = color;
            prods.SearchKeywords = keywords;

            var newEvent = new EventTelemetry();
            newEvent.Name = "SearchColorAndKeywords";
            newEvent.Properties["color"] = color;
            newEvent.Properties["color-name"] = colorName;
            newEvent.Properties["keywords"] = keywords;
            newEvent.Properties["page"] = page.ToString();
            newEvent.Properties["result-count"] = prods.FoundCount.ToString();

            telemetry.TrackEvent(newEvent);

            if (prods.FoundCount == -1)
            {
                return View("CustomError", "Records not available");
            }

            ViewBag.IsCurator = Request.Cookies["ABHCurator"] != null ? Request.Cookies["ABHCurator"] : "0";

            return View(prods);
        }

        public PartialViewResult _NextResults(string query, int page)
        {
            var prods = ProductSearch.GetForCriteria(_context, page, "red", "C3BA97");

            return PartialView(prods);
        }

        [Route("artist/{id:int}/{name?}/{page?}")]
        public IActionResult Artist(int id, string name = "", int page = 1)
        {
            page = page == 0 ? 1 : page;
            var prods = ProductSearch.GetForArtist(_context, id, page - 1);
            prods.CurrentPage = page;
            var artistName = ProductSearch.ArtistNameLookup(_context, id);
            prods.SearchKeywords = artistName;

            var newEvent = new EventTelemetry();
            newEvent.Name = "GetArtistProducts";
            newEvent.Properties["artist-id"] = id.ToString();
            newEvent.Properties["name"] = artistName;
            newEvent.Properties["page"] = page.ToString();
            newEvent.Properties["result-count"] = prods.FoundCount.ToString();

            telemetry.TrackEvent(newEvent);

            ViewBag.IsArtist = true;
            ViewBag.CanonicalURL = String.Format("{0}/artist/{1}/{2}/{3}", Request.Scheme + "://" + Request.Host, id, Slug.Create(true, artistName), page);
            ViewBag.IsCurator = Request.Cookies["ABHCurator"] != null ? Request.Cookies["ABHCurator"] : "0";
            return View(prods);
        }

        [Route("{name}/{page:int?}")]
        public IActionResult Category(string name, int page = 1)
        {
            Char[] arr = { '!', '@' };
            name = System.Net.WebUtility.HtmlDecode(name);

            Regex regex = new Regex(@"^[A-Za-z\d_-]+$");
            Match match = regex.Match(name);
            if (!match.Success)
            {
                return new StatusCodeResult(403);
            }
            page = page == 0 ? 1 : page;
            var prods = ProductSearch.GetForCriteria(_context, page - 1, name.Replace("-", " "), "");
            prods.CurrentPage = page;
            prods.SearchKeywords = name.Replace("-", " ");

            var newEvent = new EventTelemetry();
            newEvent.Name = "KeywordOnlySearch";
            newEvent.Properties["keywords"] = name.Replace("-", " ");
            newEvent.Properties["page"] = page.ToString();
            newEvent.Properties["result-count"] = prods.FoundCount.ToString();

            telemetry.TrackEvent(newEvent);
            ViewBag.CanonicalURL = String.Format("{0}/{1}/{2}", Request.Scheme + "://" + Request.Host, name, page);
            if (prods.FoundCount == -1)
            {
                return View("CustomError", "Records not available");
            }

            ViewBag.IsCurator = Request.Cookies["ABHCurator"] != null ? Request.Cookies["ABHCurator"] : "0";

            return View(prods);
        }

        [Route("color/{colorHex}/{colorName?}/{page:int?}")]
        public IActionResult ColorOnly(string colorHex, string colorName = "", int page = 1)
        {
            page = page == 0 ? 1 : page;
            colorHex = colorHex?.Replace("#", "");
            var prods = ProductSearch.GetForCriteria(_context, page - 1, "", colorHex);
            prods.CurrentPage = page;
            prods.SearchColor = colorHex;
            prods.SearchKeywords = "";
            prods.ColorName = ColorName.SlugLookup().ContainsKey(colorName) ? ColorName.SlugLookup()[colorName] : colorName.Replace("-", " ");

            var newEvent = new EventTelemetry();
            newEvent.Name = "ColorOnlySearch";
            newEvent.Properties["color"] = colorHex;
            newEvent.Properties["color-name"] = prods.ColorName;
            newEvent.Properties["page"] = page.ToString();
            newEvent.Properties["result-count"] = prods.FoundCount.ToString();

            telemetry.TrackEvent(newEvent);

            ViewBag.CanonicalURL = String.Format("{0}/color/{1}/{2}/{3}", Request.Scheme + "://" + Request.Host, colorHex, colorName, page);
            ViewBag.IsCurator = Request.Cookies["ABHCurator"] != null ? Request.Cookies["ABHCurator"] : "0";
            return View(prods);
        }

        [HttpGet]
        [Route("suggest/{term}")]
        public ActionResult Suggest(string term)
        {
            var suggestions = ProductSearch.Suggestions(term);

            return new JsonResult(suggestions);
            //return new JsonResult
            //{
            //    JsonRequestBehavior = JsonRequestBehavior.AllowGet,
            //    Data = suggestions
            //};

        }

        public IActionResult WriteEditorCookie()
        {
            CookieOptions options = new CookieOptions();
            options.Expires = DateTime.Now.AddYears(5);
            Response.Cookies.Append("ABHCurator", "1", options);

            return Content("Cookie written successfully!");

        }

        public IActionResult SetCurated(int productID, bool curated)
        {

            using (ArtByHueContext db = _context)
            {
                var prod = db.Products.FirstOrDefault(x => x.ProductID == productID);
                if (prod != null)
                {
                    prod.CuratorPick = curated;
                    db.SaveChanges();
                }
            }

            return Json(new { Complete = true });
        }
    }
}