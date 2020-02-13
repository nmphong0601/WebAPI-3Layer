using DAO.Factory;
using DTO.ApiObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class CommentsBUS
    {
        readonly DAOFactory factory;
        public CommentsBUS()
        {
            this.factory = new DAOFactory();
        }

        public CommentsBUS(DAOFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(DAOFactory));
        }

        public IEnumerable<ApiComment> GetAll(string filter = null, string sort = "UserID DESC")
        {
            return factory.CommentsDAO.GetAll(filter, sort);
        }

        public IEnumerable<ApiComment> Paged(string keyword = null, string filter = null, string sort = "UserID DESC", int page = 1, int pageSize = 6)
        {
            return factory.CommentsDAO.Paged(keyword, filter, sort, page, pageSize);
        }

        public IEnumerable<ApiComment> GetByProduct(int? proId)
        {
            return factory.CommentsDAO.GetByProduct(proId);
        }

        public IEnumerable<ApiComment> GetByUser(int? userId)
        {
            return factory.CommentsDAO.GetByUser(userId);
        }

        public ApiComment Add(ApiComment comment)
        {
            return factory.CommentsDAO.Add(comment);
        }

        public ApiComment Update(ApiComment comment)
        {
            return factory.CommentsDAO.Update(comment);
        }

        public int Delete(int? userId, int? proId)
        {
            return factory.CommentsDAO.Delete(userId, proId);
        }
    }
}
