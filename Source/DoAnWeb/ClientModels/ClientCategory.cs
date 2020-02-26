using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnWeb.ClientModels
{
    public class ClientCategory
    {
        public ClientCategory()
        {
            Products = new List<ClientProduct>();
        }

        public int? CatID { get; set; }
        public string CatName { get; set; }

        public List<ClientProduct> Products { get; set; }
    }
}
