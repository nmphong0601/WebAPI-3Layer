using DoAnWeb.Caching;
using DoAnWeb.ClientModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DoAnWeb.Controllers
{
    public class ProductController : Controller
    {
        static int nPerPage = 6;
        // GET: Product
        
        //phân trang sản phẩm theo ds
        public ActionResult GetListByCategory(int? id, int page=1)
        {

            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            Dictionary<string, object> result = CSDLQLBH.GetPagedProducts(filter: "CatID =" + id, page: page, pageSize: nPerPage);

            ViewBag.totalPage = result["totalPage"];
            ViewBag.curPage = result["curPage"];
            ViewBag.cId = id;

            var l = result["Collection"] as List<ClientProduct>;

            return View("ListByCategory", l);

        }

        public ActionResult GetListByCategorySX(int? id, int page = 1)
        {

            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }

            Dictionary<string, object> result = CSDLQLBH.GetPagedProducts(filter: "ProducerID =" + id, page: page, pageSize: nPerPage);

            ViewBag.totalPage = result["totalPage"];
            ViewBag.curPage = result["curPage"];
            ViewBag.cId = id;

            var l = result["Collection"] as List<ClientProduct>;

            return View("ListByCategory", l);

        }

        public ActionResult Detail(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }
            var product = CSDLQLBH.GetSingleProduct(id);
            List<ClientComment> listCom = CSDLQLBH.GetCommentsByProduct(product.ProID).ToList();
            if (listCom != null)
            {
                ViewBag.ListComment = listCom;
            }
            return View(product);
        }
        //sản phẩm cùng loại
        public ActionResult ProductTheSame(int catID,int proID)
        {

            var l = CSDLQLBH.GetProducts(filter: "CatID = " + catID + " and ProID != " + proID).Take(6).ToList();
            return PartialView("ProductTheSame", l);

        }
        //sản phẩm cùng nhà sản xuất
        public ActionResult ProductAsProducer(int producerID, int proID)
        {

            var l = CSDLQLBH.GetProducts(filter: "ProducerID = " + producerID + " and ProID != " + proID).Take(5).ToList();
            return PartialView("ProductAsProducer", l);

        }

        [HttpPost]
        public ActionResult AddComment(int? proId, string uName, string content, string dayCM)
        {
            ClientUser u = CSDLQLBH.GetUsers(filter: "f_Username = " + uName).FirstOrDefault();

            ClientComment com = new ClientComment
            {
                UserID = u.f_ID,
                ProID = proId,
                Content = content,
                Time = DateTime.Parse(dayCM)
            };

            CSDLQLBH.InsertComment(com);
            return RedirectToAction("Detail", "Product", new { id = proId});
        }

        [HttpPost]
        public ActionResult RateProduct(int? proId, int? rateId, int? rate, int? catId, int? page)
        {
            var p = CSDLQLBH.GetRatingsByProduct(proId).FirstOrDefault();
            if (p != null)
            {
                switch (rateId)
                {
                    case 1:
                        if (rate == 1)
                        {
                            p.One += 1;
                        }
                        else
                        {
                            p.One -= 1;
                        }
                        break;
                    case 2:
                        if (rate == 1)
                        {
                            p.Two += 1;
                        }
                        else
                        {
                            p.Two -= 1;
                        }
                        break;
                    case 3:
                        if (rate == 1)
                        {
                            p.Three += 1;
                        }
                        else
                        {
                            p.Three -= 1;
                        }
                        break;
                    case 4:
                        if (rate == 1)
                        {
                            p.Four += 1;
                        }
                        else
                        {
                            p.Four -= 1;
                        }
                        break;
                    case 5:
                        if (rate == 1)
                        {
                            p.Five += 1;
                        }
                        else
                        {
                            p.Five -= 1;
                        }
                        break;
                }
                p.Rate = ((p.One * 1) + (p.Two * 2) + (p.Three * 3) + (p.Four * 4) + (p.Five * 5)) / (p.One + p.Two + p.Three + p.Four + p.Five);

                CSDLQLBH.UpdateRating(p);
            }
            else
            {
                ClientRating ra = new ClientRating
                {
                    ProID = proId,
                    One = 0,
                    Two = 0,
                    Three = 0,
                    Four = 0,
                    Five = 0
                };
                switch (rateId)
                {
                    case 1:
                        ra.One = rate;
                        break;
                    case 2:
                        ra.Two = rate;
                        break;
                    case 3:
                        ra.Three = rate;
                        break;
                    case 4:
                        ra.Four = rate;
                        break;
                    case 5:
                        ra.Five = rate;
                        break;
                }
                ra.Rate = ((ra.One * 1) + (ra.Two * 2) + (ra.Three * 3) + (ra.Four * 4) + (ra.Five * 5)) / (ra.One + ra.Two + ra.Three + ra.Four + ra.Five);

                CSDLQLBH.InsertRating(ra);
            }
            return RedirectToAction("GetListByCategory", "Product", new { id = catId, page = page });
        }

        [HttpPost]
        public ActionResult RateProductIndex(int? proId, int? rateId, int? rate)
        {
            var p = CSDLQLBH.GetRatingsByProduct(proId).FirstOrDefault();
            if (p != null)
            {
                switch (rateId)
                {
                    case 1:
                        if (rate == 1)
                        {
                            p.One += 1;
                        }
                        else
                        {
                            p.One -= 1;
                        }
                        break;
                    case 2:
                        if (rate == 1)
                        {
                            p.Two += 1;
                        }
                        else
                        {
                            p.Two -= 1;
                        }
                        break;
                    case 3:
                        if (rate == 1)
                        {
                            p.Three += 1;
                        }
                        else
                        {
                            p.Three -= 1;
                        }
                        break;
                    case 4:
                        if (rate == 1)
                        {
                            p.Four += 1;
                        }
                        else
                        {
                            p.Four -= 1;
                        }
                        break;
                    case 5:
                        if (rate == 1)
                        {
                            p.Five += 1;
                        }
                        else
                        {
                            p.Five -= 1;
                        }
                        break;
                }
                p.Rate = ((p.One * 1) + (p.Two * 2) + (p.Three * 3) + (p.Four * 4) + (p.Five * 5)) / (p.One + p.Two + p.Three + p.Four + p.Five);

                CSDLQLBH.UpdateRating(p);
            }
            else
            {
                ClientRating ra = new ClientRating
                {
                    ProID = proId,
                    One = 0,
                    Two = 0,
                    Three = 0,
                    Four = 0,
                    Five = 0
                };
                switch (rateId)
                {
                    case 1:
                        ra.One = rate;
                        break;
                    case 2:
                        ra.Two = rate;
                        break;
                    case 3:
                        ra.Three = rate;
                        break;
                    case 4:
                        ra.Four = rate;
                        break;
                    case 5:
                        ra.Five = rate;
                        break;
                }
                ra.Rate = ((ra.One * 1) + (ra.Two * 2) + (ra.Three * 3) + (ra.Four * 4) + (ra.Five * 5)) / (ra.One + ra.Two + ra.Three + ra.Four + ra.Five);

                CSDLQLBH.InsertRating(ra);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}