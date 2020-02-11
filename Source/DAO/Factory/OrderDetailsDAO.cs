using DTO.Models;
using Ultilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.IFactory;
using AutoMapper;
using DTO.ApiObjects;

namespace DAO.Factory
{
    public class OrderDetailsDAO: IOrderDetailsDAO
    {
        private QLBH_WebEntities db = new QLBH_WebEntities();

        public IEnumerable<ApiOrderDetail> GetAll()
        {
            return Mapper.Map<IEnumerable<OrderDetail>, IEnumerable<ApiOrderDetail>>(db.OrderDetails);
        }

        public IEnumerable<ApiOrderDetail> GetByOrder(int? orderId)
        {
            return Mapper.Map<IEnumerable<OrderDetail>, IEnumerable<ApiOrderDetail>>(db.OrderDetails.Where(o => o.OrderID == orderId));
        }

        public IEnumerable<ApiOrderDetail> GetByProduct(int? proId)
        {
            return Mapper.Map<IEnumerable<OrderDetail>, IEnumerable<ApiOrderDetail>>(db.OrderDetails.Where(o => o.ProID == proId));
        }

        public ApiOrderDetail Add(ApiOrderDetail apiOrderDetail)
        {
            db.OrderDetails.Add(Mapper.Map<ApiOrderDetail, OrderDetail>(apiOrderDetail));
            db.SaveChanges();

            return apiOrderDetail;
        }

        public ApiOrderDetail Update(ApiOrderDetail apiOrderDetail)
        {
            var orderDetailInDB = db.OrderDetails.Where(o => o.OrderID == apiOrderDetail.OrderID && o.ProID == apiOrderDetail.ProID).FirstOrDefault();
            if (orderDetailInDB != null)
            {
                orderDetailInDB = Mapper.Map<ApiOrderDetail, OrderDetail>(apiOrderDetail);
                db.Entry(orderDetailInDB).State = System.Data.EntityState.Modified;
                db.SaveChanges();
            }

            return Mapper.Map<OrderDetail, ApiOrderDetail>(orderDetailInDB);
        }

        public int Delete(int? orderId, int? proId)
        {
            var orderDetailInDB = db.OrderDetails.Where(o => o.OrderID == orderId && o.ProID == proId).FirstOrDefault();
            if (orderDetailInDB != null)
            {
                db.OrderDetails.Remove(orderDetailInDB);
                db.Entry(orderDetailInDB).State = System.Data.EntityState.Deleted;
            }

            return db.SaveChanges();
        }
    }
}
