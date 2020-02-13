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
    public class OrderDAO: IOrderDAO
    {
        private QLBH_WebEntities db = new QLBH_WebEntities();

        public IEnumerable<ApiOrder> GetAll(string filter = null, string sort = "OrderId DESC")
        {
            var sqlStr = "Select * from Orders" + (filter != null ? " where " + filter + " ORDER BY " + sort : " ORDER BY " + sort);

            var orders = db.Orders.SqlQuery(sqlStr).ToList();

            var apiOrders = Mapper.Map<IEnumerable<Order>, IEnumerable<ApiOrder>>(orders);

            return apiOrders;
        }

        public IEnumerable<ApiOrder> Paged(string keyword = null, string filter = null, string sort = "OrderId DESC", int page = 1, int pageSize = 6)
        {
            var apiOrders = GetAll(filter, sort).Where(o => o.Total.ToString().Contains(keyword))
                 .Skip((page - 1) * pageSize)
                 .Take(pageSize)
                 .ToList();

            return apiOrders;
        }

        public ApiOrder GetSingle(int? id)
        {
            return Mapper.Map<Order, ApiOrder>(db.Orders.Where(o => o.OrderID == id).FirstOrDefault());
        }

        public ApiOrder Add(ApiOrder apiOrder)
        {
            db.Orders.Add(Mapper.Map<ApiOrder, Order>(apiOrder));
            apiOrder.OrderID = db.SaveChanges();

            return apiOrder;
        }

        public ApiOrder Update(int? id , ApiOrder apiOrder)
        {
            var orderInDB = db.Orders.Where(o => o.OrderID == id).FirstOrDefault();
            if (orderInDB != null)
            {
                apiOrder.OrderID = orderInDB.OrderID;
                orderInDB = Mapper.Map<ApiOrder, Order>(apiOrder);
                db.Entry(orderInDB).State = System.Data.EntityState.Modified;
                db.SaveChanges();
            }

            return apiOrder;
        }

        public int Delete(int? id)
        {
            var orderInDB = db.Orders.Where(o => o.OrderID == id).FirstOrDefault();
            if (orderInDB != null)
            {
                var orderDetails = db.OrderDetails.Where(o => o.OrderID == orderInDB.OrderID).ToList();
                if (orderDetails.Count != 0)
                {
                    foreach (var od in orderDetails)
                    {
                        db.OrderDetails.Remove(od);
                        db.Entry(od).State = System.Data.EntityState.Deleted;
                    }
                }

                db.Orders.Remove(orderInDB);
                db.Entry(orderInDB).State = System.Data.EntityState.Deleted;
            }

            return db.SaveChanges();
        }
    }
}
