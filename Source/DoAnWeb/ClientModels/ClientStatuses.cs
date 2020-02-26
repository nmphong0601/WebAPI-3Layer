using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnWeb.ClientModels
{
    public class ClientStatuses
    {
        public ClientStatuses()
        {
            Orders = new List<ClientOrder>();
        }

        public int? SttID { get; set; }
        public string SttName { get; set; }

        public virtual List<ClientOrder> Orders { get; set; }
    }
}
