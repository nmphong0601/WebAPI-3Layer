using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ApiObjects
{
    public class ApiUserInfo
    {
        public ApiUserInfo()
        {
            
        }

        public string Username { get; set; }
        public string Password { get; set; }
        public bool? RemeberMe { get; set; }
        public string Permission { get; set; }
        public ApiUser FullInfo { get; set; }
    }
}
