using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ApiObjects
{
    public class ApiProducer: ApiEntity
    {
        public ApiProducer()
        {
            Products = new List<ApiProduct>();
        }

        public int? ProducerID { get; set; }
        public string ProducerName { get; set; }

        public List<ApiProduct> Products { get; set; }
    }
}
