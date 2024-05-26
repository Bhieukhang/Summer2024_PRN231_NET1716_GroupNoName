using JewelrySalesSystem_NoName_FE.DTOs.Membership;

namespace JewelrySalesSystem_NoName_FE.DTOs.Account
{
    public class ProfileDTO
    {
        public string Phone { get; set; }
        public DateTime Dob {  get; set; }
        public string Address { get; set; }
        public string ImgUrl { get; set; }
        public MembershipProfile Membership {  get; set; } = new MembershipProfile();

    }
    public class MembershipProfile
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public double Point { get; set; }
        public double RedeemPoint { get; set; }
        public Guid UserId { get; set; }
        public double UserMoney { get; set; }
        public bool Deflag { get; set; }
    }
}
