using Microsoft.Azure.Search;
using Microsoft.Azure.Search.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Services
{
    public class ProductSearch
    {
        private static readonly string searchServiceName = "abh";
        private static readonly string apiKey = "62C020038B235A722E7F1ED597FA99CC";

        private static SearchServiceClient _serviceClient { get; set; }
        private static SearchServiceClient ServiceClient
        {
            get
            {
                if (_serviceClient == null)
                {
                    _serviceClient = new SearchServiceClient(searchServiceName, new SearchCredentials(apiKey));
                }
                return _serviceClient;
            }
        }
        private static ISearchIndexClient _indexClient { get; set; }
        private static ISearchIndexClient IndexClient
        {
            get
            {
                if (_indexClient == null)
                {
                    _indexClient = ServiceClient.Indexes.GetClient("productsearch-index");
                }
                return _indexClient;
            }
        }

        private static ISearchIndexClient _tagsClient { get; set; }
        private static ISearchIndexClient TagsClient
        {
            get
            {
                if (_tagsClient == null)
                {
                    _tagsClient = ServiceClient.Indexes.GetClient("tags-index");
                }
                return _tagsClient;
            }
        }

        public static SearchViewModel GetForCriteria(ArtByHueContext _context, int page = 0, string keywords = "", string color = "")
        {
            if (!String.IsNullOrWhiteSpace(keywords) && !String.IsNullOrWhiteSpace(color))
            {
                //both given
                var rgb = ConvertHex(color);
                return GetFullResults(_context, keywords, rgb, page);
            }
            else if (!String.IsNullOrWhiteSpace(keywords))
            {
                //keyword search only
                return GetResultsForKeywordsOnly(_context, keywords, page);
            }
            else if (!String.IsNullOrWhiteSpace(color))
            {
                //color only given
                
                return GetResultsByColor(_context, color, page);
            }

            return new SearchViewModel
            {
                SearchKeywords = keywords,
                SearchColor = color,
                CurrentPage = page,
                Results = new List<ProductSearchResult>(),
                FoundCount = 0
            };
        }

        private static SearchViewModel GetResultsByColor(ArtByHueContext _context, string color, int page)
        {
            var precision = 4;
            var rgb = ConvertHex(color);
            var results = _context.SearchResults.FromSql(@"
                SELECT  
                    ProductID
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
	                , Density AS MatchingColorDensity
                    , NULL AS MatchingTermsScore
                    , CuratorPick
                FROM 
	                ProductColorSearch
                WHERE 
	                R BETWEEN @p0 AND @p1
	                AND G BETWEEN @p2 AND @p3
	                AND B BETWEEN @p4 AND @p5
                ORDER BY 
	                CuratorPick DESC, Density DESC
                OFFSET  
	                @p6 ROWS 
                FETCH NEXT 
	                30 ROWS ONLY 
            ", rgb.R - precision, rgb.R + precision, rgb.G - precision, rgb.G + precision, rgb.B - precision, rgb.B + precision, page * 30)
            .AsNoTracking()
            .ToList();

            var countSQL = _context.ColorSearchMeta.FromSql(@"
                SELECT  
                    COUNT(*) AS ResultCount
                FROM 
	                ProductColorSearch
                WHERE 
	                R BETWEEN @p0 AND @p1
	                AND G BETWEEN @p2 AND @p3
	                AND B BETWEEN @p4 AND @p5
            ", rgb.R - precision, rgb.R + precision, rgb.G - precision, rgb.G + precision, rgb.B - precision, rgb.B + precision, page * 30)
            .AsNoTracking();

            var vm = new SearchViewModel
            {
                Results = results,
                CurrentPage = page,
                SearchColor = color,
                SearchKeywords = "",
                FoundCount = countSQL.First().ResultCount
            };

            return vm;
        }

        private static SearchViewModel GetResultsForKeywordsOnly(ArtByHueContext _context, string keywords, int page)
        {
            if (page * 30 > 100000)
            {
                return new SearchViewModel { FoundCount = -1 };
            }
            var sp = new SearchParameters();
            sp.OrderBy = new List<string> { "CuratorPick desc" };
            sp.Top = 30;
            sp.Skip = page * 30;
            sp.ScoringProfile = "Primary";
            sp.IncludeTotalResultCount = true;

            DocumentSearchResult<ProductSearchResult> response = IndexClient.Documents.Search<ProductSearchResult>(keywords, sp);

            var results = new List<ProductSearchResult>();

            foreach (SearchResult<ProductSearchResult> result in response.Results)
            {
                ProductSearchResult newResult = result.Document;
                newResult.MatchingTermsScore = result.Score;
                results.Add(result.Document);
            }

            var vm = new SearchViewModel
            {
                Results = results,
                CurrentPage = page,
                SearchColor = "",
                SearchKeywords = keywords,
                FoundCount = response.Count
            };

            return vm;
        }

        private static List<ProductSearchResult> GetAllResults(ArtByHueContext _context, string keywords)
        {
            var sp = new SearchParameters();
            //sp.Top = 1000;
            sp.ScoringProfile = "Primary";

            DocumentSearchResult<ProductSearchResult> response = IndexClient.Documents.Search<ProductSearchResult>(keywords, sp);

            var results = new List<ProductSearchResult>();
            AddResults(response, results);

            while (response.ContinuationToken != null)
            {
                response = IndexClient.Documents.ContinueSearch<ProductSearchResult>(response.ContinuationToken);
                AddResults(response, results);
            }
            return results;
        }

        private static void AddResults(DocumentSearchResult<ProductSearchResult> response, List<ProductSearchResult> results)
        {
            foreach (SearchResult<ProductSearchResult> result in response.Results)
            {
                var newResult = result.Document;
                newResult.MatchingTermsScore = result.Score;
                results.Add(result.Document);
            }
        }

        private static SearchViewModel GetFullResults(ArtByHueContext _context, string keywords, RGBColor color, int page)
        {
            var colorProds = GetProductIDsForColor(_context, color);
            var keywordProds = GetAllResults(_context, keywords);

            var results = keywordProds.Where(x => colorProds.Exists(y => y.ProductID == x.ProductID));

            foreach (var result in results)
            {
                result.MatchingColorDensity = colorProds.Find(x => x.ProductID == result.ProductID).MatchingColorDensity;
            }

            var vm = new SearchViewModel
            {
                Results = results.Skip(page * 30).Take(30).ToList(),
                CurrentPage = page,
                SearchColor = "",
                SearchKeywords = keywords,
                FoundCount = results.Count()
            };

            return vm;
        }

        public static SearchViewModel GetForArtist(ArtByHueContext _context, int artistID, int page)
        {
            var artistProds = _context.SearchResults.FromSql(@"
                SELECT DISTINCT
                    ProductID
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
	                ProductColorSearch
                WHERE 
	                ArtistID = @p0
                ORDER BY 
	                CuratorPick DESC, Title
                OFFSET  
	                @p1 ROWS 
                FETCH NEXT 
	                30 ROWS ONLY 
            ", artistID, page * 30)
            .AsNoTracking()
            .ToList();

            var countSQL = _context.ColorSearchMeta.FromSql(@"
                SELECT  
                    COUNT(DISTINCT(ProductID)) AS ResultCount
                FROM 
	                ProductColorSearch
                WHERE 
	                ArtistID = @p0
            ", artistID)
            .AsNoTracking();

            var vm = new SearchViewModel
            {
                Results = artistProds.ToList(),
                CurrentPage = 1,
                SearchColor = "",
                SearchKeywords = "",
                ArtistID = artistID,
                FoundCount = countSQL.First().ResultCount
            };

            return vm;
        }

        private static List<ProductSearchResult> GetProductIDsForColor(ArtByHueContext _context, RGBColor color)
        {
            var precision = 4;
            return _context.SearchResults.FromSql(@"
                SELECT  
                    ProductID
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
	                , Density AS MatchingColorDensity
                    , NULL AS MatchingTermsScore
                    , CuratorPick
                FROM 
	                ProductColorSearch
                WHERE 
	                R BETWEEN @p0 AND @p1
	                AND G BETWEEN @p2 AND @p3
	                AND B BETWEEN @p4 AND @p5
                ORDER BY 
	                CuratorPick DESC, Density DESC
            ", color.R - precision, color.R + precision, color.G - precision, color.G + precision, color.B - precision, color.B + precision)
            .AsNoTracking()
            .ToList();
        }

        public static string ArtistNameLookup (ArtByHueContext _context, int id)
        {
            return _context.Artists.FirstOrDefault(x => x.ArtistID == id)?.Name;
        }
        public static RGBColor ConvertHex(string hexColor)
        {
            hexColor = hexColor.Replace("#", "");
            var rHex = hexColor.Substring(0, 2);
            var gHex = hexColor.Substring(2, 2);
            var bHex = hexColor.Substring(4, 2);

            return new RGBColor
            {
                R = int.Parse(rHex, System.Globalization.NumberStyles.AllowHexSpecifier),
                G = int.Parse(gHex, System.Globalization.NumberStyles.AllowHexSpecifier),
                B = int.Parse(bHex, System.Globalization.NumberStyles.AllowHexSpecifier)
            };

        }

        public static List<string> Suggestions (string term)
        {
            
            // Call suggest query and return results
            var response = TagsClient.Documents.Suggest(term, "suggest-tags", new SuggestParameters { UseFuzzyMatching = true, Top = 15 });
            List<string> suggestions = new List<string>();
            foreach (var result in response.Results)
            {
                suggestions.Add(result.Text);
            }

            return suggestions;
        }
    }
}

