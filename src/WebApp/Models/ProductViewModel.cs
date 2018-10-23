using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ProductViewModel
    {
        public int SearchPage { get; set; }
        public string SearchColor { get; set; }
        public string SearchKeywords { get; set; }
        public Product Product { get; set; }
        public List<ColorLookup> MatchingPaints { get; set; }
    }
}
