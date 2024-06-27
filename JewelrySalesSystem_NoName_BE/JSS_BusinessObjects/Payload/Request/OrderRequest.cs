using JSS_BusinessObjects.Models;
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

    public class OrderRequestList
    {
        public string CustomerPhone { get; set; }

        public Guid? DiscountId { get; set; }

        public double? TotalPrice { get; set; }

        public double? MaterialProcessPrice { get; set; }
        public List<OrderDetailPromotionRequest> Details { get; set; }
    }

    public class OrderDetailPromotionRequest
    {

        public Guid? PromotionId { get; set; }
        public double? Amount { get; set; }

        public int? Quantity { get; set; }

        public Guid ProductId { get; set; }
    }

    public class OrderUpdate
    {
        public Guid? DiscountId { set; get; }
        public double? TotalPrice { get; set; }
        public double? MaterialProcessPrice { get; set; }
    }

    public class OrderResponseUpdate
    {
        public string Phone { get; set; }
        public Guid OrderId { get; set; }
    }
   
}
