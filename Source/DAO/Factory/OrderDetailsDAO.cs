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

        public IEnumerable<ApiOrderDetail> GetAll(string filter = null, string sort = "OrderId DESC")
        {
            var sqlStr = "Select * from OrderDetails" + (filter != null ? " where " + filter + " ORDER BY " + sort : " ORDER BY " + sort);

            var orderDetails = db.OrderDetails.SqlQuery(sqlStr).ToList();

            var apiOrderDetails = Mapper.Map<IEnumerable<OrderDetail>, IEnumerable<ApiOrderDetail>>(orderDetails);

            return apiOrderDetails;
        }

        public IEnumerable<ApiOrderDetail> Paged(string keyword = null, string filter = null, string sort = "OrderId DESC", int page = 1, int pageSize = 6)
        {
            var apiOrderDetails = GetAll(filter, sort).Where(od => od.Price.ToString().Contains(keyword))
                 .Skip((page - 1) * pageSize)
                 .Take(pageSize)
                 .ToList();

            return apiOrderDetails;
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

        public Boolean Delete(int? orderId, int? proId)
        {
            var isDelete = false;

            var orderDetailInDB = db.OrderDetails.Where(o => o.OrderID == orderId && o.ProID == proId).FirstOrDefault();
            if (orderDetailInDB != null)
            {
                isDelete = db.OrderDetails.Remove(orderDetailInDB) != null ? true : false;
                db.Entry(orderDetailInDB).State = System.Data.EntityState.Deleted;

                db.SaveChanges();
            }

            return isDelete;
        }
    }
}
