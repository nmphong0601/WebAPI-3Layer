using DTO.Models;
using Ultilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.IFactory;
using DTO.ApiObjects;
using AutoMapper;

namespace DAO.Factory
{
    public class ProductsDAO: IProductsDAO
    {
        private QLBH_WebEntities db = new QLBH_WebEntities();

        public IEnumerable<ApiProduct> GetAll(string filter = null, string sort = "ProID DESC")
        {
            var sqlStr = "Select * from Products" + (filter != null ? " where " + filter + " ORDER BY " + sort : " ORDER BY " + sort);

            var products = db.Products.SqlQuery(sqlStr).ToList();

            var apiProducts = Mapper.Map<List<Product>, List<ApiProduct>>(products);

            return apiProducts;
        }

        public IEnumerable<ApiProduct> Paged(string keyword = null, string filter = null, string sort = "ProID DESC", int page = 1, int pageSize = 6)
        {
            var apiProducts = GetAll(filter, sort).Where(p => p.ProName.Contains(keyword))
                 .Skip((page - 1) * pageSize)
                 .Take(pageSize)
                 .ToList();

            return apiProducts;
        }

        public ApiProduct GetSingle(int? id)
        {
            return Mapper.Map<Product, ApiProduct>(db.Products.Where(p => p.ProID == id).FirstOrDefault());
        }

        public ApiProduct Add(ApiProduct apiProduct)
        {
            db.Products.Add(Mapper.Map<ApiProduct, Product>(apiProduct));
            apiProduct.ProID = db.SaveChanges();

            return apiProduct;
        }

        public ApiProduct Update(int? id , ApiProduct apiProduct)
        {
            var productInDB = db.Products.Where(p => p.ProID == id).FirstOrDefault();
            if (productInDB != null)
            {
                apiProduct.ProID = productInDB.ProID;
                productInDB = Mapper.Map<ApiProduct, Product>(apiProduct);
                db.Entry(productInDB).State = System.Data.EntityState.Modified;
                db.SaveChanges();
            }

            return apiProduct;
        }

        public Boolean Delete(int? id)
        {
            var isDelete = false;

            var productInDB = db.Products.Where(p => p.ProID == id).FirstOrDefault();
            if (productInDB != null)
            {
                var comments = db.Comments.Where(c => c.ProID == productInDB.ProID).ToList();
                if (comments.Count != 0)
                {
                    foreach (var cm in comments)
                    {
                        db.Comments.Remove(cm);
                        db.Entry(cm).State = System.Data.EntityState.Deleted;
                    }
                }

                var orderDetails = db.OrderDetails.Where(o => o.ProID == productInDB.ProID).ToList();
                if (orderDetails.Count != 0)
                {
                    foreach (var od in orderDetails)
                    {
                        db.OrderDetails.Remove(od);
                        db.Entry(od).State = System.Data.EntityState.Deleted;
                    }
                }

                isDelete = db.Products.Remove(productInDB) != null ? true : false;
                db.Entry(productInDB).State = System.Data.EntityState.Deleted;

                db.SaveChanges();
            }

            return isDelete;
        }
    }
}
