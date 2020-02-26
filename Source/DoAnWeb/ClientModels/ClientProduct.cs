using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DoAnWeb.ClientModels
{
    public class ClientProduct
    {
        public ClientProduct()
        {
            OrderDetails = new List<ClientOrderDetail>();
        }

        public int? ProID { get; set; }
        public string ProName { get; set; }
        public string TinyDes { get; set; }
        public string FullDes { get; set; }
        [AllowHtml]
        //đưa dữ liệu ô FullDes dạng <li> về bình thường
        public string FullDesRaw { get; set; }
        public decimal? Price { get; set; }
        public int? ProducerID { get; set; }
        public int? Quantity { get; set; }
        public int? View { get; set; }
        public string MadeIn { get; set; }
        public int? CatID { get; set; }
        public DateTime? ReceipDate { get; set; }
        public int? Orders { get; set; }

        public ClientCategory Category { get; set; }
        public ClientProducer Producer { get; set; }
        public ClientComment Comment { get; set; }
        public List<ClientOrderDetail> OrderDetails { get; set; }
        public ClientRating Rating { get; set; }
    }
}
