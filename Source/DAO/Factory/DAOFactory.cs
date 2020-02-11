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
            CreateMap<ApiUser, User>().ReverseMap();
            CreateMap<User, ApiUser>().ReverseMap();

            CreateMap<ApiStatues, Status>().ReverseMap();
            CreateMap<Status, ApiStatues>().ReverseMap();

            CreateMap<ApiRating, Rating>().ReverseMap();
            CreateMap<Rating, ApiRating>().ReverseMap();

            CreateMap<ApiProduct, Product>().ReverseMap();
            CreateMap<Product, ApiProduct>().ReverseMap();

            CreateMap<ApiProducer, Producer>().ReverseMap();
            CreateMap<Producer, ApiProducer>().ReverseMap();

            CreateMap<ApiOrderDetail, OrderDetail>().ReverseMap();
            CreateMap<OrderDetail, ApiOrderDetail>().ReverseMap();

            CreateMap<ApiOrder, Order>().ReverseMap();
            CreateMap<Order, ApiOrder>().ReverseMap();

            CreateMap<ApiComment, Comment>().ReverseMap();
            CreateMap<Comment, ApiComment>().ReverseMap();

            CreateMap<ApiCategory, Category>().ReverseMap();
            CreateMap<Category, ApiCategory>().ReverseMap();

            CreateMap<ApiCart, Cart>().ReverseMap();
            CreateMap<Cart, ApiCart>().ReverseMap();

            CreateMap<ApiCartItem, CartItem>().ReverseMap();
            CreateMap<CartItem, ApiCartItem>().ReverseMap();
        }
    }
}
