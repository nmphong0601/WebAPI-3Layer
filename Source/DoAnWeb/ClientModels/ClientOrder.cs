using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnWeb.ClientModels
{
    public class ClientOrder
    {
        public ClientOrder()
        {
            OrderDetails = new List<ClientOrderDetail>();
        }

        public int? OrderID { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? UserID { get; set; }
        public decimal? Total { get; set; }
        public int? SttID { get; set; }

        public List<ClientOrderDetail> OrderDetails { get; set; }
        public ClientUser User { get; set; }
        public ClientStatuses Status { get; set; }
    }
}
