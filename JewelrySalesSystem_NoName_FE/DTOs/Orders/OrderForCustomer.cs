namespace JewelrySalesSystem_NoName_FE.DTOs.Orders
{
    public class OrderForCustomer
    {
        public Infor Infor { get; set; }
        public List<OrderCustomer> ListOrder { get; set; }
    }

    public class Infor
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string ImgUrl { get; set; }
    }

    public class OrderCustomer
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Type { get; set; }
        public DateTime? InsDate { get; set; }
        public double TotalPrice { get; set; }
        public double? MaterialProcessPrice { get; set; }
        public Guid? Discount { get; set; }
        public Guid? Promotion { get; set; }
    }
}
