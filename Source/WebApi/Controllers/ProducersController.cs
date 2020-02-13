using BUS;
using DTO.ApiObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class ProducersController : BaseApiController
    {
        private ProducersBUS service = new ProducersBUS();
        //[AuthActionFilter]

        // GET: Collection
        [HttpGet]
        public IEnumerable<ApiProducer> GetAll(string filter = null, string sort = "ProducerID DESC")
        {
            IEnumerable<ApiProducer> apiProducers = new List<ApiProducer>();
            try
            {
                apiProducers = service.GetAll(filter, sort);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiProducers;
        }

        // GET: Paging
        [HttpGet]
        [Route("api/Producers/GetPaged")]
        public Dictionary<string, object> GetPaged(string keyword = null, string filter = null, string sort = "ProducerID DESC", int page = 1, int pageSize = 6)
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
        public ApiProducer GetSingle(int? id)
        {
            ApiProducer apiProducer = new ApiProducer();
            try
            {
                apiProducer = service.GetSingle(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiProducer;
        }

        //POST: Insert
        [HttpPost]
        public ApiProducer Post([FromBody]ApiProducer apiProducer)
        {
            try
            {
                apiProducer = service.Add(apiProducer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiProducer;
        }

        //PUST: Update
        [HttpPut]
        public ApiProducer Put(int? id, [FromBody]ApiProducer apiProducer)
        {
            try
            {
                apiProducer = service.Update(id, apiProducer);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiProducer;
        }

        //DELETE
        [HttpPut]
        public int Delete(int? id)
        {
            return service.Delete(id);
        }
    }
}