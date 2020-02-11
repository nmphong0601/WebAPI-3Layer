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
    public class StatuesDAO: IStatuesDAO
    {
        private QLBH_WebEntities db = new QLBH_WebEntities();

        public IEnumerable<ApiStatues> GetAll()
        {
            return Mapper.Map<IEnumerable<Status>, IEnumerable<ApiStatues>>(db.Statuses.ToList());
        }

        public ApiStatues GetSingle(int? id)
        {
            return Mapper.Map<Status, ApiStatues>(db.Statuses.Where(s => s.SttID == id).FirstOrDefault());
        }

        public ApiStatues Add(ApiStatues apiStatus)
        {
            db.Statuses.Add(Mapper.Map<ApiStatues, Status>(apiStatus));
            apiStatus.SttID = db.SaveChanges();

            return apiStatus;
        }

        public ApiStatues Update(int? id , ApiStatues apiStatus)
        {
            var apiStatusInDB = db.Statuses.Where(s => s.SttID == id).FirstOrDefault();
            if (apiStatusInDB != null)
            {
                apiStatus.SttID = apiStatusInDB.SttID;
                apiStatusInDB = Mapper.Map<ApiStatues, Status>(apiStatus);
                db.Entry(apiStatusInDB).State = System.Data.EntityState.Modified;
                db.SaveChanges();
            }

            return apiStatus;
        }

        public int Delete(int? id)
        {
            using (var dc = new QLBH_WebEntities())
            {
                var apiStatusInDB = db.Statuses.Where(s => s.SttID == id).FirstOrDefault();
                if (apiStatusInDB != null)
                {
                    db.Statuses.Remove(apiStatusInDB);
                    db.Entry(apiStatusInDB).State = System.Data.EntityState.Deleted; 
                }

                return dc.SaveChanges();
            }
        }
    }
}
