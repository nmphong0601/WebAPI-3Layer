using DoAnWeb.Models;
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
        public ActionResult GetListByCategory(int? id,int page=1)
        {

            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }
            using (var dc = new QLBH_WebEntities())
            {
                int totalP = dc.Products.Where(p => p.CatID == id).Count();
                int nPage = totalP / nPerPage+(totalP % nPerPage > 0?1 :0);
                if (page < 1)
                {
                    page = 1;
                }
                if (page > nPage)
                {
                    page = nPage;
                }

                ViewBag.totalPage = nPage;
                ViewBag.curPage=page;
                ViewBag.cId = id;

                int pro1 = dc.Products.Where(n => n.CatID == id).Count();
                var l2 = dc.Products.Where(n => n.CatID == id).ToList();
                if(pro1>1)
                {
                    var l = dc.Products
                   .Where(p => p.CatID == id)
                   .OrderBy(p => p.ProID)
                   .Skip((page - 1) * nPerPage)
                   .Take(nPerPage)
                   .ToList();
                    return View("ListByCategory", l);
                }
               
                return View("ListByCategory", l2);
            }
        
        }

        public ActionResult GetListByCategorySX(int? id, int page = 1)
        {

            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }
            using (var dc = new QLBH_WebEntities())
            {
                int totalP = dc.Products.Where(p => p.ProducerID == id).Count();
                int nPage = totalP / nPerPage + (totalP % nPerPage > 0 ? 1 : 0);
                if (page < 1)
                {
                    page = 1;
                }
                if (page > nPage)
                {
                    page = nPage;
                }

                ViewBag.totalPage = nPage;
                ViewBag.curPage = page;
                ViewBag.cId = id;

                int pro1 = dc.Products.Where(n => n.ProducerID == id).Count();
                var l2 = dc.Products.Where(n => n.ProducerID == id).ToList();
                if (pro1 > 1)
                {
                    var l = dc.Products
                   .Where(p => p.ProducerID == id)
                   .OrderBy(p => p.ProID)
                   .Skip((page - 1) * nPerPage)
                   .Take(nPerPage)
                   .ToList();
                    return View("ListByCategory", l);
                }

                return View("ListByCategory", l2);
            }

        }

        public ActionResult Detail(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index", "Home");
            }
            using (var dc = new QLBH_WebEntities())
            {
                var product = dc.Products.Where(p => p.ProID == id).FirstOrDefault();
                List<Comment> listCom = dc.Comments.Where(com => com.ProID == product.ProID).ToList();
                if(listCom != null)
                {
                    ViewBag.ListComment = listCom;
                }
                return View(product);
            }
        }
        //sản phẩm cùng loại
        public ActionResult ProductTheSame(int CatID,int ProID)
        {

            using (var dc = new QLBH_WebEntities())
            {
                var l = dc.Products.Where(p => p.CatID == CatID && p.ProID != ProID).Take(6)
                       .ToList();
                return PartialView("ProductTheSame", l);
            }
           
        }
        //sản phẩm cùng nhà sản xuất
        public ActionResult ProductAsProducer(int ProducerID, int ProID)
        {

            using (var dc = new QLBH_WebEntities())
            {
                var l = dc.Products.Where(p => p.ProducerID == ProducerID && p.ProID != ProID).Take(5)
                       .ToList();
                return PartialView("ProductAsProducer", l);
            }

        }

        [HttpPost]
        public ActionResult AddComment(int? proId, string uName, string content, string dayCM)
        {
            using (var qlbh = new QLBH_WebEntities())
            {
                User u = qlbh.Users.Where(x => x.f_Username == uName).FirstOrDefault();
                Product p = qlbh.Products.Where(x => x.ProID == proId).FirstOrDefault();
                Comment com = new Comment
                {
                    //User = u,
                    //Product = p,
                    Content = content,
                    Time = DateTime.Parse(dayCM)
                };
                qlbh.Comments.Add(com);
                qlbh.SaveChanges();
            }
            return RedirectToAction("Detail", "Product", new { id = proId});
        }

        [HttpPost]
        public ActionResult RateProduct(int? proId, int? rateId, int? rate, int? catId, int? page)
        {
            using (var qlbh = new QLBH_WebEntities())
            {
                var p = qlbh.Ratings.Where(m => m.ProID == proId).FirstOrDefault();
                if(p != null)
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
                    qlbh.Entry(p).State = EntityState.Modified;
                }
                else
                {
                    var p1 = qlbh.Products.Where(m => m.ProID == proId).FirstOrDefault();
                    Rating ra = new Rating
                    {
                        Product = p1,
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
                    qlbh.Ratings.Add(ra);
                }
                qlbh.SaveChanges();
            }
            return RedirectToAction("GetListByCategory", "Product", new { id = catId, page = page });
        }

        [HttpPost]
        public ActionResult RateProductIndex(int? proId, int? rateId, int? rate)
        {
            using (var qlbh = new QLBH_WebEntities())
            {
                var p = qlbh.Ratings.Where(m => m.ProID == proId).FirstOrDefault();
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
                    qlbh.Entry(p).State = EntityState.Modified;
                }
                else
                {
                    var p1 = qlbh.Products.Where(m => m.ProID == proId).FirstOrDefault();
                    Rating ra = new Rating
                    {
                        Product = p1,
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
                    qlbh.Ratings.Add(ra);
                }
                qlbh.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}