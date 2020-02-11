using DAO.Factory;
using DTO.ApiObjects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    class ProducersBUS
    {
        private ProducersDAO producerFactory = new ProducersDAO();

        public IEnumerable<ApiProducer> GetAll()
        {
            return producerFactory.GetAll();
        }

        public ApiProducer GetSingle(int? id)
        {
            return producerFactory.GetSingle(id);
        }

        public ApiProducer Add(ApiProducer producer)
        {
            return producerFactory.Add(producer);
        }

        public ApiProducer Update(int? id , ApiProducer producer)
        {
            return producerFactory.Update(id, producer);
        }

        public int Delete(int? id)
        {
            return producerFactory.Delete(id);
        }
    }
}
