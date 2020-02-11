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

        public IEnumerable<ApiRating> GetAll()
        {
            return Mapper.Map<IEnumerable<Rating>, IEnumerable<ApiRating>>(db.Ratings.ToList());
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

        public int Delete(int? proId)
        {
            var ratingInDB = db.Ratings.Where(r => r.ProID == proId).FirstOrDefault();
            if (ratingInDB != null)
            {
                db.Ratings.Remove(ratingInDB);
                db.Entry(ratingInDB).State = System.Data.EntityState.Deleted;
            }

            return db.SaveChanges();
        }
    }
}
