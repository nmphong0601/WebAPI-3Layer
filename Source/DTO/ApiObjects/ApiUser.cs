using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ApiObjects
{
    public class ApiUser: ApiEntity
    {
        public ApiUser()
        {
            Orders = new List<ApiOrder>();
            Comments = new List<ApiComment>();
        }

        public int? f_ID { get; set; }
        public string f_Username { get; set; }
        public string f_Password { get; set; }
        public string f_Name { get; set; }
        public string f_Email { get; set; }
        public DateTime? f_DOB { get; set; }
        public int? f_Permission { get; set; }
        public string f_Address { get; set; }
        public string f_Phone { get; set; }

        public List<ApiOrder> Orders { get; set; }
        public List<ApiComment> Comments { get; set; }
    }
}
