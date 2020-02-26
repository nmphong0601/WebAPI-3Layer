using DAO.Factory;
using DTO.ApiObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class OrderBUS
    {
        readonly DAOFactory factory;
        public OrderBUS()
        {
            this.factory = new DAOFactory();
        }

        public OrderBUS(DAOFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(DAOFactory));
        }

        public IEnumerable<ApiOrder> GetAll(string filter = null, string sort = "OrderId DESC")
        {
            return factory.OrderDAO.GetAll(filter, sort);
        }

        public IEnumerable<ApiOrder> Paged(string keyword = null, string filter = null, string sort = "OrderId DESC", int page = 1, int pageSize = 6)
        {
            return factory.OrderDAO.Paged(keyword, filter, sort, page, pageSize);
        }

        public ApiOrder GetSingle(int? id)
        {
            return factory.OrderDAO.GetSingle(id);
        }

        public ApiOrder Add(ApiOrder order)
        {
            return factory.OrderDAO.Add(order);
        }

        public ApiOrder Update(int? id , ApiOrder order)
        {
            return factory.OrderDAO.Update(id, order);
        }

        public Boolean Delete(int? id)
        {
            return factory.OrderDAO.Delete(id);
        }
    }
}
