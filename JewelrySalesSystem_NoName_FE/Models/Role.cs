using System;
using System.Collections.Generic;

namespace JewelrySalesSystem_NoName_FE;

public partial class Role
{
    public Guid Id { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; } = new List<Account>();
}
