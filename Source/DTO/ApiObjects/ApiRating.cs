using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ApiObjects
{
    public class ApiRating: ApiEntity
    {
        public ApiRating()
        {
            
        }

        public int? ProID { get; set; }
        public int? Two { get; set; }
        public int? Three { get; set; }
        public int? Four { get; set; }
        public int? Five { get; set; }
        public int? One { get; set; }
        public int? Rate { get; set; }

        public ApiProduct Product { get; set; }
    }
}
