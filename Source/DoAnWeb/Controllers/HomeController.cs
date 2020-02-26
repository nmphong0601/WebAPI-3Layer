using DoAnWeb.Caching;
using DoAnWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            using (var dc = new QLBH_WebEntities())
            {
                var l = CSDLQLBH.GetProducts().ToList();
                return View(l);
            }           
        }
    }
}