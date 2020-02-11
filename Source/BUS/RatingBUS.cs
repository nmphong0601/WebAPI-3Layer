using DAO.Factory;
using DTO.ApiObjects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    class RatingBUS
    {
        private RatingDAO ratingFactory = new RatingDAO();

        public IEnumerable<ApiRating> GetAll()
        {
            return ratingFactory.GetAll();
        }

        public IEnumerable<ApiRating> GetByProduct(int? proId)
        {
            return ratingFactory.GetByProductId(proId);
        }

        public ApiRating Add(ApiRating rating)
        {
            return ratingFactory.Add(rating);
        }

        public ApiRating Update(ApiRating rating)
        {
            return ratingFactory.Update(rating);
        }

        public int Delete(int? proId)
        {
            return ratingFactory.Delete(proId);
        }
    }
}
