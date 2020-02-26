using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnWeb.ClientModels
{
    public class ClientCart
    {
        public ClientCart()
        {
            Items = new List<ClientCartItem>();
        }

        public List<ClientCartItem> Items { get; set; }
        public int? proId { get; set; }
    }

    public class ClientCartItem
    {
        public ClientProduct Product { get; set; }
        public int Quantity { get; set; }
    }
}
