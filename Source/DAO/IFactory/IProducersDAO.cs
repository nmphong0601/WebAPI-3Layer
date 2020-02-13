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
    public interface IProducersDAO
    {
        IEnumerable<ApiProducer> GetAll(string filter = null, string sort = "ProducerID DESC");

        IEnumerable<ApiProducer> Paged(string keyword = null, string filter = null, string sort = "ProducerID DESC", int page = 1, int pageSize = 6);

        ApiProducer GetSingle(int? id);

        ApiProducer Add(ApiProducer producer);

        ApiProducer Update(int? id, ApiProducer producer);

        int Delete(int? id);
    }
}
