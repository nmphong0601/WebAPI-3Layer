using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Caching;
using DoAnWeb.ClientModels;
using DoAnWeb.Ultilities;

namespace DoAnWeb.Controllers
{
    public class ManagerProducerController : Controller
    {
        // GET: ManagerProducer
        public ActionResult Index()
        {
            var l = CSDLQLBH.GetProducers().ToList();
            return View(l);
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
        public ActionResult Add(ClientProducer prod)
        {
            //lưu thông tin
            prod = CSDLQLBH.InsertProducer(prod);
            return RedirectToAction("Index");
        }

        // GET: ManagerProducer/Delete
        public ActionResult Delete(int id)
        {
            CSDLQLBH.RemoveProducer(id);
            return RedirectToAction("Index");
        }

        // GET: ManagerProducer/Update
        public ActionResult Update(int id)
        {
            var prod = CSDLQLBH.GetSingleProducer(id);
            return View(prod);
        }

        [HttpPost]
        public ActionResult Update(ClientProducer producer)
        {
            var prod = CSDLQLBH.GetSingleProducer(producer.ProducerID);
            prod.ProducerName = producer.ProducerName;
            CSDLQLBH.UpdateProducer(prod);
            return RedirectToAction("Index");
        }
    }
}