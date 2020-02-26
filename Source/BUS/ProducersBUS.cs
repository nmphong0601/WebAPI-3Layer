using DAO.Factory;
using DTO.ApiObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class ProducersBUS
    {
        readonly DAOFactory factory;
        public ProducersBUS()
        {
            this.factory = new DAOFactory();
        }

        public ProducersBUS(DAOFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(DAOFactory));
        }

        public IEnumerable<ApiProducer> GetAll(string filter = null, string sort = "ProducerID DESC")
        {
            return factory.ProducersDAO.GetAll(filter, sort);
        }

        public IEnumerable<ApiProducer> Paged(string keyword = null, string filter = null, string sort = "ProducerID DESC", int page = 1, int pageSize = 6)
        {
            return factory.ProducersDAO.Paged(keyword, filter, sort, page, pageSize);
        }

        public ApiProducer GetSingle(int? id)
        {
            return factory.ProducersDAO.GetSingle(id);
        }

        public ApiProducer Add(ApiProducer producer)
        {
            return factory.ProducersDAO.Add(producer);
        }

        public ApiProducer Update(int? id , ApiProducer producer)
        {
            return factory.ProducersDAO.Update(id, producer);
        }

        public Boolean Delete(int? id)
        {
            return factory.ProducersDAO.Delete(id);
        }
    }
}
