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
    public interface ICommentsDAO
    {
        IEnumerable<ApiComment> GetAll();

        IEnumerable<ApiComment> GetByProduct(int? proId);

        IEnumerable<ApiComment> GetByUser(int? userId);

        ApiComment Add(ApiComment comment);

        ApiComment Update(ApiComment comment);

        int Delete(int? userId, int? proId);
    }
}
