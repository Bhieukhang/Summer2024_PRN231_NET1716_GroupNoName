namespace JewelrySalesSystem_NoName_FE.DTOs.Discounts
{
    public class DiscountDTO
    {
        public Guid? Id { get; set; }

        public Guid? OrderId { get; set; }

        public Guid? ManagerId { get; set; }

        public int? PercentDiscount { get; set; }

        public string? Description { get; set; }

        public double? ConditionDiscount { get; set; }

        public string? Status { get; set; }

        public DateTime? InsDate { get; set; }

        public DateTime? UpsDate { get; set; }

        public string? Note { get; set; }

        public Guid? MembershipId { get; set; }

        public int? LevelDiscount { get; set; }

    }
}
