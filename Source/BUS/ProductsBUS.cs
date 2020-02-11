using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Factory;
using DTO.ApiObjects;

namespace BUS
{
    class ProductsBUS
    {
        private ProductsDAO productsFactory = new ProductsDAO();

        public IEnumerable<ApiProduct> GetAll()
        {
            return productsFactory.GetAll();
        }

        public ApiProduct GetSingle(int? id)
        {
            return productsFactory.GetSingle(id);
        }

        public ApiProduct Add(ApiProduct product)
        {
            return productsFactory.Add(product);
        }

        public ApiProduct Update(int? id , ApiProduct product)
        {
            return productsFactory.Update(id, product);
        }

        public int Delete(int? id)
        {
            return productsFactory.Delete(id);
        }
    }
}
