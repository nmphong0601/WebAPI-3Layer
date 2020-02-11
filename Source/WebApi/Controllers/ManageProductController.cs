using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BUS;


namespace WebApi.Controllers
{

    public class ManageProductController : Controller
    {
        // GET: ManageProduct
        //khi nào xét premession=1 thì mở nó ra bỏ cái dưới đi
        [AuthActionFilter(RequiredPermission=1)]
        // [AuthActionFilter]

        // GET: ManageProduct
        public ActionResult Index()
        {
            using (var dc = new QLBH_WebEntities())
            {
                var l = dc.Products.ToList();
                return View(l);
            }
          
        }

        // GET: ManageProduct/Delete
        public ActionResult Delete(int id)
        {
            using (var dc = new QLBH_WebEntities())
            {
                var pD = dc.Products.Where(p => p.ProID == id).FirstOrDefault();
                if(pD!=null){
                    dc.Products.Remove(pD);
                    dc.SaveChanges();

                }
                Ulti.DeleteProductImgs(id, Server.MapPath("~"));
                return RedirectToAction("Index");
            }
        }

        // GET: ManageProduct/Add
        public ActionResult Add()
        {
            var p=new Product{
                ProName="product",
                CatID=3,
                Price=100000,
                Quantity=2,
                TinyDes="tinydes"

            };
            return View(p);
        }

        // Post: ManageProduct/Add
        [HttpPost]
        public ActionResult Add(Product p, HttpPostedFile imgLg, HttpPostedFile imgSm)
        {
            //khi hai ô này rỗng
            if (p.TinyDes == null)
            {
                p.TinyDes=string.Empty;
            }

            if(p.FullDesRaw ==null){
                p.FullDesRaw= string.Empty;
            }
            p.FullDes = p.FullDesRaw;
            p.View = 0;
            //lưu thông tin
            using (var dc = new QLBH_WebEntities())
            {
                dc.Products.Add(p);
                dc.SaveChanges();
            }
            Ulti.SaveProductImgs(p.ProID,Server.MapPath("~"),imgLg,imgSm);
            return RedirectToAction("Index");
        }
    }
}