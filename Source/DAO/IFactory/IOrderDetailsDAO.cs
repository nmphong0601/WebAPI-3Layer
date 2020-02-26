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
    public interface IOrderDetailsDAO
    {
        IEnumerable<ApiOrderDetail> GetAll(string filter = null, string sort = "OrderId DESC");

        IEnumerable<ApiOrderDetail> Paged(string keyword = null, string filter = null, string sort = "OrderId DESC", int page = 1, int pageSize = 6);

        IEnumerable<ApiOrderDetail> GetByOrder(int? orderId);

        IEnumerable<ApiOrderDetail> GetByProduct(int? proId);

        ApiOrderDetail Add(ApiOrderDetail orderDetail);

        ApiOrderDetail Update(ApiOrderDetail orderDetail);

        Boolean Delete(int? orderId, int? proId);
    }
}
