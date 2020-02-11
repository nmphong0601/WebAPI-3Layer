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

        public IEnumerable<ApiOrder> GetAll()
        {
            return Mapper.Map<IEnumerable<Order>, IEnumerable<ApiOrder>>(db.Orders.ToList());
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
