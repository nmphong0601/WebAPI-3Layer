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
        IEnumerable<ApiOrder> GetAll();

        ApiOrder GetSingle(int? id);

        ApiOrder Add(ApiOrder order);

        ApiOrder Update(int? id, ApiOrder order);

        int Delete(int? id);
    }
}
