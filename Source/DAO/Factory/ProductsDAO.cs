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

        public IEnumerable<ApiProduct> GetAll()
        {
            return Mapper.Map<IEnumerable<Product>, IEnumerable<ApiProduct>>(db.Products.ToList());
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

        public int Delete(int? id)
        {
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

                db.Products.Remove(productInDB);
                db.Entry(productInDB).State = System.Data.EntityState.Deleted;
            }

            return db.SaveChanges();
        }
    }
}
