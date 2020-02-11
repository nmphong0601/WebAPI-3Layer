using DAO.Factory;
using DTO.ApiObjects;
using DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    class StatuesBUS
    {
        private StatuesDAO statuesFactory = new StatuesDAO();

        public IEnumerable<ApiStatues> GetAll()
        {
            return statuesFactory.GetAll();
        }

        public ApiStatues GetSingle(int? id)
        {
            return statuesFactory.GetSingle(id);
        }

        public ApiStatues Add(ApiStatues status)
        {
            return statuesFactory.Add(status);
        }

        public ApiStatues Update(int? id , ApiStatues status)
        {
            return statuesFactory.Update(id, status);
        }

        public int Delete(int? id)
        {
            return statuesFactory.Delete(id);
        }
    }
}
