namespace JewelrySalesSystem_NoName_FE.DTOs.Promotions
{
    public class GroupDTO
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public DateTime? InsDate { get; set; }

        public Guid PromotionId { get; set; }

        public int? Quantity { get; set; }
    }
}
