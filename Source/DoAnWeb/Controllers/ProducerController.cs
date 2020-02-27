using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Caching;
using DoAnWeb.Models;

namespace DoAnWeb.Controllers
{
    public class ProducerController : Controller
    {
        // GET: Producer
        public ActionResult GetListSX()
        {
            var l = CSDLQLBH.GetProducers().ToList();
            return PartialView("_PartialListSX", l);

        }

        public ActionResult GetListProducer()
        {
            var l1 = CSDLQLBH.GetProducers().ToList();
            return PartialView("_PartialListProducer", l1);

        }
    }
}