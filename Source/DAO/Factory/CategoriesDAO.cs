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
    public class CategoriesDAO: ICategoriesDAO
    {
        private QLBH_WebEntities db = new QLBH_WebEntities();

        public IEnumerable<ApiCategory> GetAll()
        {
            return Mapper.Map<IEnumerable<Category>, IEnumerable<ApiCategory>>(db.Categories.ToList());
        }

        public ApiCategory GetSingle(int? id)
        {
            return Mapper.Map<Category, ApiCategory>(db.Categories.Where(c => c.CatID == id).FirstOrDefault());
        }

        public ApiCategory Add(ApiCategory apiCategory)
        {
            db.Categories.Add(Mapper.Map<ApiCategory, Category>(apiCategory));
            apiCategory.CatID = db.SaveChanges();

            return apiCategory;
        }

        public ApiCategory Update(int? id , ApiCategory apiCategory)
        {
            var categoryInDB = db.Categories.Where(c => c.CatID == id).FirstOrDefault();
            if (categoryInDB != null)
            {
                apiCategory.CatID = categoryInDB.CatID;
                categoryInDB = Mapper.Map<ApiCategory, Category>(apiCategory);
                db.Entry(categoryInDB).State = System.Data.EntityState.Modified;
                db.SaveChanges();
            }

            return apiCategory;
        }

        public int Delete(int? id)
        {
            var categoryInDB = db.Categories.Where(c => c.CatID == id).FirstOrDefault();
            if (categoryInDB != null)
            {
                var products = db.Products.Where(p => p.CatID == categoryInDB.CatID).ToList();
                if (products.Count != 0)
                {
                    foreach (var prod in products)
                    {
                        db.Products.Remove(prod);
                        db.Entry(prod).State = System.Data.EntityState.Deleted;
                    }
                }

                db.Categories.Remove(categoryInDB);
                db.Entry(categoryInDB).State = System.Data.EntityState.Deleted;
            }

            return db.SaveChanges();
        }
    }
}
