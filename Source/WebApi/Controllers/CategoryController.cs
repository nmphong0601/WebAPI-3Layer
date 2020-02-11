using BUS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApi.Controllers
{
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult GetList()
        {
            using (var dc = new QLBH_WebEntities())
            {
                var l = dc.Categories.ToList();
                return PartialView("_PartialList",l);
            }
          
        }
        public ActionResult GetListSX()
        {
            using (var dc = new QLBH_WebEntities())
            {
                var l = dc.Producers.ToList();
                return PartialView("_PartialListSX", l);
            }
        }
        public ActionResult GetListCategories()
        {
            using (var qlbh = new QLBH_WebEntities())
            {
                var l1 = qlbh.Categories.ToList();
                return PartialView("_PartialListCategories", l1);
            }
        }
    }
}