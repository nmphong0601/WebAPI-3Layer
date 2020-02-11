using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ApiObjects
{
    public class ApiStatues
    {
        public ApiStatues()
        {
            Orders = new List<ApiEntity>();
        }

        public int? SttID { get; set; }
        public string SttName { get; set; }

        public virtual List<ApiEntity> Orders { get; set; }
    }
}
