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

        IEnumerable<ApiStatuses> GetAll(string filter = null, string sort = "SttID DESC");

        IEnumerable<ApiStatuses> Paged(string keyword = null, string filter = null, string sort = "SttID DESC", int page = 1, int pageSize = 6);

        ApiStatuses GetSingle(int? id);

        ApiStatuses Add(ApiStatuses status);

        ApiStatuses Update(int? id, ApiStatuses status);

        int Delete(int? id);
    }
}
