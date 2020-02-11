using DTO.Models;
using DTO.ApiObjects;
using Ultilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.IFactory;
using AutoMapper;

namespace DAO.Factory
{
    public class UserDAO: IUserDAO
    {
        private QLBH_WebEntities db = new QLBH_WebEntities();
        private OrderDAO orderFactory = new OrderDAO();

        public ApiUserInfo Login(string userName, string password, bool? rememberMe)
        {
            var userInfor = new ApiUserInfo();
            var passEnscrypt = Ulti.Md5Hash(password);
            var test = db.Users.ToList();
            var user = db.Users.Where(u => u.f_Username == userName && u.f_Password == passEnscrypt).FirstOrDefault();

            if (user != null)
            {

                user.f_Password = "";
                userInfor.Username = userName;
                userInfor.Permission = Ulti.PermissionMapTo(user.f_Permission);
                userInfor.RemeberMe = rememberMe;
                userInfor.FullInfo = Mapper.Map<User, ApiUser>(user);


            }
            else
            {
                throw new Exception("Tên đăng nhập hoặc mật khẩu không đúng");
            }

            return userInfor;
        }

        public IEnumerable<ApiUser> GetAll()
        {
            var users = db.Users.ToList();
            var apiUsers = Mapper.Map<IEnumerable<User>, IEnumerable<ApiUser>>(users);

            return apiUsers;
        }

        public ApiUser GetSingle(int? id)
        {
            var user = db.Users.Where(u => u.f_ID == id).FirstOrDefault();
            var apiUser = Mapper.Map<User, ApiUser>(user);
            return apiUser;
        }

        public ApiUser Add(ApiUser apiUser)
        {
            var user = Mapper.Map<ApiUser, User>(apiUser);
            db.Users.Add(user);
            apiUser.f_ID = db.SaveChanges();

            return apiUser;
        }

        public ApiUser Update(int? id , ApiUser apiUser)
        {
            var userInDB = db.Users.Where(u => u.f_ID == id && u.f_Username == apiUser.f_Username).FirstOrDefault();
            if (userInDB != null)
            {
                apiUser.f_ID = userInDB.f_ID;
                userInDB = Mapper.Map<ApiUser, User>(apiUser);
                db.Entry(userInDB).State = System.Data.EntityState.Modified;
                db.SaveChanges();
            }

            return apiUser;
        }

        public int Delete(int? id)
        {
            var userInDB = db.Users.Where(u => u.f_ID == id).FirstOrDefault();
            if (userInDB != null)
            {
                var orders = db.Orders.Where(o => o.UserID == userInDB.f_ID).ToList();
                if (orders.Count != 0)
                {
                    foreach (var od in orders)
                    {
                        orderFactory.Delete(od.OrderID);
                    }
                }

                var comments = db.Comments.Where(c => c.UserID == userInDB.f_ID).ToList();
                if (comments.Count != 0)
                {
                    foreach (var cm in comments)
                    {
                        db.Comments.Remove(cm);
                        db.Entry(cm).State = System.Data.EntityState.Deleted;
                    }
                }

                db.Users.Remove(userInDB);
                db.Entry(userInDB).State = System.Data.EntityState.Deleted;
            }

            return db.SaveChanges();
        }
    }
}
