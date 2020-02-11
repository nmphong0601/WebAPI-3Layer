using DAO.Factory;
using DTO.ApiObjects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    class CategoriesBUS
    {
        private CategoriesDAO categoriesFactory = new CategoriesDAO();

        public IEnumerable<ApiCategory> GetAll()
        {
            return categoriesFactory.GetAll();
        }

        public ApiCategory GetSingle(int? id)
        {
            return categoriesFactory.GetSingle(id);
        }

        public ApiCategory Add(ApiCategory category)
        {
            return categoriesFactory.Add(category);
        }

        public ApiCategory Update(int? id , ApiCategory category)
        {
            return categoriesFactory.Update(id, category);
        }

        public int Delete(int? id)
        {
            return categoriesFactory.Delete(id);
        }
    }
}
