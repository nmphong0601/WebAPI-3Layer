using DoAnWeb.Caching;
using DoAnWeb.ClientModels;
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
            var l = CSDLQLBH.GetProducts().ToList();
            return View(l);
        }
    }
}