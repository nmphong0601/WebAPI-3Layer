using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnWeb.ClientModels
{
    public class ClientProducer
    {
        public ClientProducer()
        {
            Products = new List<ClientProduct>();
        }

        public int? ProducerID { get; set; }
        public string ProducerName { get; set; }

        public List<ClientProduct> Products { get; set; }
    }
}
