using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Caching;
using DoAnWeb.ClientModels;
using System.Threading.Tasks;
using Newtonsoft.Json;

[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace DoAnWeb.Caching
{
    public class CSDLQLBH
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(CSDLQLBH));
        static string endPointQLBH = System.Configuration.ConfigurationManager.AppSettings["QLBHServerUrl"];
        static string accessToken = string.Empty;

        #region cache management

        static ObjectCache cache = new MemoryCache("CSDLQLBH");

        // clear entire cache
        public static void Clear()
        {
            foreach (var item in cache)
                cache.Remove(item.Key);
        }

        // clears single cache entry
        public static void Clear(string key)
        {
            cache.Remove(key);
        }

        // add to cache helper
        static void Add(string key, object value, DateTimeOffset expiration, CacheItemPriority priority = CacheItemPriority.Default)
        {
            var policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = expiration;
            policy.Priority = priority;

            var item = new CacheItem(key, value);
            cache.Add(item, policy);
        }

        #endregion

        #region Access token

        #endregion

        #region Public static methods

        #region UserInfo

        public static ClientUserInfo Login(ClientUserInfo userInfor)
        {
            try
            {
                var endPointString = endPointQLBH + "api/accounts/login";
                var nguoidung = Task.Run(() => PostAsync<ClientUserInfo>(endPointString, userInfor)).Result;

                return nguoidung;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "Đăng nhập thất bại.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "Đăng nhập thất bại.");
            }
        }

        #endregion

        #region Users

        public static IEnumerable<ClientUser> GetUsers(string filter = null, string sort = "OrderId DESC")
        {
            try
            {
                var endPointString = endPointQLBH + "api/users?filter=" + filter + "&sort=" + sort;
                var nguoidungs = Task.Run(() => GetCachedNullCheckAsync<IEnumerable<ClientUser>>(endPointString)).Result;

                return nguoidungs;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không lấy được dữ liệu người dùng.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không lấy được dữ liệu người dùng.");
            }
        }

        public static ClientUser UserGetSingle(int? id)
        {
            id = id ?? throw new BusinessLayerException(500, "#1001001 Không lấy được dữ liệu người dùng.");

            try
            {
                var endPointString = endPointQLBH + "api/users/" + id;
                var nguoidung = Task.Run(() => GetCachedNullCheckAsync<ClientUser>(endPointString)).Result;

                return nguoidung;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không lấy được dữ liệu người dùng.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không lấy được dữ liệu người dùng.");
            }
        }

        public static ClientUser UserGetSingleByUserName(string userName = null)
        {
            try
            {
                var endPointString = endPointQLBH + "api/users/GetByUserName?userName=" + userName;
                var nguoidung = Task.Run(() => GetCachedNullCheckAsync<ClientUser>(endPointString)).Result;

                return nguoidung;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không lấy được dữ liệu người dùng.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không lấy được dữ liệu người dùng.");
            }
        }

        /// <summary>
        ///   Lấy người dùng theo id, có lưu cache, có kiểm tra null.
        /// </summary>
        /// 
        /// <param name="id">Id của người dùng.</param>
        /// <returns>NguoiDung tương ứng.</returns>
        public static async Task<ClientUser> UserGetSingleAsync(int? id)
        {
            id = id ?? throw new BusinessLayerException(500, "#1001001 Không lấy được dữ liệu người dùng.");

            try
            {
                var endPointString = endPointQLBH + "api/users/" + id;
                var nguoidung = await GetCachedNullCheckAsync<ClientUser>(endPointString);

                return nguoidung;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không lấy được dữ liệu người dùng.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không lấy được dữ liệu người dùng.");
            }
        }

        public static ClientUser InsertUser(ClientUser user)
        {
            try
            {
                var endPointString = endPointQLBH + "api/users";
                var nguoidung = Task.Run(() => PostAsync<ClientUser>(endPointString, user)).Result;

                return nguoidung;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Thêm mới user thất bại.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Thêm mới user thất bại.");
            }
        }

        public static ClientUser UpdateUser(int? id, ClientUser user)
        {
            try
            {
                var endPointString = endPointQLBH + "api/users/" + id;
                var nguoidung = Task.Run(() => PutAsync<ClientUser>(endPointString, user)).Result;

                return nguoidung;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Thêm mới user thất bại.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Thêm mới user thất bại.");
            }
        }

        #endregion

        #region Cart

        public static ClientCart InsertCartItem(ClientCart cart, int proId, int quantity = 1)
        {
            try
            {
                var endPointString = endPointQLBH + "api/carts/Add/Item?productId=" + proId + "&quantity=" + quantity;
                var cartUpdate = Task.Run(() => PostAsync<ClientCart>(endPointString, cart)).Result;

                return cartUpdate;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Cập nhật giỏ hàng thất bại.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Cập nhật giỏ hàng thất bại.");
            }
        }

        public static ClientCart UpdateCartItem(ClientCart cart, int proId, int quantity = 1)
        {
            try
            {
                var endPointString = endPointQLBH + "api/carts/Update/Item?productId=" + proId + "&quantity=" + quantity;
                var cartUpdate = Task.Run(() => PutAsync<ClientCart>(endPointString, cart)).Result;

                return cartUpdate;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Cập nhật giỏ hàng thất bại.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Cập nhật giỏ hàng thất bại.");
            }
        }

        public static ClientCart RemoveCartItem(ClientCart cart, int proId)
        {
            try
            {
                var endPointString = endPointQLBH + "api/carts/Delete/Item?productId=" + proId;
                var cartUpdate = Task.Run(() => PutAsync<ClientCart>(endPointString, cart)).Result;

                return cartUpdate;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Cập nhật giỏ hàng thất bại.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Cập nhật giỏ hàng thất bại.");
            }
        }

        public static ClientCart Checkout(ClientCart cart)
        {
            try
            {
                var endPointString = endPointQLBH + "api/carts/Checkout";
                var cartCheckout = Task.Run(() => PostAsync<ClientCart>(endPointString, cart)).Result;

                return cartCheckout;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Cập nhật giỏ hàng thất bại.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Cập nhật giỏ hàng thất bại.");
            }
        }

        #endregion

        #region Orders

        public static IEnumerable<ClientOrder> GetOrders(string filter = null, string sort = "OrderId DESC")
        {
            try
            {
                var endPointString = endPointQLBH + "api/orders?filter=" + filter + "&sort=" + sort;
                var orders = Task.Run(() => GetCachedNullCheckAsync<IEnumerable<ClientOrder>>(endPointString)).Result;

                return orders;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không lấy được dữ liệu hóa đơn.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không lấy được dữ liệu hóa đơn.");
            }
        }

        public static ClientOrder GetSingleOrder(int? id)
        {
            try
            {
                var endPointString = endPointQLBH + "api/orders/" + id;
                var order = Task.Run(() => GetCachedNullCheckAsync<ClientOrder>(endPointString)).Result;

                return order;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không lấy được dữ liệu hóa đơn.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không lấy được dữ liệu hóa đơn.");
            }
        }

        public static ClientOrder InsertOrders(ClientOrder order)
        {
            try
            {
                var endPointString = endPointQLBH + "api/orders";
                var orders = Task.Run(() => PostAsync<ClientOrder>(endPointString, order)).Result;

                return orders;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không thêm được hóa đơn.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không thêm được hóa đơn.");
            }
        }

        public static ClientOrder UpdateOrders(ClientOrder order)
        {
            try
            {
                var endPointString = endPointQLBH + "api/orders";
                var orderUpdate = Task.Run(() => PutAsync<ClientOrder>(endPointString, order)).Result;

                return orderUpdate;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không cập nhật được hóa đơn.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không cập nhật được hóa đơn.");
            }
        }

        #endregion

        #region Producer

        public static IEnumerable<ClientProducer> GetProducers()
        {
            try
            {
                var endPointString = endPointQLBH + "api/producers";
                var producers = Task.Run(() => GetCachedNullCheckAsync<IEnumerable<ClientProducer>>(endPointString)).Result;

                return producers;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không lấy được dữ liệu nhà sản xuất.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không lấy được dữ liệu nhà sản xuất.");
            }
        }

        public static ClientProducer GetSingleProducer(int? id)
        {
            try
            {
                var endPointString = endPointQLBH + "api/producers/" + id;
                var producer = Task.Run(() => GetCachedNullCheckAsync<ClientProducer>(endPointString)).Result;

                return producer;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không lấy được dữ liệu nhà sản xuất.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không lấy được dữ liệu nhà sản xuất.");
            }
        }

        public static ClientProducer InsertProducer(ClientProducer producer)
        {
            try
            {
                var endPointString = endPointQLBH + "api/producers";
                var producerInsert = Task.Run(() => PostAsync<ClientProducer>(endPointString, producer)).Result;

                return producerInsert;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không lấy được dữ liệu nhà sản xuất.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không lấy được dữ liệu nhà sản xuất.");
            }
        }

        public static ClientProducer UpdateProducer(ClientProducer producer)
        {
            try
            {
                var endPointString = endPointQLBH + "api/producers";
                var producerUpdate = Task.Run(() => PutAsync<ClientProducer>(endPointString, producer)).Result;

                return producerUpdate;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không lấy được dữ liệu nhà sản xuất.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không lấy được dữ liệu nhà sản xuất.");
            }
        }

        public static Boolean RemoveProducer(int? id)
        {
            try
            {
                var endPointString = endPointQLBH + "api/producers/" + id;
                var isDelete = Task.Run(() => DeleteAsync<Boolean>(endPointString)).Result;

                return isDelete;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không lấy được dữ liệu nhà sản xuất.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không lấy được dữ liệu nhà sản xuất.");
            }
        }

        #endregion

        #region Products

        public static IEnumerable<ClientProduct> GetProducts(string filter = null, string sort = "ProID DESC")
        {
            try
            {
                var endPointString = endPointQLBH + "api/products?filter=" + filter + "&sort=" + sort;
                var products = Task.Run(() => GetCachedNullCheckAsync<IEnumerable<ClientProduct>>(endPointString)).Result;

                return products;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không lấy được dữ liệu sản phẩm.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không lấy được dữ liệu sản phẩm.");
            }
        }

        public static Dictionary<string, object> GetPagedProducts(string keyword = null, string filter = null, string sort = "ProID DESC", int page = 1, int pageSize = 6)
        {
            try
            {
                var endPointString = endPointQLBH + "api/products/SearchProduct?keywork="
                                    + keyword + "&filter=" + filter + "&sort=" + sort + "&page=" + page + "&pageSize=" + pageSize;

                var products = Task.Run(() => GetCachedNullCheckAsync<Dictionary<string, object>>(endPointString)).Result;

                return products;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không lấy được dữ liệu sản phẩm.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không lấy được dữ liệu sản phẩm.");
            }
        }

        public static ClientProduct GetSingleProduct(int? id)
        {
            try
            {
                var endPointString = endPointQLBH + "api/products/" + id;
                var product = Task.Run(() => GetCachedNullCheckAsync<ClientProduct>(endPointString)).Result;

                return product;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không lấy được dữ liệu sản phẩm.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không lấy được dữ liệu sản phẩm.");
            }
        }

        public static ClientProduct InsertProduct(ClientProduct product)
        {
            try
            {
                var endPointString = endPointQLBH + "api/products";
                var productUpdate = Task.Run(() => PostAsync<ClientProduct>(endPointString, product)).Result;

                return productUpdate;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không thêm được dữ liệu sản phẩm.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không thêm được dữ liệu sản phẩm.");
            }
        }

        public static ClientProduct UpdateProduct(ClientProduct product)
        {
            try
            {
                var endPointString = endPointQLBH + "api/products";
                var productUpdate = Task.Run(() => PutAsync<ClientProduct>(endPointString, product)).Result;

                return productUpdate;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không cập nhật được dữ liệu sản phẩm.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không cập nhật được dữ liệu sản phẩm.");
            }
        }

        public static Boolean RemoveProduct(int? id)
        {
            try
            {
                var endPointString = endPointQLBH + "api/products/" + id;
                var isDelete = Task.Run(() => DeleteAsync<Boolean>(endPointString)).Result;

                return isDelete;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không cập nhật được dữ liệu sản phẩm.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không cập nhật được dữ liệu sản phẩm.");
            }
        }

        #endregion

        #region Order detail

        public static ClientOrderDetail InsertOrderDetail(ClientOrderDetail orderDetail)
        {
            try
            {
                var endPointString = endPointQLBH + "api/orderdetails";
                var orderDetailNew = Task.Run(() => PostAsync<ClientOrderDetail>(endPointString, orderDetail)).Result;

                return orderDetailNew;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không thêm được chi tiết hóa đơn.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không thêm được chi tiết hóa đơn.");
            }
        }

        #endregion

        #region Category

        public static IEnumerable<ClientCategory> GetCategories()
        {
            try
            {
                var endPointString = endPointQLBH + "api/categories";
                var categories = Task.Run(() => GetCachedNullCheckAsync<IEnumerable<ClientCategory>>(endPointString)).Result;

                return categories;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không lấy được dữ liệu loại sản phẩm.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không lấy được dữ liệu loại sản phẩm.");
            }
        }

        public static ClientCategory GetSingleCategory(int? id)
        {
            try
            {
                var endPointString = endPointQLBH + "api/categories/" + id;
                var categorie = Task.Run(() => GetCachedNullCheckAsync<ClientCategory>(endPointString)).Result;

                return categorie;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không lấy được dữ liệu loại sản phẩm.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không lấy được dữ liệu loại sản phẩm.");
            }
        }

        public static ClientCategory InsertCategory(ClientCategory category)
        {
            try
            {
                var endPointString = endPointQLBH + "api/categories";
                var categoryInsert = Task.Run(() => PostAsync<ClientCategory>(endPointString, category)).Result;

                return categoryInsert;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không thêm được dữ liệu loại sản phẩm.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không thêm được dữ liệu loại sản phẩm.");
            }
        }

        public static ClientCategory UpdateCategory(ClientCategory category)
        {
            try
            {
                var endPointString = endPointQLBH + "api/categories";
                var categoryUpdate = Task.Run(() => PutAsync<ClientCategory>(endPointString, category)).Result;

                return categoryUpdate;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không cập nhật được dữ liệu loại sản phẩm.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không cập nhật được dữ liệu loại sản phẩm.");
            }
        }

        public static Boolean RemoveCategory(int? id)
        {
            try
            {
                var endPointString = endPointQLBH + "api/categories/" + id;
                var categoryDelete = Task.Run(() => DeleteAsync<Boolean>(endPointString)).Result;

                return categoryDelete;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không xóa được dữ liệu loại sản phẩm.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không xóa được dữ liệu loại sản phẩm.");
            }
        }

        #endregion

        #region Statuses

        public static IEnumerable<ClientStatuses> GetStatuses(string filter = null, string sort = "SttID DESC")
        {
            try
            {
                var endPointString = endPointQLBH + "api/statuses?filter=" + filter + "&sort=" + sort;
                var statuses = Task.Run(() => GetCachedNullCheckAsync<IEnumerable<ClientStatuses>>(endPointString)).Result;

                return statuses;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không lấy được dữ liệu trạng thái.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không lấy được dữ liệu trạng thái.");
            }
        }

        #endregion

        #region Comment

        public static IEnumerable<ClientComment> GetCommentsByProduct(int? proId)
        {
            try
            {
                var endPointString = endPointQLBH + "api/comments/Product?proId=" + proId;
                var comments = Task.Run(() => GetCachedNullCheckAsync<IEnumerable<ClientComment>>(endPointString)).Result;

                return comments;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không lấy được dữ liệu bình luận.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không lấy được dữ liệu bình luận.");
            }
        }

        public static ClientComment InsertComment(ClientComment clientComment)
        {
            try
            {
                var endPointString = endPointQLBH + "api/comments";
                var commentInsert = Task.Run(() => PostAsync<ClientComment>(endPointString, clientComment)).Result;

                return commentInsert;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không thêm mới được dữ liệu bình luận.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không thêm mới được dữ liệu bình luận.");
            }
        }

        public static ClientComment UpdateComment(ClientComment clientComment)
        {
            try
            {
                var endPointString = endPointQLBH + "api/comments";
                var commentUpdate = Task.Run(() => PutAsync<ClientComment>(endPointString, clientComment)).Result;

                return commentUpdate;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không cập nhật được dữ liệu bình luận.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không cập nhật được dữ liệu bình luận.");
            }
        }

        #endregion

        #region Rating

        public static IEnumerable<ClientRating> GetRatingsByProduct(int? proId)
        {
            try
            {
                var endPointString = endPointQLBH + "api/ratings/Product?proId=" + proId;
                var ratings = Task.Run(() => GetCachedNullCheckAsync<IEnumerable<ClientRating>>(endPointString)).Result;

                return ratings;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không lấy được dữ liệu đánh giá.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không lấy được dữ liệu đánh giá.");
            }
        }

        public static ClientRating InsertRating(ClientRating clientRating)
        {
            try
            {
                var endPointString = endPointQLBH + "api/ratings";
                var ratingInsert = Task.Run(() => PostAsync<ClientRating>(endPointString, clientRating)).Result;

                return ratingInsert;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không thêm mới được dữ liệu đánh giá.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không thêm mới được dữ liệu đánh giá.");
            }
        }

        public static ClientRating UpdateRating(ClientRating clientRating)
        {
            try
            {
                var endPointString = endPointQLBH + "api/ratings";
                var ratingUpdate = Task.Run(() => PutAsync<ClientRating>(endPointString, clientRating)).Result;

                return ratingUpdate;
            }
            catch (BusinessLayerException)
            {
                throw new BusinessLayerException(500, "#1001002 Không cập nhật được dữ liệu đánh giá.");
            }
            catch (Exception)
            {
                throw new BusinessLayerException(500, "#1001003 Không cập nhật được dữ liệu đánh giá.");
            }
        }

        #endregion

        #endregion


        #region Calling api methods

        /// <summary>
        ///   Lấy dữ liệu theo endpoint, có lưu cache, kiểm tra null.
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        /// <exception cref="BusinessLayerException">Khi không lấy được dữ liệu.</exception>
        /// <exception cref="Exception">Khi có lỗi khi lấy dữ liệu.</exception>
        private static async Task<T> GetCachedNullCheckAsync<T>(string endpoint, int days = 1)
        {
            try
            {
                var resultCache = cache[endpoint];

                if (resultCache == null)
                {
                    var client = HttpClientInstance.Instance;

                    //if (accessToken == string.Empty)
                    //{
                    //    accessToken = await GetAccessTokenAsync();
                    //}

                    //client.DefaultRequestHeaders.Accept.Clear();

                    //// Add the Authorization header with the AccessToken.
                    //client.DefaultRequestHeaders.Clear();
                    //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                    var response = (await client.GetAsync(endpoint)) ?? throw new BusinessLayerException(errorDescription: "Không lấy được dữ liệu.");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content;

                        string responseString = await responseContent.ReadAsStringAsync();

                        resultCache = JsonConvert.DeserializeObject<T>(responseString);
                        if (((dynamic)resultCache).Id == null)
                            throw new BusinessLayerException(errorDescription: "Không lấy được dữ liệu.");
                        Add(endpoint, resultCache, DateTime.Now.AddDays(days));
                    }
                    else
                    {
                        Log.Info($"====={nameof(GetCachedNullCheckAsync)} response:{JsonConvert.SerializeObject(response)}=====client:{JsonConvert.SerializeObject(client)}====accessToken:{accessToken}===Endpoint:{endpoint}");
                        throw new BusinessLayerException(errorDescription: "Không lấy được dữ liệu");
                    }

                    Log.Info($"====={nameof(GetCachedNullCheckAsync)} Khonglaytucache:Endpoint:{endpoint}");
                }
                else
                {
                    Log.Info($"====={nameof(GetCachedNullCheckAsync)} Laytucache:{endpoint}");
                }

                return (T)resultCache;
            }
            catch (BusinessLayerException ex)
            {
                Log.Debug($"====={nameof(GetCachedNullCheckAsync)} BusinessLayerException: Endpoint:{endpoint}=====ex:{ ex.ToString()}");
                throw ex;
            }
            catch (Exception ex)
            {
                Log.Debug($"====={nameof(GetCachedNullCheckAsync)} Exception:Endpoint:{endpoint}");
                throw ex;
            }
        }

        /// <summary>
        ///   Lấy dữ liệu theo endpoint, có lưu cache, không kiểm tra null.
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        /// <exception cref="BusinessLayerException">Khi không lấy được dữ liệu.</exception>
        /// <exception cref="Exception">Khi có lỗi khi lấy dữ liệu.</exception>
        private static async Task<T> GetCachedAsync<T>(string endpoint, int days = 1)
        {
            try
            {
                var resultCache = cache[endpoint];

                if (resultCache == null)
                {
                    var client = HttpClientInstance.Instance;

                    //if (accessToken == string.Empty)
                    //{
                    //    accessToken = await GetAccessTokenAsync();
                    //}

                    //client.DefaultRequestHeaders.Accept.Clear();

                    //// Add the Authorization header with the AccessToken.
                    //client.DefaultRequestHeaders.Clear();
                    //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                    var response = (await client.GetAsync(endpoint));

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content;

                        string responseString = await responseContent.ReadAsStringAsync();

                        resultCache = JsonConvert.DeserializeObject<T>(responseString);
                        if (resultCache != null && resultCache.GetType().GetProperty("Id")?.GetValue(resultCache, null) != null)
                            Add(endpoint, resultCache, DateTime.Now.AddDays(days));
                    }
                    else
                    {
                        Log.Info($"====={nameof(GetCachedAsync)} response:{JsonConvert.SerializeObject(response)}=====client:{JsonConvert.SerializeObject(client)}====accessToken:{accessToken}===Endpoint:{endpoint}");
                        throw new BusinessLayerException(errorDescription: "Không lấy được dữ liệu");
                    }

                    Log.Info($"====={nameof(GetCachedAsync)} Khonglaytucache:Endpoint:{endpoint}");
                }
                else
                {
                    Log.Info($"====={nameof(GetCachedAsync)} Laytucache:{endpoint}");
                }

                return (T)resultCache;
            }
            catch (BusinessLayerException ex)
            {
                Log.Debug($"====={nameof(GetCachedAsync)} BusinessLayerException: Endpoint:{endpoint}=====ex:{ ex.ToString()}");
                throw ex;
            }
            catch (Exception ex)
            {
                Log.Debug($"====={nameof(GetCachedAsync)} Exception:Endpoint:{endpoint}");
                throw ex;
            }
        }

        /// <summary>
        ///   Lấy dữ liệu theo endpoint, không lưu cache, có kiểm tra null.
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        private static async Task<T> GetNullCheckAsync<T>(string endpoint)
        {
            try
            {
                var client = HttpClientInstance.Instance;

                //if (accessToken == string.Empty)
                //{
                //    accessToken = await GetAccessTokenAsync();
                //}

                //client.DefaultRequestHeaders.Accept.Clear();

                //// Add the Authorization header with the AccessToken.
                //client.DefaultRequestHeaders.Clear();
                //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                var response = await client.GetAsync(endpoint) ?? throw new BusinessLayerException(errorDescription: "Không lấy được dữ liệu.");
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;

                    // by calling .Result you are synchronously reading the result
                    string responseString = await responseContent.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<T>(responseString);
                }
                else
                {
                    Log.Info($"====={nameof(GetNullCheckAsync)} response:{JsonConvert.SerializeObject(response)}=====client:{JsonConvert.SerializeObject(client)}====accessToken:{accessToken}===Endpoint:{endpoint}");
                    throw new BusinessLayerException(errorDescription: "Không lấy được dữ liệu");
                }
            }
            catch (BusinessLayerException ex)
            {
                Log.Debug($"====={nameof(GetNullCheckAsync)} BusinessLayerException: Endpoint:{endpoint}=====ex:{ex.ToString()}");
                throw ex;
            }
            catch (Exception ex)
            {
                Log.Debug($"====={nameof(GetNullCheckAsync)} Exception:Endpoint:{endpoint}");
                throw ex;
            }
        }
        /// <summary>
        ///   Lấy dữ liệu theo endpoint, không lưu cache, không kiểm tra null.
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        /// <exception cref="BusinessLayerException">Khi không lấy được dữ liệu.</exception>
        /// <exception cref="Exception">Khi có lỗi khi lấy dữ liệu.</exception>
        private static async Task<T> GetAsync<T>(string endpoint)
        {
            try
            {
                var client = HttpClientInstance.Instance;

                //if (accessToken == string.Empty)
                //{
                //    accessToken = await GetAccessTokenAsync();
                //}

                //client.DefaultRequestHeaders.Accept.Clear();

                //// Add the Authorization header with the AccessToken.
                //client.DefaultRequestHeaders.Clear();
                //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                var response = (await client.GetAsync(endpoint));

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content;

                    string responseString = await responseContent.ReadAsStringAsync();

                    return JsonConvert.DeserializeObject<T>(responseString);
                }
                else
                {
                    Log.Info($"====={nameof(GetAsync)} response:{JsonConvert.SerializeObject(response)}=====client:{JsonConvert.SerializeObject(client)}====accessToken:{accessToken}===Endpoint:{endpoint}");
                    throw new BusinessLayerException(errorDescription: "Không lấy được dữ liệu");
                }
            }
            catch (BusinessLayerException ex)
            {
                Log.Debug($"====={nameof(GetAsync)} BusinessLayerException: Endpoint:{endpoint}=====ex:{ex.ToString()}");
                throw ex;
            }
            catch (Exception ex)
            {
                Log.Debug($"====={nameof(GetAsync)} Exception:Endpoint:{endpoint}");
                throw ex;
            }
        }

        /// <summary>
        ///   Thực hiện lệnh post.
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// <param name="endPoint"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async static Task<T> PostAsync<T>(string endPoint, object data)
        {
            try
            {
                var client = HttpClientInstance.Instance;

                //if (accessToken == string.Empty)
                //{
                //    accessToken = await GetAccessTokenAsync();
                //}

                //client.DefaultRequestHeaders.Accept.Clear();

                //// Add the Authorization header with the AccessToken.
                //client.DefaultRequestHeaders.Clear();
                //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                var httpContent = new StringContent(JsonConvert.SerializeObject(data));
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                httpContent.Headers.ContentType.CharSet = "UTF-8";

                var response = await client.PostAsync(endPoint, httpContent);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<T>(content);
                }
                else
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Log.Info($"=====response:{JsonConvert.SerializeObject(content)}object: {JsonConvert.SerializeObject(data)} ");
                    throw new BusinessLayerException(errorDescription: content);
                }
            }
            catch (BusinessLayerException ex)
            {
                Log.Debug(data, ex);
                throw ex;
            }
            catch (Exception ex)
            {
                Log.Debug(data, ex);
                throw ex;
            }
        }

        /// <summary>
        ///   Thực hiện lệnh put.
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static async Task<T> PutAsync<T>(string endpoint, object data)
        {
            try
            {
                var client = HttpClientInstance.Instance;

                //if (accessToken == string.Empty)
                //{
                //    accessToken = await GetAccessTokenAsync();
                //}

                //client.DefaultRequestHeaders.Accept.Clear();

                //// Add the Authorization header with the AccessToken.
                //client.DefaultRequestHeaders.Clear();
                //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                var httpContent = new StringContent(JsonConvert.SerializeObject(data));
                httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                var response = await client.PutAsync(endpoint, httpContent);
                string content = await response.Content.ReadAsStringAsync();

                return await Task.Run(() => JsonConvert.DeserializeObject<T>(content));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        ///   Thực hiện lệnh delete.
        /// </summary>
        /// 
        /// <typeparam name="T"></typeparam>
        /// <param name="endpoint"></param>
        /// <returns></returns>
        public static async Task<bool> DeleteAsync<T>(string endpoint)
        {
            try
            {
                var client = HttpClientInstance.Instance;

                //if (accessToken == string.Empty)
                //{
                //    accessToken = await GetAccessTokenAsync();
                //}

                //client.DefaultRequestHeaders.Accept.Clear();

                //// Add the Authorization header with the AccessToken.
                //client.DefaultRequestHeaders.Clear();
                //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                var response = await client.DeleteAsync(endpoint);

                if (response.IsSuccessStatusCode)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}