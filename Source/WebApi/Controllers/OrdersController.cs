using BUS;
using DTO.ApiObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class OrdersController : BaseApiController
    {
        private OrderBUS service = new OrderBUS();
        //[AuthActionFilter]

        // GET: Collection
        [HttpGet]
        public IEnumerable<ApiOrder> GetAll(string filter = null, string sort = "OrderId DESC")
        {
            IEnumerable<ApiOrder> apiUsers = new List<ApiOrder>();
            try
            {
                apiUsers = service.GetAll(filter, sort);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiUsers;
        }

        // GET: Paging
        [HttpGet]
        [Route("api/Orders/GetPaged")]
        public Dictionary<string, object> GetPaged(string keyword = null, string filter = null, string sort = "OrderId DESC", int page = 1, int pageSize = 6)
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
        public ApiOrder GetSingle(int? id)
        {
            ApiOrder apiOrder = new ApiOrder();
            try
            {
                apiOrder = service.GetSingle(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiOrder;
        }

        //POST: Insert
        [HttpPost]
        public ApiOrder Post([FromBody]ApiOrder apiOrder)
        {
            try
            {
                apiOrder = service.Add(apiOrder);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiOrder;
        }

        //PUST: Update
        [HttpPut]
        public ApiOrder Put([FromBody]ApiOrder apiOrder)
        {
            try
            {
                int? id = apiOrder.OrderID;
                apiOrder = service.Update(id, apiOrder);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return new ApiOrder();
        }

        //DELETE
        [HttpPut]
        public int Delete(int? id)
        {
            return service.Delete(id);
        }
    }
}