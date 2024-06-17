using JSS_BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.Payload.Response
{
    public class OrderResponse
    {
        public OrderResponse() { }
        public OrderResponse(Guid id, Guid customerId, string? type, DateTime? insDate, double? totalPrice,
            double? materialProcessPrice, Guid? discount)
        {
            Id = id;
            CustomerId = customerId;
            Type = type;
            InsDate = insDate;
            TotalPrice = totalPrice;
            MaterialProcessPrice = materialProcessPrice;
            Discount = discount;
        }
        public OrderResponse(Guid id, Guid customerId, string? type, DateTime? insDate, double? totalPrice,
            double? materialProcessPrice)
        {
            Id = id;
            CustomerId = customerId;
            Type = type;
            InsDate = insDate;
            TotalPrice = totalPrice;
            MaterialProcessPrice = materialProcessPrice;
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

    public class CustomerOrderResponse
    {
        public CustomerOrderResponse() { }
        public CustomerOrderResponse(InforCustomer infor, List<OrderResponse> list)
        {
            Infor = infor;
            ListOrder = list;
        }

        public InforCustomer Infor { get; set; }
        public List<OrderResponse> ListOrder { get; set; }
    }

    public class OrderDetailList
    {
        public OrderDetailList() { }
        public OrderDetailList(Guid orderId, List<OrderDetailResponse> listOrder)
        {
            OrderId = orderId;
            ListOrder = listOrder;
        }
        public Guid OrderId { get; set; }
        public List<OrderDetailResponse> ListOrder = new List<OrderDetailResponse>();
    }
}
