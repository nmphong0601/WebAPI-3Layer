using AutoMapper;
using DAO.IFactory;
using DTO.ApiObjects;
using DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAO.Factory
{
    public class DAOFactory: IDAOFactory
    {
        public IUserDAO UserDAO { get { return new UserDAO(); } }

        public IStatuesDAO StatuesDAO { get { return new StatuesDAO(); } }
        public IRatingDAO RatingDAO { get { return new RatingDAO(); } }
        public IProductsDAO ProductsDAO { get { return new ProductsDAO(); } }
        public IProducersDAO ProducersDAO { get { return new ProducersDAO(); } }
        public IOrderDetailsDAO OrderDetailsDAO { get { return new OrderDetailsDAO(); } }
        public IOrderDAO OrderDAO { get { return new OrderDAO(); } }
        public ICommentsDAO CommentsDAO { get { return new CommentsDAO(); } }
        public ICategoriesDAO CategoriesDAO { get { return new CategoriesDAO(); } }
        public ICartDAO CartDAO { get { return new CartDAO(); } }

        static bool initialized = false;
        static DAOFactory()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<ApiMappingProfile>();
            });
        }
    }

    public class ApiMappingProfile : AutoMapper.Profile
    {
        public ApiMappingProfile()
        {
            CreateMap<ApiUser, User>().ForMember(d => d.f_ID, o => o.MapFrom(s => s));
            CreateMap<User, ApiUser>().ForMember(d => d.f_ID, o => o.MapFrom(s => s));

            CreateMap<ApiStatues, Status>();
            CreateMap<Status, ApiStatues>();

            CreateMap<ApiRating, Rating>();
            CreateMap<Rating, ApiRating>();

            CreateMap<ApiProduct, Product>();
            CreateMap<Product, ApiProduct>();

            CreateMap<ApiProducer, Producer>();
            CreateMap<Producer, ApiProducer>();

            CreateMap<ApiOrderDetail, OrderDetail>();
            CreateMap<OrderDetail, ApiOrderDetail>();

            CreateMap<ApiOrder, Order>();
            CreateMap<Order, ApiOrder>();

            CreateMap<ApiComment, Comment>();
            CreateMap<Comment, ApiComment>();

            CreateMap<ApiCategory, Category>();
            CreateMap<Category, ApiCategory>();

            CreateMap<ApiCart, Cart>();
            CreateMap<Cart, ApiCart>();

            CreateMap<ApiCartItem, CartItem>();
            CreateMap<CartItem, ApiCartItem>();
        }
    }
}
