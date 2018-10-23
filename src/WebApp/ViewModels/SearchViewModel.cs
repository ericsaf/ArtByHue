using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class SearchViewModel
    {
        public string SearchColor { get; set; }
        public string ColorName { get; set; }
        public string SearchKeywords { get; set; }
        public int ArtistID { get; set; }
        public int CurrentPage { get; set; }
        public long? FoundCount { get; set; }
        public List<ProductSearchResult> Results { get; set; }

    }
}
