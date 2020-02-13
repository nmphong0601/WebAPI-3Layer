using BUS;
using DTO.ApiObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class OrderDetailsController : BaseApiController
    {
        private OrderDetailsBUS service = new OrderDetailsBUS();
        //[AuthActionFilter]

        // GET: Collection
        [HttpGet]
        public IEnumerable<ApiOrderDetail> GetAll(string filter = null, string sort = "OrderId DESC")
        {
            IEnumerable<ApiOrderDetail> apiOrderDetails = new List<ApiOrderDetail>();
            try
            {
                apiOrderDetails = service.GetAll(filter, sort);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiOrderDetails;
        }

        // GET: Paging
        [HttpGet]
        [Route("api/OrderDetails/GetPaged")]
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
        public ApiOrderDetail GetSingle(int? id)
        {
            ApiOrderDetail apiOrderDetail = new ApiOrderDetail();
            try
            {
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiOrderDetail;
        }

        //GET: By order
        [HttpGet]
        [Route("api/OrderDetails/Order")]
        public IEnumerable<ApiOrderDetail> GetByOrder(int? orderId)
        {
            IEnumerable<ApiOrderDetail> apiOrderDetails = new List<ApiOrderDetail>();
            try
            {
                apiOrderDetails = service.GetByOrder(orderId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiOrderDetails;
        }

        //GET: By product
        [HttpGet]
        [Route("api/OrderDetails/Product")]
        public IEnumerable<ApiOrderDetail> GetByProduct(int? productId)
        {
            IEnumerable<ApiOrderDetail> apiOrderDetails = new List<ApiOrderDetail>();
            try
            {
                apiOrderDetails = service.GetByProduct(productId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiOrderDetails;
        }

        //POST: Insert
        [HttpPost]
        public ApiOrderDetail Post([FromBody]ApiOrderDetail apiOrderDetail)
        {
            try
            {
                apiOrderDetail = service.Add(apiOrderDetail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiOrderDetail;
        }

        //PUST: Update
        [HttpPut]
        public ApiOrderDetail Put([FromBody]ApiOrderDetail apiOrderDetail)
        {
            try
            {
                apiOrderDetail = service.Update(apiOrderDetail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiOrderDetail;
        }

        //DELETE
        [HttpPut]
        public ApiOrderDetail Delete(int? id)
        {
            return new ApiOrderDetail();
        }
    }
}