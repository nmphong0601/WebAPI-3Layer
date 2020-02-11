using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DTO.Models
{
    public class Cart
    {
        public IList<CartItem> Items { get; set; }
        public int proId { get; set; }

        public Cart()
        {
            Items = new List<CartItem>();
        }
    }
    public class CartItem {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}