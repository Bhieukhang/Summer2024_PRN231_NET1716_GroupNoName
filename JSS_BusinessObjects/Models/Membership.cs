using System;
using System.Collections.Generic;

namespace JSS_BusinessObjects.Models;

public partial class Membership
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public int? Level { get; set; }

    public int? Point { get; set; }

    public int? RedeemPoint { get; set; }

    public Guid UserId { get; set; }

    public double? UsedMoney { get; set; }

    public virtual ICollection<Program> Programs { get; set; } = new List<Program>();

    public virtual Account User { get; set; } = null!;
}
