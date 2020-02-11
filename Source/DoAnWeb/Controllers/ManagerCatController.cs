using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Models;
using DoAnWeb.Ultilities;

namespace DoAnWeb.Controllers
{
    public class ManagerCatController : Controller
    {
        // GET: ManagerCat
        public ActionResult Index()
        {
            using (var dc = new QLBH_WebEntities())
            {
                var l = dc.Categories.ToList();
                return View(l);
            }
        }

        // GET: ManagerCat/Add
        public ActionResult Add()
        {
            var c = new Category
            {
                CatName = "cat"

            };
            return View(c);
        }

        // Post: ManagerCat/Add
        [HttpPost]
        public ActionResult Add(Category c)
        {
            //lưu thông tin
            using (var dc = new QLBH_WebEntities())
            {
                dc.Categories.Add(c);
                dc.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        // GET: ManagerCat/Delete
        public ActionResult Delete(int id)
        {
            using (var dc = new QLBH_WebEntities())
            {
                var cat = dc.Categories.Where(c => c.CatID == id).FirstOrDefault();
                if (cat != null)
                {
                    dc.Categories.Remove(cat);
                    var listProduct = dc.Products.Where(p => p.CatID == id);
                    foreach (Product p in listProduct)
                    {
                        dc.Products.Remove(p);
                        Ulti.DeleteProductImgs(p.ProID, Server.MapPath("~"));
                    }
                    dc.SaveChanges();
                }
                return RedirectToAction("Index");
            }
        }

        // GET: ManagerCat/Update
        public ActionResult Update(int id)
        {
            using (var qlbh = new QLBH_WebEntities())
            {
                var cat = qlbh.Categories.Where(c => c.CatID == id).FirstOrDefault();
                return View(cat);
            }
        }

        [HttpPost]
        public ActionResult Update(Category category)
        {
            using (var qlbh = new QLBH_WebEntities())
            {
                var cat = qlbh.Categories.Where(c => c.CatID == category.CatID).FirstOrDefault();
                cat.CatName = category.CatName;
                qlbh.SaveChanges();
                return RedirectToAction("Index");
            }
        }


    }
}