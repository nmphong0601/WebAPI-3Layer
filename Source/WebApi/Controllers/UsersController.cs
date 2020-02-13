using BUS;
using DTO.ApiObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class UsersController : BaseApiController
    {
        private UserBUS service = new UserBUS();
        //[AuthActionFilter]

        // GET: Collection
        [HttpGet]
        public IEnumerable<ApiUser> GetAll(string filter = null, string sort = "f_ID DESC")
        {
            IEnumerable<ApiUser> apiUsers = new List<ApiUser>();
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
        [Route("api/Users/GetPaged")]
        public Dictionary<string, object> GetPaged(string keyword = null, string filter = null, string sort = "f_ID DESC", int page = 1, int pageSize = 6)
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
        public ApiUser GetSingle(int? id)
        {
            ApiUser apiUser = new ApiUser();
            try
            {
                apiUser = service.GetSingle(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiUser;
        }

        //POST: Insert
        [HttpPost]
        public ApiUser Post([FromBody]ApiUser apiUser)
        {
            try
            {
                apiUser = service.Add(apiUser);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiUser;
        }

        //PUST: Update
        [HttpPut]
        public ApiUser Put(int? id, [FromBody]ApiUser apiUser)
        {
            try
            {
                apiUser = service.Update(id, apiUser);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiUser;
        }

        //DELETE
        [HttpPut]
        public int Delete(int? id)
        {
            return service.Delete(id);
        }
    }
}