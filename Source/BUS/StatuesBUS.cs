using DAO.Factory;
using DTO.ApiObjects;
using DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class StatuesBUS
    {
        readonly DAOFactory factory;
        public StatuesBUS()
        {
            this.factory = new DAOFactory();
        }

        public StatuesBUS(DAOFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(DAOFactory));
        }

        public IEnumerable<ApiStatuses> GetAll(string filter = null, string sort = "SttID DESC")
        {
            return factory.StatuesDAO.GetAll(filter, sort);
        }

        public IEnumerable<ApiStatuses> Paged(string keyword = null, string filter = null, string sort = "SttID DESC", int page = 1, int pageSize = 6)
        {
            return factory.StatuesDAO.Paged(keyword, filter, sort, page, pageSize);
        }

        public ApiStatuses GetSingle(int? id)
        {
            return factory.StatuesDAO.GetSingle(id);
        }

        public ApiStatuses Add(ApiStatuses status)
        {
            return factory.StatuesDAO.Add(status);
        }

        public ApiStatuses Update(int? id , ApiStatuses status)
        {
            return factory.StatuesDAO.Update(id, status);
        }

        public int Delete(int? id)
        {
            return factory.StatuesDAO.Delete(id);
        }
    }
}
