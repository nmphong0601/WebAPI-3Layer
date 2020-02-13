using DAO.Factory;
using DTO.ApiObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class CategoriesBUS
    {
        readonly DAOFactory factory;
        public CategoriesBUS()
        {
            this.factory = new DAOFactory();
        }

        public CategoriesBUS(DAOFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(DAOFactory));
        }

        public IEnumerable<ApiCategory> GetAll(string filter = null, string sort = "CatID DESC")
        {
            return factory.CategoriesDAO.GetAll(filter, sort);
        }

        public IEnumerable<ApiCategory> Paged(string keyword = null, string filter = null, string sort = "CatId DESC", int page = 1, int pageSize = 6)
        {
            return factory.CategoriesDAO.Paged(keyword, filter, sort, page, pageSize);
        }

        public ApiCategory GetSingle(int? id)
        {
            return factory.CategoriesDAO.GetSingle(id);
        }

        public ApiCategory Add(ApiCategory category)
        {
            return factory.CategoriesDAO.Add(category);
        }

        public ApiCategory Update(int? id , ApiCategory category)
        {
            return factory.CategoriesDAO.Update(id, category);
        }

        public int Delete(int? id)
        {
            return factory.CategoriesDAO.Delete(id);
        }
    }
}
