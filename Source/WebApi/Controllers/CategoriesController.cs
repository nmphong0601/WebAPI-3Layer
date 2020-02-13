using BUS;
using DTO.ApiObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class CategoriesController : BaseApiController
    {
        private CategoriesBUS service = new CategoriesBUS();
        //[AuthActionFilter]

        // GET: Collection
        [HttpGet]
        public IEnumerable<ApiCategory> GetAll(string filter = null, string sort = "CatID DESC")
        {
            IEnumerable<ApiCategory> apiCategories = new List<ApiCategory>();
            try
            {
                apiCategories = service.GetAll(filter, sort);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiCategories;
        }

        // GET: Paging
        [HttpGet]
        [Route("api/Categories/GetPaged")]
        public Dictionary<string, object> GetPaged(string keyword = null, string filter = null, string sort = "CatID DESC", int page = 1, int pageSize = 6)
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

                var apiUsers = service.Paged(keyword, filter, sort, page, pageSize);

                result["totalPage"] = totalPage;
                result["curPage"] = page;
                result["Collection"] = apiUsers;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        //GET: Gingle
        [HttpGet]
        public ApiCategory GetSingle(int? id)
        {
            ApiCategory apiCategory = new ApiCategory();
            try
            {
                apiCategory = service.GetSingle(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiCategory;
        }

        //POST: Insert
        [HttpPost]
        public ApiCategory Post([FromBody]ApiCategory apiCategorie)
        {
            try
            {
                apiCategorie = service.Add(apiCategorie);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiCategorie;
        }

        //PUST: Update
        [HttpPut]
        public ApiCategory Put(int? id, [FromBody]ApiCategory apiCategorie)
        {
            try
            {
                apiCategorie = service.Update(id, apiCategorie);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiCategorie;
        }

        //DELETE
        [HttpPut]
        public int Delete(int? id)
        {
            return service.Delete(id);
        }
    }
}