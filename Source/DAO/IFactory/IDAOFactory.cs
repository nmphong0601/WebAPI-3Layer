using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.IFactory
{
    public class IDAOFactory
    {
        IUserDAO UserDAO { get; }
        IStatuesDAO StatuesDAO { get; }
        IRatingDAO RatingDAO { get; }
        IProductsDAO ProductsDAO { get; }
        IProducersDAO ProducersDAO { get; }
        IOrderDetailsDAO OrderDetailsDAO { get; }
        IOrderDAO OrderDAO { get; }
        ICommentsDAO CommentsDAO { get; }
        ICategoriesDAO CategoriesDAO { get; }
        ICartDAO CartDAO { get; }
    }
}
