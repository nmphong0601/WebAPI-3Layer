using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.Factory;
using DTO.ApiObjects;

namespace BUS
{
    public class ProductsBUS
    {
        readonly DAOFactory factory;
        public ProductsBUS()
        {
            this.factory = new DAOFactory();
        }

        public ProductsBUS(DAOFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(DAOFactory));
        }

        public IEnumerable<ApiProduct> GetAll(string filter = null, string sort = "ProID DESC")
        {
            return factory.ProductsDAO.GetAll(filter, sort);
        }

        public IEnumerable<ApiProduct> Paged(string keyword = null, string filter = null, string sort = "Id DESC", int page = 1, int pageSize = 6)
        {
            return factory.ProductsDAO.Paged(keyword, filter, sort, page, pageSize);
        }

        public ApiProduct GetSingle(int? id)
        {
            return factory.ProductsDAO.GetSingle(id);
        }

        public ApiProduct Add(ApiProduct product)
        {
            return factory.ProductsDAO.Add(product);
        }

        public ApiProduct Update(int? id , ApiProduct product)
        {
            return factory.ProductsDAO.Update(id, product);
        }

        public int Delete(int? id)
        {
            return factory.ProductsDAO.Delete(id);
        }
    }
}
