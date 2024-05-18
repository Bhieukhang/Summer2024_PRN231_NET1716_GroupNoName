using System;
using System.Collections.Generic;

namespace JSS_BusinessObjects.Models;

public partial class Role
{
    public Guid Id { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<Account> Accounts { get; } = new List<Account>();
}
