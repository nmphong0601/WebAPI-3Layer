using BUS;
using DTO.ApiObjects;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class AccountsController : BaseApiController
    {
        private UserBUS userService = new UserBUS();
         //[AuthActionFilter]

        // GET: Accounts/Login
        [HttpGet]
        public Dictionary<string, object> Login()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();

            return result;
        }

        //post: Accounts/Login
        [Route("api/Accounts/Login")]
        [HttpPost]
        public ApiUserInfo Login([FromBody]ApiUserInfo ui)
        {
            var userInfor = userService.Login(ui.Username, ui.Password, ui.RemeberMe);

            if (userInfor != null)
            {
                Dictionary<string, object> result = new Dictionary<string, object>();

                return userInfor;
            }

            return new ApiUserInfo();
        }
    }
}