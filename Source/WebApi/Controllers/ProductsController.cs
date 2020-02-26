using BUS;
using DTO.ApiObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class ProductsController : BaseApiController
    {
        private ProductsBUS service = new ProductsBUS();
        //[AuthActionFilter]

        // GET: Collection
        [HttpGet]
        public IEnumerable<ApiProduct> GetAll(string filter = null, string sort = "ProID DESC")
        {
            IEnumerable<ApiProduct> apiProducts = new List<ApiProduct>();
            try
            {
                apiProducts = service.GetAll(filter, sort);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiProducts;
        }

        [HttpGet]
        [Route("api/Products/SearchProduct")]
        public Dictionary<string, object> GetPaged(string keyword = null, string filter = null, string sort = "ProID DESC", int page = 1, int pageSize = 6)
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            try
            {
                int total = service.GetAll(filter).Count();
                int totalPage = total / pageSize + (total % pageSize > 0 ? 1 : 0);
                if (page < 1)
                {
                    page = 1;
                }
                if (page > totalPage)
                {
                    page = totalPage;
                }

                var apiProducts = service.Paged(keyword, filter, sort, page, pageSize);

                result["totalPage"] = totalPage;
                result["curPage"] = page;
                result["Collection"] = apiProducts;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        //GET: Gingle
        [HttpGet]
        public ApiProduct GetSingle(int? id)
        {
            ApiProduct apiProduct = new ApiProduct();
            try
            {
                apiProduct = service.GetSingle(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiProduct;
        }

        //POST: Insert
        [HttpPost]
        public ApiProduct Post([FromBody]ApiProduct apiProduct)
        {
            try
            {
                apiProduct = service.Add(apiProduct);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiProduct;
        }

        //PUST: Update
        [HttpPut]
        public ApiProduct Put([FromBody]ApiProduct apiProduct)
        {
            try
            {
                int? id = apiProduct.ProID;
                apiProduct = service.Update(id, apiProduct);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiProduct;
        }

        //DELETE
        [HttpPut]
        public int Delete(int? id)
        {
            return service.Delete(id);
        }
    }
}