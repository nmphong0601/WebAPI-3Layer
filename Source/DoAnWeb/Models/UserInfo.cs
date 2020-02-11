using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DoAnWeb.Models
{
    //file lưu dữ liệu tạm
    public class UserInfo
    {
        public string Username{ get; set; }
        public string Password { get; set; }
        public bool RemeberMe { get; set; }
        public int Permission { get; set; }
        public User FullInfo { get; set; }
    }
}