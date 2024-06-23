namespace JewelrySalesSystem_NoName_FE.DTOs.Membership
{
    public class MembershipDTO
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string Level { get; set; }

        public int? Point { get; set; }

        public int? RedeemPoint { get; set; }

        public Guid UserId { get; set; }

        public double UsedMoney { get; set; }

        public bool? Deflag { get; set; }
    }
}
