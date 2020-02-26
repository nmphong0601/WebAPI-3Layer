using DoAnWeb.Caching;
using DoAnWeb.ClientModels;
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
                Session["cart"] = new ClientCart();
            }
            var c = Session["cart"] as ClientCart;
     
            c = CSDLQLBH.InsertCartItem(c, proId, quantity);
            return RedirectToAction("Detail", "Product", new { id=proId});
        }

        //post:Cart/AddCIFromListProduct
        [HttpPost]
        //bấm nút mua sẽ thêm vào được giỏ hàng
        public ActionResult AddCIFromListProduct(int proId, int catId,int page)
        {

            if (Session["cart"] == null)
            {
                Session["cart"] = new ClientCart();
            }
            var c = Session["cart"] as ClientCart;

            c = CSDLQLBH.InsertCartItem(c, proId);
            return RedirectToAction("GetListByCategory", "Product", new { id = catId ,page=page});
        }

        [HttpPost]
        //bấm nút mua sẽ thêm vào được giỏ hàng
        public ActionResult AddCIFromIndex(int proId)
        {

            if (Session["cart"] == null)
            {
                Session["cart"] = new ClientCart();
            }
            var c = Session["cart"] as ClientCart;

            c = CSDLQLBH.InsertCartItem(c, proId);
            return RedirectToAction("Index", "Home");
        }

        //post:Cart/Remove
        [HttpPost]
        //bấm nút xóa trong trang thanh toán
        public ActionResult Remove(int proId)
        {
       
            var c = Session["cart"] as ClientCart;
            c = CSDLQLBH.RemoveCartItem(c, proId);
            return RedirectToAction("Detail", "Cart");
        }

        //post:Cart/Update
        [HttpPost]
        //bấm nút cập nhật trong trang thanh toán
        public ActionResult Update(int proId,int quantity)
        {
            var c = Session["cart"] as ClientCart;
            c = CSDLQLBH.UpdateCartItem(c, proId, quantity);
            return RedirectToAction("Detail", "Cart");
        }

        //post:Cart/Checkout
        [HttpPost]
        //bấm nút thanh toán, nó sẽ lưu vào csdl và giỏ hàng sẽ trở về số 0
        public ActionResult Checkout( decimal totalPrice)
        {
            var c = Session["cart"] as ClientCart;
            var ui = Session["Logged"] as ClientUserInfo;

            var user = CSDLQLBH.UserGetSingleByUserName(ui.Username);
            var order = new ClientOrder
            {
                OrderDate = DateTime.Now,
                User = user,
                Total = totalPrice,
            };

            var totalOrders = CSDLQLBH.GetOrders().ToList().Count;
            if (totalOrders == 0)
            {
                order.OrderID = 1;
            }
            CSDLQLBH.InsertOrders(order);

            decimal amount = 0;
            foreach (var ci in c.Items)
            {
                var p = CSDLQLBH.GetSingleProduct(ci.Product.ProID);
                amount = (decimal)p.Price * ci.Quantity;
                var od = new ClientOrderDetail
                {
                    Order = order,
                    Product = p,
                    Quantity = ci.Quantity,
                    Price = (decimal)p.Price,
                    Amount = amount
                };
                CSDLQLBH.InsertOrderDetail(od);
                p.Quantity -= ci.Quantity;
                CSDLQLBH.UpdateProduct(p);
            }

            c = CSDLQLBH.Checkout(c);
            Session["CheckOut"] = 1;
            return RedirectToAction("Detail", "Cart");
        }
    }   
}