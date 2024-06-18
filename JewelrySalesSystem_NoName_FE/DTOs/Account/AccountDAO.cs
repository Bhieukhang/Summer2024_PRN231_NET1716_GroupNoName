namespace JewelrySalesSystem_NoName_FE.DTOs.Account
{
    public class AccountDAO
    {
        public Guid Id { get; set; }

        public string? FullName { get; set; }

        public string Phone { get; set; } = null!;

        public DateTime? Dob { get; set; }

        public string Password { get; set; } = null!;

        public string? Address { get; set; }

        public string? ImgUrl { get; set; }

        public string? Status { get; set; }

        public bool? Deflag { get; set; }

        public Guid RoleId { get; set; }

        public DateTime? InsDate { get; set; }

        public DateTime? UpsDate { get; set; }
        public string RoleName { get; set; }
        public static readonly List<Guid> ExcludedRoleIds = new List<Guid>
        {
            Guid.Parse("7C9E6679-7425-40DE-944B-E07FC1F90AE9"),
            Guid.Parse("0F8FAD5B-D9CB-469F-A165-70867728950E")
        };
    }
}
