using DAO.Factory;
using DTO.ApiObjects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    class OrderDetailsBUS
    {
        private OrderDetailsDAO orderDetailFactory = new OrderDetailsDAO();

        public IEnumerable<ApiOrderDetail> GetAll()
        {
            return orderDetailFactory.GetAll();
        }

        public IEnumerable<ApiOrderDetail> GetByOrder(int? orderId)
        {
            return orderDetailFactory.GetByOrder(orderId);
        }

        public IEnumerable<ApiOrderDetail> GetByProduct(int? proId)
        {
            return orderDetailFactory.GetByProduct(proId);
        }

        public ApiOrderDetail Add(ApiOrderDetail orderDetail)
        {
            return orderDetailFactory.Add(orderDetail);
        }

        public ApiOrderDetail Update(ApiOrderDetail orderDetail)
        {
            return orderDetailFactory.Update(orderDetail);
        }

        public int Delete(int? orderId, int? proId)
        {
            return orderDetailFactory.Delete(orderId, proId);
        }
    }
}
