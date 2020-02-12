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
            //CreateMap<ApiUser, User>().ForMember(dest => dest.Orders, conf => conf.MapFrom(src => src.Orders))
            //                          .ForMember(dest => dest.Comments, conf => conf.MapFrom(src => src.Comments));
            //CreateMap<User, ApiUser>().ForMember(dest => dest.Orders, conf => conf.MapFrom(src => src.Orders))
            //                          .ForMember(dest => dest.Comments, conf => conf.MapFrom(src => src.Comments));

            //CreateMap<ApiUserInfo, UserInfo>().ForMember(dest => dest.FullInfo, conf => conf.MapFrom(src => src.FullInfo));
            //CreateMap<UserInfo, ApiUserInfo>().ForMember(dest => dest.FullInfo, conf => conf.MapFrom(src => src.FullInfo));

            //CreateMap<ApiStatues, Status>().ForMember(dest => dest.Orders, conf => conf.MapFrom(src => src.Orders));
            //CreateMap<Status, ApiStatues>().ForMember(dest => dest.Orders, conf => conf.MapFrom(src => src.Orders));

            //CreateMap<ApiRating, Rating>().ForMember(dest => dest.Product, conf => conf.MapFrom(src => src.Product));
            //CreateMap<Rating, ApiRating>().ForMember(dest => dest.Product, conf => conf.MapFrom(src => src.Product));

            //CreateMap<ApiProduct, Product>().ForMember(dest => dest.Rating, conf => conf.MapFrom(src => src.Rating))
            //                                .ForMember(dest => dest.Comment, conf => conf.MapFrom(src => src.Comment))
            //                                .ForMember(dest => dest.Category, conf => conf.MapFrom(src => src.Category))
            //                                .ForMember(dest => dest.Producer, conf => conf.MapFrom(src => src.Producer))
            //                                .ForMember(dest => dest.OrderDetails, conf => conf.MapFrom(src => src.OrderDetails));
            //CreateMap<Product, ApiProduct>().ForMember(dest => dest.Rating, conf => conf.MapFrom(src => src.Rating))
            //                                .ForMember(dest => dest.Comment, conf => conf.MapFrom(src => src.Comment))
            //                                .ForMember(dest => dest.Category, conf => conf.MapFrom(src => src.Category))
            //                                .ForMember(dest => dest.Producer, conf => conf.MapFrom(src => src.Producer))
            //                                .ForMember(dest => dest.OrderDetails, conf => conf.MapFrom(src => src.OrderDetails));

            //CreateMap<ApiProducer, Producer>().ForMember(dest => dest.Products, conf => conf.MapFrom(src => src.Products));
            //CreateMap<Producer, ApiProducer>().ForMember(dest => dest.Products, conf => conf.MapFrom(src => src.Products));

            //CreateMap<ApiOrderDetail, OrderDetail>().ForMember(dest => dest.Order, conf => conf.MapFrom(src => src.Order))
            //                                        .ForMember(dest => dest.Product, conf => conf.MapFrom(src => src.Product));
            //CreateMap<OrderDetail, ApiOrderDetail>().ForMember(dest => dest.Order, conf => conf.MapFrom(src => src.Order))
            //                                        .ForMember(dest => dest.Product, conf => conf.MapFrom(src => src.Product));

            //CreateMap<ApiOrder, Order>().ForMember(dest => dest.User, conf => conf.MapFrom(src => src.User))
            //                            .ForMember(dest => dest.Status, conf => conf.MapFrom(src => src.Status))
            //                            .ForMember(dest => dest.OrderDetails, conf => conf.MapFrom(src => src.OrderDetails));
            //CreateMap<Order, ApiOrder>().ForMember(dest => dest.User, conf => conf.MapFrom(src => src.User))
            //                            .ForMember(dest => dest.Status, conf => conf.MapFrom(src => src.Status))
            //                            .ForMember(dest => dest.OrderDetails, conf => conf.MapFrom(src => src.OrderDetails));

            //CreateMap<ApiComment, Comment>().ForMember(dest => dest.User, conf => conf.MapFrom(src => src.User))
            //                                .ForMember(dest => dest.Product, conf => conf.MapFrom(src => src.Product));
            //CreateMap<Comment, ApiComment>().ForMember(dest => dest.User, conf => conf.MapFrom(src => src.User))
            //                                .ForMember(dest => dest.Product, conf => conf.MapFrom(src => src.Product));

            //CreateMap<ApiCategory, Category>().ForMember(dest => dest.Products, conf => conf.MapFrom(src => src.Products));
            //CreateMap<Category, ApiCategory>().ForMember(dest => dest.Products, conf => conf.MapFrom(src => src.Products));

            //CreateMap<ApiCart, Cart>().ReverseMap();
            //CreateMap<Cart, ApiCart>().ReverseMap();

            //CreateMap<ApiCartItem, CartItem>().ReverseMap();
            //CreateMap<CartItem, ApiCartItem>().ReverseMap();


            CreateMap<ApiUser, User>().ForMember(dest => dest.Orders, option => option.Ignore())// Bỏ qua map Collection<Model> property
                                      .ForMember(dest => dest.Comments, option => option.Ignore());
            CreateMap<User, ApiUser>().ForMember(dest => dest.Orders, option => option.Ignore())
                                      .ForMember(dest => dest.Comments, option => option.Ignore());

            CreateMap<ApiUserInfo, UserInfo>().ForMember(dest => dest.FullInfo, conf => conf.MapFrom(src => src.FullInfo));// Map Model property
            CreateMap<UserInfo, ApiUserInfo>().ForMember(dest => dest.FullInfo, conf => conf.MapFrom(src => src.FullInfo));

            CreateMap<ApiStatues, Status>();
            CreateMap<Status, ApiStatues>();

            CreateMap<ApiRating, Rating>().ForMember(dest => dest.Product, conf => conf.MapFrom(src => src.Product));
            CreateMap<Rating, ApiRating>().ForMember(dest => dest.Product, conf => conf.MapFrom(src => src.Product));

            CreateMap<ApiProduct, Product>().ForMember(dest => dest.OrderDetails, option => option.Ignore())
                                            .ForMember(dest => dest.Rating, conf => conf.MapFrom(src => src.Rating))
                                            .ForMember(dest => dest.Comment, conf => conf.MapFrom(src => src.Comment))
                                            .ForMember(dest => dest.Category, conf => conf.MapFrom(src => src.Category))
                                            .ForMember(dest => dest.Producer, conf => conf.MapFrom(src => src.Producer));
            CreateMap<Product, ApiProduct>().ForMember(dest => dest.OrderDetails, option => option.Ignore())
                                            .ForMember(dest => dest.Rating, conf => conf.MapFrom(src => src.Rating))
                                            .ForMember(dest => dest.Comment, conf => conf.MapFrom(src => src.Comment))
                                            .ForMember(dest => dest.Category, conf => conf.MapFrom(src => src.Category))
                                            .ForMember(dest => dest.Producer, conf => conf.MapFrom(src => src.Producer));

            CreateMap<ApiProducer, Producer>().ForMember(dest => dest.Products, option => option.Ignore());
            CreateMap<Producer, ApiProducer>().ForMember(dest => dest.Products, option => option.Ignore());

            CreateMap<ApiOrderDetail, OrderDetail>().ForMember(dest => dest.Order, conf => conf.MapFrom(src => src.Order))
                                                    .ForMember(dest => dest.Product, conf => conf.MapFrom(src => src.Product));
            CreateMap<OrderDetail, ApiOrderDetail>().ForMember(dest => dest.Order, conf => conf.MapFrom(src => src.Order))
                                                    .ForMember(dest => dest.Product, conf => conf.MapFrom(src => src.Product));

            CreateMap<ApiOrder, Order>().ForMember(dest => dest.OrderDetails, option => option.Ignore())
                                        .ForMember(dest => dest.User, conf => conf.MapFrom(src => src.User))
                                        .ForMember(dest => dest.Status, conf => conf.MapFrom(src => src.Status));
            CreateMap<Order, ApiOrder>().ForMember(dest => dest.OrderDetails, option => option.Ignore())
                                        .ForMember(dest => dest.User, conf => conf.MapFrom(src => src.User))
                                        .ForMember(dest => dest.Status, conf => conf.MapFrom(src => src.Status));

            CreateMap<ApiComment, Comment>().ForMember(dest => dest.User, conf => conf.MapFrom(src => src.User))
                                            .ForMember(dest => dest.Product, conf => conf.MapFrom(src => src.Product));
            CreateMap<Comment, ApiComment>().ForMember(dest => dest.User, conf => conf.MapFrom(src => src.User))
                                            .ForMember(dest => dest.Product, conf => conf.MapFrom(src => src.Product));

            CreateMap<ApiCategory, Category>().ForMember(dest => dest.Products, option => option.Ignore());
            CreateMap<Category, ApiCategory>().ForMember(dest => dest.Products, option => option.Ignore());

            CreateMap<ApiCart, Cart>().ReverseMap();
            CreateMap<Cart, ApiCart>().ReverseMap();

            CreateMap<ApiCartItem, CartItem>().ReverseMap();
            CreateMap<CartItem, ApiCartItem>().ReverseMap();
        }
    }
}
