namespace JewelrySalesSystem_NoName_FE.DTOs.Warranty
{
    public class WarrantyDTO
    {
        public Guid Id { get; set; }
        public DateTime? DateOfPurchase { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public string Period { get; set; }
        public bool? Deflag { get; set; }
        public Guid? OrderDetailId { get; set; }
        public string Phone { get; set; }
        public Guid ConditionWarrantyId { get; set; }
        public string Status { get; set; }
        public string ConditionWarranty { get; set; }
        public string OrderDetail { get; set; }
        public string Product { get; set; }
    }

    //    public class PaginatedResponse<T>
    //    {
    //        public int Size { get; set; }
    //        public int Page { get; set; }
    //        public int Total { get; set; }
    //        public int TotalPages { get; set; }
    //        public List<T> Items { get; set; }
    //    }
    //}
}
