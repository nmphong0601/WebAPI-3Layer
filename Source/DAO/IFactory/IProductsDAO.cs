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
    public interface IProductsDAO
    {
        IEnumerable<ApiProduct> GetAll(string filter = null, string sort = "ProID DESC");

        IEnumerable<ApiProduct> Paged(string keyword = null, string filter = null, string sort = "Id DESC", int page = 1, int pageSize = 6);

        ApiProduct GetSingle(int? id);

        ApiProduct Add(ApiProduct product);

        ApiProduct Update(int? id, ApiProduct product);

        Boolean Delete(int? id);
    }
}
