﻿using JSS_BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.Payload.Request
{
    public class OrderRequest
    {
        public string CustomerPhone { get; set; }

        public Guid? PromotionId { get; set; }

        public Guid? DiscountId { get; set; }

        public double? TotalPrice { get; set; }

        public double? MaterialProcessPrice { get; set; }
        public List<OrderDetailRequest> Details { get; set; }
    }
}
