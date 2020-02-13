using DTO.Models;
using Ultilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO.IFactory;
using DTO.ApiObjects;
using AutoMapper;

namespace DAO.Factory
{
    public class ProducersDAO: IProducersDAO
    {
        private QLBH_WebEntities db = new QLBH_WebEntities();

        public IEnumerable<ApiProducer> GetAll(string filter = null, string sort = "ProducerID DESC")
        {
            var sqlStr = "Select * from Producers" + (filter != null ? " where " + filter + " ORDER BY " + sort : " ORDER BY " + sort);

            var producers = db.Producers.SqlQuery(sqlStr).ToList();

            var apiProducers = Mapper.Map<IEnumerable<Producer>, IEnumerable<ApiProducer>>(producers);

            return apiProducers;
        }

        public IEnumerable<ApiProducer> Paged(string keyword = null, string filter = null, string sort = "ProducerID DESC", int page = 1, int pageSize = 6)
        {
            var apiProducers = GetAll(filter, sort).Where(p => p.ProducerName.Contains(keyword))
                 .Skip((page - 1) * pageSize)
                 .Take(pageSize)
                 .ToList();

            return apiProducers;
        }

        public ApiProducer GetSingle(int? id)
        {
            return Mapper.Map<Producer, ApiProducer>(db.Producers.Where(p => p.ProducerID == id).FirstOrDefault());
        }

        public ApiProducer Add(ApiProducer apiProducer)
        {
            db.Producers.Add(Mapper.Map<ApiProducer, Producer>(apiProducer));
            apiProducer.ProducerID = db.SaveChanges();

            return apiProducer;
        }

        public ApiProducer Update(int? id , ApiProducer apiProducer)
        {
            var producerInDB = db.Producers.Where(p => p.ProducerID == id).FirstOrDefault();
            if (producerInDB != null)
            {
                apiProducer.ProducerID = producerInDB.ProducerID;
                producerInDB = Mapper.Map<ApiProducer, Producer>(apiProducer);
                db.Entry(producerInDB).State = System.Data.EntityState.Modified;
                db.SaveChanges();
            }

            return apiProducer;
        }

        public int Delete(int? id)
        {
            var producerInDB = db.Producers.Where(p => p.ProducerID == id).FirstOrDefault();
            if (producerInDB != null)
            {
                var products = db.Products.Where(prod => prod.ProducerID == producerInDB.ProducerID).ToList();
                if (products.Count != 0)
                {
                    foreach (var prod in products)
                    {
                        db.Products.Remove(prod);
                        db.Entry(prod).State = System.Data.EntityState.Deleted;
                    }
                }

                db.Producers.Remove(producerInDB);
                db.Entry(producerInDB).State = System.Data.EntityState.Deleted;
            }

            return db.SaveChanges();
        }
    }
}
