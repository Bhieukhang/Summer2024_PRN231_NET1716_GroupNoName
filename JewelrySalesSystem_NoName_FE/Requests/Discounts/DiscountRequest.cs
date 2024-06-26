using System.ComponentModel.DataAnnotations;

namespace JewelrySalesSystem_NoName_FE.Requests.Discounts
{
    public class DiscountRequest
    {
        public Guid? Id { get; set; }

        [Required] public Guid? OrderId { get; set; }

        [Required] public Guid? ManagerId { get; set; }

        [Required] public int? PercentDiscount { get; set; }

        public string? Status { get; set; }

        [Required] public string? Description { get; set; }

        [Required] public double? ConditionDiscount { get; set; }
        public string? Note { get; set; }
    }
}
