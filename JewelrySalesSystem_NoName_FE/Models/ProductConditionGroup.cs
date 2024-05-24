using System;
using System.Collections.Generic;

namespace JewelrySalesSystem_NoName_FE;

public partial class ProductConditionGroup
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public DateTime? InsDate { get; set; }

    public Guid PromotionId { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Promotion Promotion { get; set; } = null!;
}
