﻿using JSS_BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.Payload.Response
{
    public class OrderResponse
    {
        public OrderResponse(Guid id, Guid customerId, string? type, DateTime? insDate, double? totalPrice,
            double? materialProcessPrice, Guid? discount, Guid? promotion)
        {
            Id = id;
            CustomerId = customerId;
            Type = type;
            InsDate = insDate;
            TotalPrice = totalPrice;
            MaterialProcessPrice = materialProcessPrice;
            Discount = discount;
            Promotion = promotion;
        }
        public Guid Id { get; set; }

        public Guid CustomerId { get; set; }

        public string? Type { get; set; }

        public DateTime? InsDate { get; set; }

        public double? TotalPrice { get; set; }

        public double? MaterialProcessPrice { get; set; }

        public virtual Guid? Discount { get; set; } = null!;

        public virtual Guid? Promotion { get; set; }
    }
}
