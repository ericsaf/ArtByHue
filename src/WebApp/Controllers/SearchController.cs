using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Models;
using WebApp.ViewModels;
using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using WebApp.Services;

namespace WebApp.Controllers
{
    public class SearchController : Controller
    {
        private ArtByHueContext _context;

        public SearchController(ArtByHueContext context)
        {
            _context = context;
        }

        public IActionResult Index(string color = "", string keywords = "", int page = 1)
        {
            var prods = ProductSearch.GetForCriteria(_context, page - 1, keywords, color);
            prods.CurrentPage = page;
            return View(prods);
        }

        public PartialViewResult _NextResults (string query, int page)
        {
            var prods = ProductSearch.GetForCriteria(_context, page, "red", "C3BA97");

            return PartialView(prods);
        }
    }
}