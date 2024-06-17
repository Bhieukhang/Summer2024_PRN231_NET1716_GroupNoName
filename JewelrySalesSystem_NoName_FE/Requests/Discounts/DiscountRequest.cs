namespace JewelrySalesSystem_NoName_FE.Requests.Discounts
{
    public class DiscountRequest
    {
        public Guid? Id { get; set; }

        public Guid? OrderId { get; set; }

        public Guid? ManagerId { get; set; }

        public int? PercentDiscount { get; set; }

        public string? Description { get; set; }

        public double? ConditionDiscount { get; set; }
    }
}
