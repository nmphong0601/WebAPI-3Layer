using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DoAnWeb.Caching;
using DoAnWeb.ClientModels;
using DoAnWeb.Models;

namespace DoAnWeb.Controllers
{
    public class SearchController : Controller
    {
        static int nPerPage = 6;
        // GET: Search
        public ActionResult SearchProduct(string keyword, int page = 1)
        {
            ViewBag.Keyword = keyword;
            List<ClientProduct> listPro = new List<ClientProduct>();

            Dictionary<string, object> result = CSDLQLBH.GetPagedProducts(keyword: keyword, page: page, pageSize: nPerPage);

            ViewBag.totalPage = result["totalPage"];
            ViewBag.curPage = result["curPage"];

            listPro = result["Collection"] as List<ClientProduct>;

            return  View("Search", listPro);
        }
        public ActionResult GetListProduct(decimal? PriceMin, decimal? PriceMax, int? CatID, int? ProducerID, int page = 1)
        {
            ViewBag.PriceMin = PriceMin;
            ViewBag.PriceMax = PriceMax;
            ViewBag.CatID = CatID;
            ViewBag.ProducerID = ProducerID;

            var List = CSDLQLBH.GetProducts();
            var ListProduct = List.ToList();


            if (PriceMin != null)
            {
                ListProduct = List.Where(p => p.Price > PriceMin).ToList();
            }

            if(PriceMax != null)
            {
                ListProduct = ListProduct.Where(p => p.Price < PriceMax).ToList();
            }

            if(CatID != null)
            {
                ListProduct = ListProduct.Where(p => p.CatID == CatID).ToList();
            }

            if(ProducerID != null)
            {
                ListProduct = ListProduct.Where(p => p.ProducerID == ProducerID).ToList();
            }


            int totalP = ListProduct.Count();
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
            ListProduct = ListProduct.OrderBy(p => p.ProID)
                    .Skip((page - 1) * nPerPage)
                    .Take(nPerPage)
                    .ToList();

            return View("SearchNangCao", ListProduct);
        }
    }
}