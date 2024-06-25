namespace JewelrySalesSystem_NoName_FE.DTOs.Orders
{
    public class OrderDetailList
    {
        public Guid OrderId { get; set; }
        public List<OrderDetailResponse> ListOrder = new List<OrderDetailResponse>();
    }

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

    public class OrderDetailProccess
    {
        public Guid Id { get; set; }
        public double? Amount { get; set; }

        public int? Quantity { get; set; }
        public string ProductName { get; set; }
        public double ProcessPrice { get; set; }
    }
}
