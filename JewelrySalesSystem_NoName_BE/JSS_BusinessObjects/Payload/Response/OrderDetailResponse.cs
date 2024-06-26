using JSS_BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.Payload.Response
{
    public class OrderDetailResponse
    {
        public Guid Id { get; set; }

        public double? Amount { get; set; }

        public int? Quantity { get; set; }

        public double? Discount { get; set; }

        public double? TotalPrice { get; set; }
        public string ProductName { get; set; }
        public int? PeriodWarranty { get; set; }
        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public DateTime? InsDate { get; set; }
        public Guid OrderDetailId { get; set; }

    }
    public class OrderDetailsResponse
    {
        public Guid Id { get; set; }

        public double? Amount { get; set; }

        public int? Quantity { get; set; }

        public double? Discount { get; set; }

        public double? TotalPrice { get; set; }

        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }

        public DateTime? InsDate { get; set; }

        public Guid? PromotionId { get; set; }

        public virtual Order Order { get; set; } = null!;

        public virtual Product Product { get; set; } = null!;

        public virtual Promotion? Promotion { get; set; }

        public OrderDetailsResponse(Guid id, double? amount, int? quantity, double? discount, double? totalPrice, Guid orderId, Guid productId, DateTime? insDate, Guid? promotionId, Order order, Product product, Promotion? promotion)
        {
            Id = id;
            Amount = amount;
            Quantity = quantity;
            Discount = discount;
            TotalPrice = totalPrice;
            OrderId = orderId;
            ProductId = productId;
            InsDate = insDate;
            PromotionId = promotionId;
            Order = order;
            Product = product;
            Promotion = promotion;
        }
    }
}
