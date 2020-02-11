using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ApiObjects
{
    public class ApiProducer
    {
        public ApiProducer()
        {
            Products = new List<ApiEntity>();
        }

        public int? ProducerID { get; set; }
        public string ProducerName { get; set; }

        public List<ApiEntity> Products { get; set; }
    }
}
