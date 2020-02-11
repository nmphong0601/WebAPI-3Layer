using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;
using DoAnWeb.Ultilities;

namespace DoAnWeb.Controllers
{
    public class ManagerProducerController : Controller
    {
        // GET: ManagerProducer
        public ActionResult Index()
        {
            using (var dc = new QLBH_WebEntities())
            {
                var l = dc.Producers.ToList();
                return View(l);
            }
        }

        // GET: ManagerProducer/Add
        public ActionResult Add()
        {
            var prod = new Producer
            {
                ProducerName = "prodName"

            };
            return View(prod);
        }

        // Post: ManagerProducer/Add
        [HttpPost]
        public ActionResult Add(Producer prod)
        {
            //lưu thông tin
            using (var dc = new QLBH_WebEntities())
            {
                dc.Producers.Add(prod);
                dc.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: ManagerProducer/Delete
        public ActionResult Delete(int id)
        {
            using (var dc = new QLBH_WebEntities())
            {
                var prod = dc.Producers.Where(p => p.ProducerID == id).FirstOrDefault();
                if (prod != null)
                {
                    dc.Producers.Remove(prod);
                    var listProduct = dc.Products.Where(p => p.ProducerID == id);
                    foreach (Product pro in listProduct)
                    {
                        dc.Products.Remove(pro);
                        Ulti.DeleteProductImgs(pro.ProID, Server.MapPath("~"));
                    }
                    dc.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }

        // GET: ManagerProducer/Update
        public ActionResult Update(int id)
        {
            using (var qlbh = new QLBH_WebEntities())
            {
                var prod = qlbh.Producers.Where(p => p.ProducerID == id).FirstOrDefault();
                return View(prod);
            }
        }

        [HttpPost]
        public ActionResult Update(Producer producer)
        {
            using (var qlbh = new QLBH_WebEntities())
            {
                var prod = qlbh.Producers.Where(p => p.ProducerID == producer.ProducerID).FirstOrDefault();
                prod.ProducerName = producer.ProducerName;
                qlbh.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}