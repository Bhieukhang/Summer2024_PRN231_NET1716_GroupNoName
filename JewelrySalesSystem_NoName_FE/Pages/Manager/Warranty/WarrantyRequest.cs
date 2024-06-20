namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Warranty
{
    public class WarrantyRequest
    {
        public DateTime DateOfPurchase { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Period { get; set; }
        public bool Deflag { get; set; }
        public string Status { get; set; }
        public Guid ConditionWarrantyId { get; set; }
        public string Note { get; set; }
    }

}
