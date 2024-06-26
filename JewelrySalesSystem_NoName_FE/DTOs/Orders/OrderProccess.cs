namespace JewelrySalesSystem_NoName_FE.DTOs.Orders
{
    public class OrderProccess
    {
        public Guid OrderId { get; set; }
        public double TotalPrice { get; set; }
        public List<OrderDetailProccess> Details { get; set; }
    }
}
