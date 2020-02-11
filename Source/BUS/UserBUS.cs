using DTO.ApiObjects;
using DAO.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.IFactory;

namespace BUS
{
    public class UserBUS
    {
        readonly DAOFactory factory;
        public UserBUS()
        {
            this.factory = new DAOFactory();
        }

        public UserBUS(DAOFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(DAOFactory));
        }

        public ApiUserInfo Login(string userName, string password, bool? rememberMe)
        {
            return factory.UserDAO.Login(userName, password, rememberMe);
        }

        public IEnumerable<ApiUser> GetAll()
        {
            return factory.UserDAO.GetAll();
        }

        public ApiUser GetSingle(int? id)
        {
            return factory.UserDAO.GetSingle(id);
        }

        public ApiUser Add(ApiUser user)
        {
            return factory.UserDAO.Add(user);
        }

        public ApiUser Update(int? id, ApiUser user)
        {
            return factory.UserDAO.Update(id, user);
        }

        public int Delete(int? id)
        {
            return factory.UserDAO.Delete(id);
        }
    }
}
