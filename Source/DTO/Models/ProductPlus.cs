using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb.Models
{
    public partial class Product
    {
        [AllowHtml]
        //đưa dữ liệu ô FullDes dạng <li> về bình thường
        public string FullDesRaw { get; set; }
    }
}