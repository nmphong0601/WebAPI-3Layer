using BUS;
using DTO.ApiObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class CartsController : BaseApiController
    {
        private CartsBUS service = new CartsBUS();
        //[AuthActionFilter]

        // GET: Collection
        [HttpGet]
        public Dictionary<string, object> GetAll()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            try
            {

            }
            catch
            {

            }
            return result;
        }

        //GET: Gingle
        [HttpGet]
        public ApiCart GetSingle(int? id)
        {
            try
            {

            }
            catch
            {

            }
            return new ApiCart();
        }

        //POST: Insert
        [HttpPost]
        public ApiCart Post([FromBody]ApiCart apiCart, int productId, int quantity = 1)
        {
            try
            {
                apiCart = service.AddItem(apiCart, productId, quantity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiCart;
        }

        //POST: Checkout
        [HttpPost]
        [Route("api/Carts/Checkout")]
        public ApiCart Checkout([FromBody]ApiCart apiCart)
        {
            try
            {
                apiCart = service.Checkout(apiCart);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiCart;
        }

        //PUST: Update
        [HttpPut]
        public ApiCart Put(int? id, [FromBody]ApiCart apiCart, int productId, int quality)
        {
            try
            {
                apiCart = service.Update(apiCart, productId, quality);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return apiCart;
        }

        //DELETE
        [HttpPut]
        public ApiCart Delete(ApiCart cart, int productId)
        {
            return service.Remove(cart, productId);
        }
    }
}