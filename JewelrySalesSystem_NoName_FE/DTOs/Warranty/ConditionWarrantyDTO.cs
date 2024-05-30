namespace JewelrySalesSystem_NoName_FE.DTOs.Warranty
{
    public class ConditionWarrantyDTO
    {
        public Guid Id { get; set; }

        public string? Condition { get; set; }

        public DateTime? InsDate { get; set; }

        public bool? Deflag { get; set; }
    }
}
