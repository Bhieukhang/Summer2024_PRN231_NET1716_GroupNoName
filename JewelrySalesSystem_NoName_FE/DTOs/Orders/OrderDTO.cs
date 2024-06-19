using System.ComponentModel.DataAnnotations;

namespace JewelrySalesSystem_NoName_FE.DTOs.Orders
{
    public class OrderDTO
    {
        [Required]
        public string CustomerPhone { get; set; } = string.Empty;
        public Guid? DiscountId { get; set; }
        public double TotalPrice { get; set; }
        public Guid CustomerId { get; set; }
        public double MaterialProccessPrice { get; set; }
        public List<OrderDetailDTO> Details { get; set; }
        public Guid Id { get; set; }
        public DateTime? InsDate { get; set; }
    }

    public class OrderDetailDTO
    {
        public Guid PromotionId { get; set; }
        public double Amount { get; set; }
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
    }

    public class CheckPromotionRequest
    {
        public Guid PromotionId { get; set; }
        public OrderDTO Order { get; set; }
    }

    public class OrderData
    {
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public double TotalPrice { get; set; }
        public double TotalDiscount { get; set; }
        public string PaymentMethod { get; set; }
        public List<ProductDetail> ProductDetails { get; set; }
    }

    public class ProductDetail
    {
        public string Code { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }
    }
}
