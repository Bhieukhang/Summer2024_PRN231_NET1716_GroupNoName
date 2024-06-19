using System.ComponentModel.DataAnnotations;

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
        //public string OrderDetail { get; set; }
        //public string Product { get; set; }
    }
    public class ConditionWarranty
    {
        public Guid Id { get; set; }
        public string Condition { get; set; }
        public DateTime? InsDate { get; set; }
        public bool? Deflag { get; set; }
        public List<WarrantyDTO> warranties { get; set; }
    }
    public class WarrantyCreate
    {
        [Required]
        public DateTime? DateOfPurchase { get; set; }
        [Required]
        public DateTime? ExpirationDate { get; set; }
        
        public string Period { get; set; }
        [Required]
        public Guid? OrderDetailId { get; set; }
        public string Note { get; set; } = string.Empty;
        [Required]
        public List<ConditionMap> conditionMap { get; set; }
    }

    public class ConditionMap
    {
        public Guid ConditionWarrantyId { get; set; }
    }
}
