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
    public interface IOrderDAO
    {
        IEnumerable<ApiOrder> GetAll(string filter = null, string sort = "OrderId DESC");

        IEnumerable<ApiOrder> Paged(string keyword = null, string filter = null, string sort = "OrderId DESC", int page = 1, int pageSize = 6);

        ApiOrder GetSingle(int? id);

        ApiOrder Add(ApiOrder order);

        ApiOrder Update(int? id, ApiOrder order);

        Boolean Delete(int? id);
    }
}
