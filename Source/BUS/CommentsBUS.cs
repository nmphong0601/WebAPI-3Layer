using DAO.Factory;
using DTO.ApiObjects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    class CommentsBUS
    {
        private CommentsDAO commentsFactory = new CommentsDAO();

        public IEnumerable<ApiComment> GetAll()
        {
            return commentsFactory.GetAll();
        }

        public IEnumerable<ApiComment> GetByProduct(int? proId)
        {
            return commentsFactory.GetByProduct(proId);
        }

        public IEnumerable<ApiComment> GetByUser(int? userId)
        {
            return commentsFactory.GetByUser(userId);
        }

        public ApiComment Add(ApiComment comment)
        {
            return commentsFactory.Add(comment);
        }

        public ApiComment Update(ApiComment comment)
        {
            return commentsFactory.Update(comment);
        }

        public int Delete(int? userId, int? proId)
        {
            return commentsFactory.Delete(userId, proId);
        }
    }
}
