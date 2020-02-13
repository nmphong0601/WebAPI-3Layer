using DAO.Factory;
using DTO.ApiObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class RatingBUS
    {
        readonly DAOFactory factory;
        public RatingBUS()
        {
            this.factory = new DAOFactory();
        }

        public RatingBUS(DAOFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(DAOFactory));
        }

        public IEnumerable<ApiRating> GetAll(string filter = null, string sort = "ProId DESC")
        {
            return factory.RatingDAO.GetAll(filter, sort);
        }

        public IEnumerable<ApiRating> Paged(string keyword = null, string filter = null, string sort = "ProId DESC", int page = 1, int pageSize = 6)
        {
            return factory.RatingDAO.Paged(keyword, filter, sort, page, pageSize);
        }

        public IEnumerable<ApiRating> GetByProduct(int? proId)
        {
            return factory.RatingDAO.GetByProductId(proId);
        }

        public ApiRating Add(ApiRating rating)
        {
            return factory.RatingDAO.Add(rating);
        }

        public ApiRating Update(ApiRating rating)
        {
            return factory.RatingDAO.Update(rating);
        }

        public int Delete(int? proId)
        {
            return factory.RatingDAO.Delete(proId);
        }
    }
}
