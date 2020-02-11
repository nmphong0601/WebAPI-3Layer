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
        IEnumerable<ApiProduct> GetAll();

        ApiProduct GetSingle(int? id);

        ApiProduct Add(ApiProduct product);

        ApiProduct Update(int? id, ApiProduct product);

        int Delete(int? id);
    }
}
