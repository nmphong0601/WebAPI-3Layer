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
        IEnumerable<ApiProducer> GetAll();

        ApiProducer GetSingle(int? id);

        ApiProducer Add(ApiProducer producer);

        ApiProducer Update(int? id, ApiProducer producer);

        int Delete(int? id);
    }
}
