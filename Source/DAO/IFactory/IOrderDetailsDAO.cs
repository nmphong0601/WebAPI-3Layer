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
        IEnumerable<ApiOrderDetail> GetAll();

        IEnumerable<ApiOrderDetail> GetByOrder(int? orderId);

        IEnumerable<ApiOrderDetail> GetByProduct(int? proId);

        ApiOrderDetail Add(ApiOrderDetail orderDetail);

        ApiOrderDetail Update(ApiOrderDetail orderDetail);

        int Delete(int? orderId, int? proId);
    }
}
