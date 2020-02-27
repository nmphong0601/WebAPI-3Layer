using BUS;
using DTO.ApiObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class CommentsController : BaseApiController
    {
        private CommentsBUS service = new CommentsBUS();
        //[AuthActionFilter]

        // GET: Collection
        [HttpGet]
        public IEnumerable<ApiComment> GetAll(string filter = null, string sort = "Time DESC")
        {
            IEnumerable<ApiComment> apiComments = new List<ApiComment>();
            try
            {
                apiComments = service.GetAll(filter, sort);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiComments;
        }

        // GET: Paging
        [HttpGet]
        [Route("api/Comments/GetPaged")]
        public Dictionary<string, object> GetPaged(string keyword = null, string filter = null, string sort = "Time DESC", int page = 1, int pageSize = 6)
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

                var apiComments = service.Paged(keyword, filter, sort, page, pageSize);

                result["totalPage"] = totalPage;
                result["curPage"] = page;
                result["Collection"] = apiComments;
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        //GET: Gingle
        [HttpGet]
        public ApiComment GetSingle(int? id)
        {
            ApiComment apiComment = new ApiComment();
            try
            {
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiComment;
        }
        
        //GET: By Product
        [HttpGet]
        [Route("api/Comments/Product")]
        public IEnumerable<ApiComment> GetByProduct(int? proId)
        {
            return service.GetByProduct(proId);
        }

        //GET: By User
        [HttpGet]
        [Route("api/Comments/User")]
        public IEnumerable<ApiComment> GetByUser(int? userId)
        {
            return service.GetByUser(userId);
        }

        //POST: Insert
        [HttpPost]
        public ApiComment Post([FromBody]ApiComment apiComment)
        {
            try
            {
                apiComment = service.Add(apiComment);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiComment;
        }

        //PUST: Update
        [HttpPut]
        public ApiComment Put([FromBody]ApiComment apiComment)
        {
            try
            {
                apiComment = service.Update(apiComment);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiComment;
        }

        //DELETE
        [HttpPut]
        public Boolean Delete(int? userId, int? proId)
        {
            return service.Delete(userId, proId);
        }
    }
}