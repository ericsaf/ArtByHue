using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class SearchFormViewModel
    {
        [Display(Name = "Color")]
        public string color { get; set; }
        [Display(Name = "Keywords")]
        public string keywords { get; set; }
    }
}
