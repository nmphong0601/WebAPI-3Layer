using DoAnWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb.Controllers
{
    public class CartController : Controller
    {
        // GET: Cart
        public ActionResult Detail()
        {
            var cart = Session["cart"] as Cart;
            return View(cart);
        }

        //post:Cart/Add
        [HttpPost]
        public ActionResult Add(int proId,int quantity )
        {
           
            if(Session["cart"]==null){
                Session["cart"] = new Cart();
            }
            var c = Session["cart"] as Cart;
     
            c.AddItem(proId, quantity);
            return RedirectToAction("Detail", "Product", new { id=proId});
        }

        //post:Cart/AddCIFromListProduct
        [HttpPost]
        //bấm nút mua sẽ thêm vào được giỏ hàng
        public ActionResult AddCIFromListProduct(int proId, int catId,int page)
        {

            if (Session["cart"] == null)
            {
                Session["cart"] = new Cart();
            }
            var c = Session["cart"] as Cart;

            c.AddItem(proId);
            return RedirectToAction("GetListByCategory", "Product", new { id = catId ,page=page});
        }

        [HttpPost]
        //bấm nút mua sẽ thêm vào được giỏ hàng
        public ActionResult AddCIFromIndex(int proId)
        {

            if (Session["cart"] == null)
            {
                Session["cart"] = new Cart();
            }
            var c = Session["cart"] as Cart;

            c.AddItem(proId);
            return RedirectToAction("Index", "Home");
        }

        //post:Cart/Remove
        [HttpPost]
        //bấm nút xóa trong trang thanh toán
        public ActionResult Remove(int proId)
        {
       
            var c = Session["cart"] as Cart;
            c.Remove(proId);
            return RedirectToAction("Detail", "Cart");
        }

        //post:Cart/Update
        [HttpPost]
        //bấm nút cập nhật trong trang thanh toán
        public ActionResult Update(int proId,int quantity)
        {
            var c = Session["cart"] as Cart;
            c.Update(proId,quantity);
            return RedirectToAction("Detail", "Cart");
        }

        //post:Cart/Checkout
        [HttpPost]
        //bấm nút thanh toán, nó sẽ lưu vào csdl và giỏ hàng sẽ trở về số 0
        public ActionResult Checkout( decimal totalPrice)
        {
            var c = Session["cart"] as Cart;
            var ui = Session["Logged"] as UserInfo;

            using (var dc = new QLBH_WebEntities())
            {
                var user = dc.Users.Where(u => u.f_Username == ui.Username).FirstOrDefault();
                var order = new Order
                {
                    OrderDate = DateTime.Now,
                    User = user,
                    Total = totalPrice,
                };
                if(dc.Orders.Count() == 0)
                {
                    order.OrderID = 1;
                }
                dc.Orders.Add(order);
                decimal amount = 0;
                foreach(var ci in c.Items){
                    var p = dc.Products.Where(i=>i.ProID==ci.Product.ProID).FirstOrDefault();
                    amount=(decimal)p.Price*ci.Quantity;
                    var od = new OrderDetail { 
                        Order=order,
                        Product=p,
                        Quantity=ci.Quantity,
                        Price=(decimal)p.Price,
                        Amount=amount
                    };
                    dc.OrderDetails.Add(od);
                    p.Quantity -= ci.Quantity;
                    dc.Entry(p).State = EntityState.Modified;
                    dc.SaveChanges();
                }
                dc.SaveChanges();
            }
            c.Checkout();
            Session["CheckOut"] = 1;
            return RedirectToAction("Detail", "Cart");
        }
    }   
}