using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ApiObjects
{
    public class ApiCategory
    {
        public ApiCategory()
        {
            Products = new List<ApiEntity>();
        }

        public int? CatID { get; set; }
        public string CatName { get; set; }

        public List<ApiEntity> Products { get; set; }
    }
}
