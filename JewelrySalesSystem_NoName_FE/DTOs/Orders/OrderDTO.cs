namespace JewelrySalesSystem_NoName_FE.DTOs.Orders
{
    public class OrderDTO
    {
        public Guid CustomerId { get; set; }
        public Guid PromotionId { get; set; }
        public Guid DiscountId { get; set; }
        public double TotalPrice { get; set; }
        public double MaterialProccessPrice { get; set; }
        public List<OrderDetailDTO> Details { get; set; } = new List<OrderDetailDTO>();
    }

    public class OrderDetailDTO
    {
        public string ProductName { get; set; }
        public double Amount { get; set; }
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
    }
}
