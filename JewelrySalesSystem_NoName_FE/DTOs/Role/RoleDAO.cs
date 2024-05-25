using JewelrySalesSystem_NoName_FE.DTOs.Account;

namespace JewelrySalesSystem_NoName_FE.DTOs.Role
{
    public class RoleDAO
    {
        public Guid Id { get; set; }

        public string RoleName { get; set; } = null!;
        public virtual ICollection<AccountDAO> Accounts { get; } = new List<AccountDAO>();
    }
}
