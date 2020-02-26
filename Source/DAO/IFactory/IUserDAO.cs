using DTO.Models;
using Ultilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.ApiObjects;

namespace DAO.IFactory
{
    public interface IUserDAO
    {
        ApiUserInfo Login(string userName, string password, bool? rememberMe);

        IEnumerable<ApiUser> GetAll(string filter = null, string sort = "f_ID DESC");

        IEnumerable<ApiUser> Paged(string keyword = null, string filter = null, string sort = "Id DESC", int page = 1, int pageSize = 6);

        ApiUser GetSingle(int? id);

        ApiUser GetSingleByUserName(string userName = null);

        ApiUser Add(ApiUser user);

        ApiUser Update(int? id, ApiUser user);

        Boolean Delete(int? id);
    }
}
