namespace JewelrySalesSystem_NoName_FE.DTOs.Account
{
    public class AccountProfileDTO
    {
        public string? FullName { get; set; }
        public DateTime? Dob { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public string? ImgUrl { get; set; }
        public Guid RoleId { get; set; }
    }
}
