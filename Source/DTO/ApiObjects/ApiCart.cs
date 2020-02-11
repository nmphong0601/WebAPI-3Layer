using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ApiObjects
{
    public class ApiCart
    {
        public ApiCart()
        {
            Items = new List<ApiCartItem>();
        }

        public List<ApiCartItem> Items { get; set; }
        public int? proId { get; set; }
    }

    public class ApiCartItem
    {
        public ApiProduct Product { get; set; }
        public int Quantity { get; set; }
    }
}
