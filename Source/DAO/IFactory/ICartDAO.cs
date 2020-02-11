using DTO.Models;
using Ultilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.ApiObjects;

namespace DAO.IFactory
{
    public interface ICartDAO
    {
        int TotalQuantity(ApiCart cart);

        ApiCart AddItem(ApiCart cart, int proId, int quantity = 1);

        //nút xóa
        ApiCart Remove(ApiCart cart, int pId);

        //nút cập nhật
        ApiCart Update(ApiCart cart, int pId, int quality);

        //checkout
        ApiCart Checkout(ApiCart cart);

    }
}
