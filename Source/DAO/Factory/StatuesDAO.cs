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

        public IEnumerable<ApiStatuses> GetAll(string filter = null, string sort = "SttID DESC")
        {
            var sqlStr = "Select * from Statuses" + (filter != null ? " where " + filter + " ORDER BY " + sort : " ORDER BY " + sort);

            var statuses = db.Statuses.SqlQuery(sqlStr).ToList();

            var apiStatues = Mapper.Map<IEnumerable<Status>, IEnumerable<ApiStatuses>>(statuses);

            return apiStatues;
        }

        public IEnumerable<ApiStatuses> Paged(string keyword = null, string filter = null, string sort = "SttID DESC", int page = 1, int pageSize = 6)
        {
            var apiStatues = GetAll(filter, sort).Where(s => s.SttName.Contains(keyword))
                 .Skip((page - 1) * pageSize)
                 .Take(pageSize)
                 .ToList();

            return apiStatues;
        }

        public ApiStatuses GetSingle(int? id)
        {
            return Mapper.Map<Status, ApiStatuses>(db.Statuses.Where(s => s.SttID == id).FirstOrDefault());
        }

        public ApiStatuses Add(ApiStatuses apiStatus)
        {
            db.Statuses.Add(Mapper.Map<ApiStatuses, Status>(apiStatus));
            apiStatus.SttID = db.SaveChanges();

            return apiStatus;
        }

        public ApiStatuses Update(int? id , ApiStatuses apiStatus)
        {
            var apiStatusInDB = db.Statuses.Where(s => s.SttID == id).FirstOrDefault();
            if (apiStatusInDB != null)
            {
                apiStatus.SttID = apiStatusInDB.SttID;
                apiStatusInDB = Mapper.Map<ApiStatuses, Status>(apiStatus);
                db.Entry(apiStatusInDB).State = System.Data.EntityState.Modified;
                db.SaveChanges();
            }

            return apiStatus;
        }

        public Boolean Delete(int? id)
        {
            var isDelete = false;

            using (var dc = new QLBH_WebEntities())
            {
                var apiStatusInDB = db.Statuses.Where(s => s.SttID == id).FirstOrDefault();
                if (apiStatusInDB != null)
                {
                    isDelete = db.Statuses.Remove(apiStatusInDB) != null ? true : false;
                    db.Entry(apiStatusInDB).State = System.Data.EntityState.Deleted;

                    db.SaveChanges();
                }

                return isDelete;
            }
        }
    }
}
