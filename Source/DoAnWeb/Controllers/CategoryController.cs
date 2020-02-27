using DoAnWeb.Caching;
using DoAnWeb.ClientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult GetList()
        {
            var l = CSDLQLBH.GetCategories().ToList();
            return PartialView("_PartialList", l);

        }
        public ActionResult GetListSX()
        {
            var l = CSDLQLBH.GetProducers().ToList();
            return PartialView("_PartialListSX", l);
        }
        public ActionResult GetListCategories()
        {
            var l1 = CSDLQLBH.GetCategories().ToList();
            return PartialView("_PartialListCategories", l1);
        }
    }
}