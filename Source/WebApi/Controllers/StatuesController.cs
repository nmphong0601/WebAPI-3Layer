using BUS;
using DTO.ApiObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class StatusesController : BaseApiController
    {
        private StatuesBUS service = new StatuesBUS();
        //[AuthActionFilter]

        // GET: Collection
        [HttpGet]
        public IEnumerable<ApiStatuses> GetAll(string filter = null, string sort = "SttID DESC")
        {
            IEnumerable<ApiStatuses> apiStatues = new List<ApiStatuses>();
            try
            {
                apiStatues = service.GetAll(filter, sort);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiStatues;
        }

        // GET: Paging
        [HttpGet]
        [Route("api/Statues/GetPaged")]
        public Dictionary<string, object> GetPaged(string keyword = null, string filter = null, string sort = "SttID DESC", int page = 1, int pageSize = 6)
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

                var apiStatues = service.Paged(keyword, filter, sort, page, pageSize);

                result["totalPage"] = totalPage;
                result["curPage"] = page;
                result["Collection"] = apiStatues;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        //GET: Gingle
        [HttpGet]
        public ApiStatuses GetSingle(int? id)
        {
            ApiStatuses apiStatues = new ApiStatuses();
            try
            {
                apiStatues = service.GetSingle(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiStatues;
        }

        //POST: Insert
        [HttpPost]
        public ApiStatuses Post([FromBody]ApiStatuses apiStatues)
        {
            try
            {
                apiStatues = service.Add(apiStatues);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiStatues;
        }

        //PUST: Update
        [HttpPut]
        public ApiStatuses Put([FromBody]ApiStatuses apiStatues)
        {
            try
            {
                int? id = apiStatues.SttID;
                apiStatues = service.Update(id, apiStatues);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiStatues;
        }

        //DELETE
        [HttpPut]
        public int Delete(int? id)
        {
            return service.Delete(id);
        }
    }
}