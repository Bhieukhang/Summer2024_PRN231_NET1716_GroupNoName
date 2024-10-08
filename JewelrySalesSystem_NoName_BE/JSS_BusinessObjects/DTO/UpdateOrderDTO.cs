﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.DTO
{
    public class UpdateOrderDTO
    {
        public Guid? OrderId { get; set; }
    
        public double? TotalPrice { get; set; }
        public double? FinalPrice { get; set; }
        public int? VoucherId { get; set; }
        public int? ShippingId { get; set; }
        public int? Status { get; set; }
    }
}
