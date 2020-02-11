using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTO.Models
{
    public class SearchProduct
    {
        public float PriceMin { get; set; }
        public float PriceMax { get; set; }
        public string CatName  { get; set; }
        public string ProducerName { get; set; }
    }
}