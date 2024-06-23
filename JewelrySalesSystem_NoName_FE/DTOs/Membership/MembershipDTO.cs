using System.ComponentModel.DataAnnotations;

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

        [Range(0, 999_999_999.99, ErrorMessage = "Số tiền sử dụng phải nằm trong khoảng từ 0 đến 999,999,999.99.")]
        public double UsedMoney { get; set; }

        public bool? Deflag { get; set; }
    }
}
