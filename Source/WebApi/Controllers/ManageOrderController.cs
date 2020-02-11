using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BUS;

namespace WebApi.Controllers
{
    public class ManageOrderController : Controller
    {
        // GET: ManageOrder
        public ActionResult Index()
        {
            using (var dc = new QLBH_WebEntities())
            {
                var l = dc.Orders.OrderByDescending(o => o.OrderDate);
                return View(l.ToList());
            }
        }

        public ActionResult Update(int id)
        {
            using (var qlbh = new QLBH_WebEntities())
            {
                var listStt = qlbh.Statuses.ToList();
                var order = qlbh.Orders.Where(o => o.OrderID == id).FirstOrDefault();
                var list = new SelectList(listStt, "SttID", "SttName", order.SttID);
                ViewBag.Loai = list;
                return View(order);
            }
        }

        [HttpPost]
        public ActionResult Update(Order order)
        {
            using (var qlbh = new QLBH_WebEntities())
            {
                var or = qlbh.Orders.Where(o => o.OrderID == order.OrderID).FirstOrDefault();
                or.SttID = order.SttID;
                qlbh.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}