using System;
using System.Collections.Generic;
using JewelrySalesSystem_NoName_FE.DTOs.Product;

namespace JewelrySalesSystem_NoName_FE;

public partial class ProductMaterial
{
    public Guid Id { get; set; }

    public Guid? ProductId { get; set; }

    public Guid? MaterialId { get; set; }

    public DateTime? InsDate { get; set; }

    public virtual Material? Material { get; set; }

    public virtual ProductDTO? Product { get; set; }
}
