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
    public class CommentsDAO: ICommentsDAO
    {
        private QLBH_WebEntities db = new QLBH_WebEntities();

        public IEnumerable<ApiComment> GetAll()
        {
            return Mapper.Map<IEnumerable<Comment>, IEnumerable<ApiComment>>(db.Comments.ToList());
        }

        public IEnumerable<ApiComment> GetByProduct(int? proId)
        {
            return Mapper.Map<IEnumerable<Comment>, IEnumerable<ApiComment>>(db.Comments.Where(c => c.ProID == proId));
        }

        public IEnumerable<ApiComment> GetByUser(int? userId)
        {
            return Mapper.Map<IEnumerable<Comment>, IEnumerable<ApiComment>>(db.Comments.Where(c => c.ProID == userId));
        }

        public ApiComment Add(ApiComment apiComment)
        {
            db.Comments.Add(Mapper.Map<ApiComment, Comment>(apiComment));
            db.SaveChanges();

            return apiComment;
        }

        public ApiComment Update(ApiComment apiComment)
        {
            var commentInDB = db.Comments.Where(c => c.UserID == apiComment.UserID && c.ProID == apiComment.ProID).FirstOrDefault();
            if (commentInDB != null)
            {
                commentInDB = Mapper.Map<ApiComment, Comment>(apiComment);
                db.Entry(commentInDB).State = System.Data.EntityState.Modified;
                db.SaveChanges();
            }

            return apiComment;
        }

        public int Delete(int? userId, int? proId)
        {
            var commentInDB = db.Comments.Where(c => c.UserID == userId && c.ProID == proId).FirstOrDefault();
            if (commentInDB != null)
            {
                db.Comments.Remove(commentInDB);
                db.Entry(commentInDB).State = System.Data.EntityState.Deleted;
            }

            return db.SaveChanges();
        }
    }
}
