using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnWeb.Models
{
    public class SearchProduct
    {
        public float PriceMin { get; set; }
        public float PriceMax { get; set; }
        public string CatName  { get; set; }
        public string ProducerName { get; set; }
    }
}