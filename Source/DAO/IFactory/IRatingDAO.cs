using DTO.Models;
using Ultilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.ApiObjects;

namespace DAO.IFactory
{
    public interface IRatingDAO
    {
        IEnumerable<ApiRating> GetAll(string filter = null, string sort = "ProId DESC");

        IEnumerable<ApiRating> Paged(string keyword = null, string filter = null, string sort = "ProId DESC", int page = 1, int pageSize = 6);

        IEnumerable<ApiRating> GetByProductId(int? proId);

        ApiRating Add(ApiRating rating);

        ApiRating Update(ApiRating rating);

        Boolean Delete(int? proId);
    }
}
