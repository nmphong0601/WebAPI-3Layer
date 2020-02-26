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
    public class RatingDAO: IRatingDAO
    {
        private QLBH_WebEntities db = new QLBH_WebEntities();

        public IEnumerable<ApiRating> GetAll(string filter = null, string sort = "ProId DESC")
        {
            var sqlStr = "Select * from Ratings" + (filter != null ? " where " + filter + " ORDER BY " + sort : " ORDER BY " + sort);

            var ratings = db.Ratings.SqlQuery(sqlStr).ToList();

            var apiRatings = Mapper.Map<IEnumerable<Rating>, IEnumerable<ApiRating>>(ratings);

            return apiRatings;
        }

        public IEnumerable<ApiRating> Paged(string keyword = null, string filter = null, string sort = "ProId DESC", int page = 1, int pageSize = 6)
        {
            var apiRatings = GetAll(filter, sort).Skip((page - 1) * pageSize)
                 .Take(pageSize)
                 .ToList();

            return apiRatings;
        }

        public IEnumerable<ApiRating> GetByProductId(int? proId)
        {
            return Mapper.Map<IEnumerable<Rating>, IEnumerable<ApiRating>>(db.Ratings.Where(r => r.ProID == proId));
        }

        public ApiRating Add(ApiRating apiRating)
        {
            db.Ratings.Add(Mapper.Map<ApiRating, Rating>(apiRating));
            db.SaveChanges();

            return apiRating;
        }

        public ApiRating Update(ApiRating apiRating)
        {
            var ratingInDB = db.Ratings.Where(r => r.ProID == apiRating.ProID).FirstOrDefault();
            if (ratingInDB != null)
            {
                ratingInDB = Mapper.Map<ApiRating, Rating>(apiRating);
                db.Entry(ratingInDB).State = System.Data.EntityState.Modified;
                db.SaveChanges();
            }

            return apiRating;
        }

        public Boolean Delete(int? proId)
        {
            var isDelete = false; 

            var ratingInDB = db.Ratings.Where(r => r.ProID == proId).FirstOrDefault();
            if (ratingInDB != null)
            {
                isDelete = db.Ratings.Remove(ratingInDB) != null ? true : false;
                db.Entry(ratingInDB).State = System.Data.EntityState.Deleted;

                db.SaveChanges();
            }

            return isDelete;
        }
    }
}
