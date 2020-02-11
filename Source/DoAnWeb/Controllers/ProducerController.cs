using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;

namespace DoAnWeb.Controllers
{
    public class ProducerController : Controller
    {
        // GET: Producer
        public ActionResult GetListSX()
        {
            using (var dc = new QLBH_WebEntities())
            {
                var l = dc.Producers.ToList();
                return PartialView("_PartialListSX", l);
            }

        }

        public ActionResult GetListProducer()
        {
            using (var qlbh = new QLBH_WebEntities())
            {
                var l1 = qlbh.Producers.ToList();
                return PartialView("_PartialListProducer", l1);
            }

        }
    }
}