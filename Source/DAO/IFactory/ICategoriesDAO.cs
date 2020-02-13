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
        IEnumerable<ApiCategory> GetAll(string filter = null, string sort = "CatID DESC");

        IEnumerable<ApiCategory> Paged(string keyword = null, string filter = null, string sort = "CatId DESC", int page = 1, int pageSize = 6);

        ApiCategory GetSingle(int? id);

        ApiCategory Add(ApiCategory category);

        ApiCategory Update(int? id, ApiCategory category);

        int Delete(int? id);
    }
}
