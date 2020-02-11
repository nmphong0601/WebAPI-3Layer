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

        IEnumerable<ApiUser> GetAll();

        ApiUser GetSingle(int? id);

        ApiUser Add(ApiUser user);

        ApiUser Update(int? id, ApiUser user);

        int Delete(int? id);
    }
}
