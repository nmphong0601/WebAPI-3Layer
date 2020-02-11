﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.ApiObjects
{
    public class ApiOrder
    {
        public ApiOrder()
        {
            OrderDetails = new List<ApiEntity>();
        }

        public int? OrderID { get; set; }
        public DateTime? OrderDate { get; set; }
        public int? UserID { get; set; }
        public decimal? Total { get; set; }
        public int? SttID { get; set; }

        public List<ApiEntity> OrderDetails { get; set; }
        public ApiUser User { get; set; }
        public ApiStatues Status { get; set; }
    }
}
