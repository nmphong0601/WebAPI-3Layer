using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ApiObjects
{
    public class ApiComment
    {
        public ApiComment()
        {
            
        }

        public int? UserID { get; set; }
        public string Content { get; set; }
        public DateTime? Time { get; set; }
        public int? ProID { get; set; }

        public ApiProduct Product { get; set; }
        public ApiUser User { get; set; }
    }
}
