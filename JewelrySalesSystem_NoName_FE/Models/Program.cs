using System;
using System.Collections.Generic;

namespace JewelrySalesSystem_NoName_FE;

public partial class Program
{
    public Guid Id { get; set; }

    public Guid MembershipId { get; set; }

    public Guid PromotionId { get; set; }

    public DateTime? InsDate { get; set; }

    public bool? Deflag { get; set; }

    public virtual Membership Membership { get; set; } = null!;

    public virtual Promotion Promotion { get; set; } = null!;
}
