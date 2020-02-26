using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnWeb.ClientModels
{
    public class ClientOrderDetail
    {
        public ClientOrderDetail()
        {
            
        }

        public int? OrderID { get; set; }
        public int? ProID { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Amount { get; set; }

        public ClientOrder Order { get; set; }
        public ClientProduct Product { get; set; }
    }
}
