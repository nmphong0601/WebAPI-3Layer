using BUS;
using DTO.ApiObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class RatingsController : BaseApiController
    {
        private RatingBUS service = new RatingBUS();
        //[AuthActionFilter]

        // GET: Collection
        [HttpGet]
        public IEnumerable<ApiRating> GetAll(string filter = null, string sort = "ProID DESC")
        {
            IEnumerable<ApiRating> apiRatings = new List<ApiRating>();
            try
            {
                apiRatings = service.GetAll(filter, sort);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiRatings;
        }

        // GET: Paging
        [HttpGet]
        [Route("api/Ratings/GetPaged")]
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

                var apiRatings = service.Paged(keyword, filter, sort, page, pageSize);

                result["totalPage"] = totalPage;
                result["curPage"] = page;
                result["Collection"] = apiRatings;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        //GET: Gingle
        [HttpGet]
        public ApiRating GetSingle(int? id)
        {
            ApiRating apiRating = new ApiRating();
            try
            {
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiRating;
        }
        
        //GET: By Product
        [HttpGet]
        [Route("api/Ratings/Product")]
        public IEnumerable<ApiRating> GetByProduct(int? proId)
        {
            return service.GetByProduct(proId);
        }

        //POST: Insert
        [HttpPost]
        public ApiRating Post([FromBody]ApiRating apiRating)
        {
            try
            {
                apiRating = service.Add(apiRating);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiRating;
        }

        //PUST: Update
        [HttpPut]
        public ApiRating Put([FromBody]ApiRating apiRating)
        {
            try
            {
                apiRating = service.Update(apiRating);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiRating;
        }

        //DELETE
        [HttpPut]
        public Boolean Delete(int? proId)
        {
            return service.Delete(proId);
        }
    }
}