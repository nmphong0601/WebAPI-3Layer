using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnWeb.ClientModels
{
    public class ClientRating
    {
        public ClientRating()
        {
            
        }

        public int? ProID { get; set; }
        public int? Two { get; set; }
        public int? Three { get; set; }
        public int? Four { get; set; }
        public int? Five { get; set; }
        public int? One { get; set; }
        public int? Rate { get; set; }

        public ClientProduct Product { get; set; }
    }
}
