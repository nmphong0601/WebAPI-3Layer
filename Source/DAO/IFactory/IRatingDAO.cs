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
        IEnumerable<ApiRating> GetAll();

        IEnumerable<ApiRating> GetByProductId(int? proId);

        ApiRating Add(ApiRating rating);

        ApiRating Update(ApiRating rating);

        int Delete(int? proId);
    }
}
