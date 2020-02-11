using DAO.Factory;
using DTO.ApiObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    class OrderBUS
    {
        private OrderDAO orderFactory = new OrderDAO();

        public IEnumerable<ApiOrder> GetAll()
        {
            return orderFactory.GetAll();
        }

        public ApiOrder GetSingle(int? id)
        {
            return orderFactory.GetSingle(id);
        }

        public ApiOrder Add(ApiOrder order)
        {
            return orderFactory.Add(order);
        }

        public ApiOrder Update(int? id , ApiOrder order)
        {
            return orderFactory.Update(id, order);
        }

        public int Delete(int? id)
        {
            return orderFactory.Delete(id);
        }
    }
}
