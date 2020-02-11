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
    public interface IStatuesDAO
    {

        IEnumerable<ApiStatues> GetAll();

        ApiStatues GetSingle(int? id);

        ApiStatues Add(ApiStatues status);

        ApiStatues Update(int? id, ApiStatues status);

        int Delete(int? id);
    }
}
