using DAO.Factory;
using DTO.ApiObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class CartsBUS
    {
        readonly DAOFactory factory;
        public CartsBUS()
        {
            this.factory = new DAOFactory();
        }

        public CartsBUS(DAOFactory factory)
        {
            this.factory = factory ?? throw new ArgumentNullException(nameof(DAOFactory));
        }

        public ApiCart AddItem(ApiCart cart, int proId, int quantity = 1)
        {
            return factory.CartDAO.AddItem(cart, proId, quantity);
        }

        public ApiCart Remove(ApiCart cart, int pId)
        {
            return factory.CartDAO.Remove(cart, pId);
        }

        public ApiCart Update(ApiCart cart, int pId, int quality)
        {
            return factory.CartDAO.Update(cart, pId, quality);
        }

        public ApiCart Checkout(ApiCart cart)
        {
            return factory.CartDAO.Checkout(cart);
        }
    }
}
