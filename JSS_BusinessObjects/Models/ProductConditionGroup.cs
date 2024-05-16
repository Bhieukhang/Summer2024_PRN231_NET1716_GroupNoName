using System;
using System.Collections.Generic;

namespace JSS_BusinessObjects.Models;

public partial class ProductConditionGroup
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public DateTime? InsDate { get; set; }

    public Guid PromotionId { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Promotion Promotion { get; set; } = null!;
}
