using DAO.Factory;
using DTO.ApiObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class OrderDetailsBUS
    {
        readonly DAOFactory factory;
        public OrderDetailsBUS()
        {
            this.factory = new DAOFactory();
        }

        public OrderDetailsBUS(DAOFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(DAOFactory));
        }

        public IEnumerable<ApiOrderDetail> GetAll(string filter = null, string sort = "OrderId DESC")
        {
            return factory.OrderDetailsDAO.GetAll(filter, sort);
        }

        public IEnumerable<ApiOrderDetail> Paged(string keyword = null, string filter = null, string sort = "OrderId DESC", int page = 1, int pageSize = 6)
        {
            return factory.OrderDetailsDAO.Paged(keyword, filter, sort, page, pageSize);
        }

        public IEnumerable<ApiOrderDetail> GetByOrder(int? orderId)
        {
            return factory.OrderDetailsDAO.GetByOrder(orderId);
        }

        public IEnumerable<ApiOrderDetail> GetByProduct(int? proId)
        {
            return factory.OrderDetailsDAO.GetByProduct(proId);
        }

        public ApiOrderDetail Add(ApiOrderDetail orderDetail)
        {
            return factory.OrderDetailsDAO.Add(orderDetail);
        }

        public ApiOrderDetail Update(ApiOrderDetail orderDetail)
        {
            return factory.OrderDetailsDAO.Update(orderDetail);
        }

        public Boolean Delete(int? orderId, int? proId)
        {
            return factory.OrderDetailsDAO.Delete(orderId, proId);
        }
    }
}
