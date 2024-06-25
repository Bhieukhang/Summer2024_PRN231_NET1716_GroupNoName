namespace JSS_BusinessObjects.Payload.Request
{
    public class DiscountRequest
    {
        public Guid? Id { get; set; }

        public Guid OrderId { get; set; }

        public Guid ManagerId { get; set; }

        public int? PercentDiscount { get; set; }

        public string? Description { get; set; }

        public string? Status { get; set; }

        public double? ConditionDiscount { get; set; }
        public string? Note { get; set; }

    }
}
