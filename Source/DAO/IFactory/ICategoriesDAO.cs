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
    public interface ICategoriesDAO
    {
        IEnumerable<ApiCategory> GetAll();

        ApiCategory GetSingle(int? id);

        ApiCategory Add(ApiCategory category);

        ApiCategory Update(int? id, ApiCategory category);

        int Delete(int? id);
    }
}
