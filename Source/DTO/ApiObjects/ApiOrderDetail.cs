using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ApiObjects
{
    public class ApiOrderDetail: ApiEntity
    {
        public ApiOrderDetail()
        {
            
        }

        public int? OrderID { get; set; }
        public int? ProID { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Amount { get; set; }

        public ApiOrder Order { get; set; }
        public ApiProduct Product { get; set; }
    }
}
