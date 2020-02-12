using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ApiObjects
{
    public class ApiProduct: ApiEntity
    {
        public ApiProduct()
        {
            OrderDetails = new List<ApiOrderDetail>();
        }

        public int? ProID { get; set; }
        public string ProName { get; set; }
        public string TinyDes { get; set; }
        public string FullDes { get; set; }
        public decimal? Price { get; set; }
        public int? ProducerID { get; set; }
        public int? Quantity { get; set; }
        public int? View { get; set; }
        public string MadeIn { get; set; }
        public int? CatID { get; set; }
        public DateTime? ReceipDate { get; set; }
        public int? Orders { get; set; }

        public ApiCategory Category { get; set; }
        public ApiProducer Producer { get; set; }
        public ApiComment Comment { get; set; }
        public List<ApiOrderDetail> OrderDetails { get; set; }
        public ApiRating Rating { get; set; }
    }
}
