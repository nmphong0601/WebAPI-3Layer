using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ApiObjects
{
    public class ApiStatues: ApiEntity
    {
        public ApiStatues()
        {
            Orders = new List<ApiOrder>();
        }

        public int? SttID { get; set; }
        public string SttName { get; set; }

        public virtual List<ApiOrder> Orders { get; set; }
    }
}
