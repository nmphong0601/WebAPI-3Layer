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
    public class ManagerCatController : Controller
    {
        // GET: ManagerCat
        public ActionResult Index()
        {
            var l = CSDLQLBH.GetCategories().ToList();
            return View(l);
        }

        // GET: ManagerCat/Add
        public ActionResult Add()
        {
            var c = new ClientCategory
            {
                CatName = "cat"

            };
            return View(c);
        }

        // Post: ManagerCat/Add
        [HttpPost]
        public ActionResult Add(ClientCategory c)
        {
            //lưu thông tin
            CSDLQLBH.InsertCategory(c);
            return RedirectToAction("Index");
        }

        // GET: ManagerCat/Delete
        public ActionResult Delete(int id)
        {
            CSDLQLBH.RemoveCategory(id);
            return RedirectToAction("Index");
        }

        // GET: ManagerCat/Update
        public ActionResult Update(int id)
        {
            var cat = CSDLQLBH.GetSingleCategory(id);
            return View(cat);
        }

        [HttpPost]
        public ActionResult Update(ClientCategory category)
        {
            var cat = CSDLQLBH.GetSingleCategory(category.CatID);
            cat.CatName = category.CatName;
            CSDLQLBH.UpdateCategory(cat);
            return RedirectToAction("Index");
        }


    }
}