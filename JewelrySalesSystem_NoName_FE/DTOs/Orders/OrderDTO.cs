namespace JewelrySalesSystem_NoName_FE.DTOs.Orders
{
    public class OrderDTO
    {
        public Guid CustomerId { get; set; }
        public Guid PromotionId { get; set; }
        public Guid? DiscountId { get; set; }
        public double TotalPrice { get; set; }
        public double MaterialProccessPrice { get; set; }
        public List<OrderDetailDTO> Details { get; set; }
        public Guid Id { get; set; }
    }

    public class OrderDetailDTO
    {
        public double Amount { get; set; }
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
    }

    public class CheckPromotionRequest
    {
        public Guid PromotionId { get; set; }
        public OrderDTO Order { get; set; }
    }
}
