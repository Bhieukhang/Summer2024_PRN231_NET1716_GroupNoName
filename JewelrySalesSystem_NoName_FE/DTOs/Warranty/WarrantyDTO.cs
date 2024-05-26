namespace JewelrySalesSystem_NoName_FE.DTOs.Warranty
{
    public class WarrantyDTO
    {
        public Guid Id { get; set; }
        public DateTime? DateOfPurchase { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string Period { get; set; }
        public bool? Deflag { get; set; }
        public string Status { get; set; }
        public ConditionWarranty ConditionWarranty { get; set; }
        public string OrderDetail { get; set; }
        public string Product { get; set; }
    }
    public class ConditionWarranty
    {
        public Guid Id { get; set; }
        public string Condition { get; set; }
        public DateTime? InsDate { get; set; }
        public bool? Deflag { get; set; }
        public List<WarrantyDTO> warranties { get; set; }
    }

}
