namespace JewelrySalesSystem_NoName_FE.DTOs.Orders
{
    public class OrderProccess
    {
        public Guid OrderId { get; set; }
        public double TotalPrice { get; set; }
        public List<OrderDetailProccess> Details { get; set; }
    }

    public class OrderPatch
    {
        public Guid OrderId { get; set; }
        public Guid? DiscountId { set; get; }
        public double? TotalPrice { get; set; }
        public double? MaterialProcessPrice { get; set; }
    }
}
