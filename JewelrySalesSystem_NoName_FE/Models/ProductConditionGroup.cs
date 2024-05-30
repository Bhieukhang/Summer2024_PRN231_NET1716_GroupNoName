using System;
using System.Collections.Generic;
using JewelrySalesSystem_NoName_FE.DTOs.Product;

namespace JewelrySalesSystem_NoName_FE;

public partial class ProductConditionGroup
{
    public Guid Id { get; set; }

    public Guid ProductId { get; set; }

    public DateTime? InsDate { get; set; }

    public Guid PromotionId { get; set; }

    public virtual ProductDTO Product { get; set; } = null!;

    public virtual Promotion Promotion { get; set; } = null!;
}
