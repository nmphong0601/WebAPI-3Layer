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
    public class CartDAO: ICartDAO
    {
        private QLBH_WebEntities db = new QLBH_WebEntities();

        public int TotalQuantity(ApiCart cart)
        {
            return cart.Items.Sum(i => i.Quantity);
        }

        public ApiCart AddItem(ApiCart cart, int proId, int quantity = 1)
        {
            var pro = db.Products.Where(p => p.ProID == proId).FirstOrDefault();
            if (pro != null)
            {
                var ci = cart.Items.Where(i => i.Product.ProID == proId).FirstOrDefault();
                if (ci == null)
                {
                    ci = new ApiCartItem { Product = Mapper.Map<Product, ApiProduct>(pro), Quantity = quantity };
                    cart.Items.Add(ci);
                }

                else
                {
                    ci.Quantity += quantity;
                }
            }

            return cart;
        }

        //nút xóa
        public ApiCart Remove(ApiCart cart, int pId)
        {
            var ci = cart.Items.Where(i => i.Product.ProID == pId).FirstOrDefault();
            if (ci != null)
            {
                cart.Items.Remove(ci);
            }
            return cart;
        }
        //nút cập nhật
        public ApiCart Update(ApiCart cart, int pId, int quality)
        {
            var ci = cart.Items.Where(i => i.Product.ProID == pId).FirstOrDefault();
            if (ci != null)
            {
                ci.Quantity = quality;
            }

            return cart;
        }
        //checkout
        public ApiCart Checkout(ApiCart cart)
        {
            cart.Items.Clear();
            return cart;
        }
    }
}
