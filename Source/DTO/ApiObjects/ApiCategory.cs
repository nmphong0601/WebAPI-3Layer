using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ApiObjects
{
    public class ApiCategory: ApiEntity
    {
        public ApiCategory()
        {
            Products = new List<ApiProduct>();
        }

        public int? CatID { get; set; }
        public string CatName { get; set; }

        public List<ApiProduct> Products { get; set; }
    }
}
