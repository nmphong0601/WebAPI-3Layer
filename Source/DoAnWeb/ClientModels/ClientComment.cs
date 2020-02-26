using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAnWeb.ClientModels
{
    public class ClientComment
    {
        public ClientComment()
        {
            
        }

        public int? UserID { get; set; }
        public string Content { get; set; }
        public DateTime? Time { get; set; }
        public int? ProID { get; set; }

        public ClientProduct Product { get; set; }
        public ClientUser User { get; set; }
    }
}
