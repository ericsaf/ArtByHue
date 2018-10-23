using ColorMine.ColorSpaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using WebApp.Services;
using WebApp.Services.Utilities;

namespace WebApp.Models
{
    public class ColorName
    {
        private static Dictionary<string, string> _SlugLookup { get; set; }

        public static Dictionary<string, string> SlugLookup()
        {
            if (_SlugLookup == null)
            {
                var slugs = new Dictionary<string, string>();
                foreach (var color in ColorName.PaintColors)
                {
                    if (!slugs.ContainsValue(color.name))
                    {
                        slugs.Add(Slug.Create(true, color.name), color.name);
                    }
                }
                _SlugLookup = slugs;
            }
            return _SlugLookup;
        }
        public static string Get(string color)
        {
            var name = "";
            if (color.Contains("-"))
            {
                var code = color.Substring(color.Length - 2);
                var supplierName = SupplierCodes[code];
                name = PaintColors.First(x => x.name.Contains(supplierName) && x.hex.ToUpper() == color.Substring(0, 7).ToUpper())?.name;
            }
            else
            {
                name = PaintColors.FirstOrDefault(x => x.hex.ToUpper() == "#" + color.Substring(0, 6).ToUpper())?.name;
                if (String.IsNullOrEmpty(name))
                {
                    var myColor = ProductSearch.ConvertHex(color);
                    name = GetHueName(GetHue(myColor)) + " (" + color + ")";
                }
            }
            return name;
        }

        public static int GetHue(RGBColor color)
        {
            
            var R = decimal.Divide(color.R, 255);
            var G = decimal.Divide(color.G, 255);
            var B = decimal.Divide(color.B, 255);

            var Min = Math.Min(R, Math.Max(G, B));
            var Max = Math.Max(R, Math.Max(G, B));
            //var del_Max = Max - Min;

            decimal H = 0;
            decimal S = 0;

            decimal L = (Max + Min) / 2;

            if (Min != Max)
            {
                var del_Max = Max - Min;
                S = L > (decimal)0.5 ? del_Max / (2 - Max - Min) : Max + Min;

                if (Max == R)
                {
                    H = (G - B) / del_Max + (G < B ? 6 : 0);
                }
                else if (Max == G)
                {
                    H = (B - R) / del_Max + 2;
                }
                else
                {
                    H = (R - G) / del_Max + 4;
                }

                H = H * 60;
                if (H < 0) { H = H + 360; }

            }

            return Convert.ToInt32(Math.Round(H, 0));
        }
        private static List<ColorLookup> _colors { get; set; }

        public static List<ColorLookup> PaintColors
        {
            get
            {
                if (_colors != null) { return _colors; }
                var colorData = new Rootobject();

                using (StreamReader sr = File.OpenText("wwwroot/content/pcolors.json"))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    colorData = (Rootobject)serializer.Deserialize(sr, typeof(Rootobject));
                }

                var colors = colorData.colors.ToList();
                _colors = colors;

                return colors;
            }
        }

        private static Dictionary<string, string> _SupplierCodes { get; set; }
        private static Dictionary<string, string> SupplierCodes
        {
            get
            {
                if (_SupplierCodes == null)
                {
                    _SupplierCodes = new Dictionary<string, string>();
                    _SupplierCodes.Add("BM", "B. Moore");
                    _SupplierCodes.Add("PA", "Pantone");
                    _SupplierCodes.Add("BH", "Behr");
                    _SupplierCodes.Add("SW", "S. Williams");
                    _SupplierCodes.Add("VL", "Valspar");

                }
                return _SupplierCodes;
            }
        }

        private static string GetHueName(float hue)
        {
            var color = "";

            if ((hue > 0 && hue <= 10) || (hue > 355 && hue <= 360))
            {
                color = "Red";
            }
            else if (hue > 10 && hue <= 20)
            {
                color = "Red / Orange";
            }
            else if (hue > 20 && hue <= 40)
            {
                color = "Orange / Brown";
            }
            else if (hue > 40 && hue <= 50)
            {
                color = "Orange / Yellow";
            }
            else if (hue > 50 && hue <= 60)
            {
                color = "Yellow";
            }
            else if (hue > 60 && hue <= 80)
            {
                color = "Yellow / Green";
            }
            else if (hue > 80 && hue <= 140)
            {
                color = "Green";
            }
            else if (hue > 140 && hue <= 169)
            {
                color = "Green / Cyan";
            }
            else if (hue > 169 && hue <= 200)
            {
                color = "Cyan";
            }
            else if (hue > 200 && hue <= 220)
            {
                color = "Cyan / Blue";
            }
            else if (hue > 220 && hue <= 240)
            {
                color = "Blue";
            }
            else if (hue > 240 && hue <= 280)
            {
                color = "Blue / Magenta";
            }
            else if (hue > 280 && hue <= 320)
            {
                color = "Magenta";
            }
            else if (hue > 320 && hue <= 330)
            {
                color = "Magenta / Pink";
            }
            else if (hue > 330 && hue <= 345)
            {
                color = "Pink";
            }
            else if (hue > 345 && hue <= 355)
            {
                color = "Pink / Red";
            }

            return "Custom " + color;

        }
    }

    public class Rootobject
    {
        public ColorLookup[] colors { get; set; }
    }

    public class ColorLookup
    {
        public string name { get; set; }
        public string hex { get; set; }
    }

    public class MatchedPaint
    {
        public Rgb RGB { get; set; }
        public string Manufacturer { get; set; }
        public string Name { get; set; }
    }
}
