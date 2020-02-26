using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Caching;
using DoAnWeb.Models;

namespace DoAnWeb.Controllers
{
    public class ManageOrderController : Controller
    {
        // GET: ManageOrder
        public ActionResult Index()
        {
            var l = CSDLQLBH.GetOrders(sort: "OrderDate DESC");
            return View(l.ToList());
        }

        public ActionResult Update(int id)
        {
            var listStt = CSDLQLBH.GetStatuses().ToList();
            var order = CSDLQLBH.GetSingleOrder(id);
            var list = new SelectList(listStt, "SttID", "SttName", order.SttID);
            ViewBag.Loai = list;
            return View(order);
        }

        [HttpPost]
        public ActionResult Update(Order order)
        {
            var or = CSDLQLBH.GetSingleOrder(order.OrderID);
            or.SttID = order.SttID;
            CSDLQLBH.UpdateOrders(or);
            return RedirectToAction("Index");
        }
    }
}