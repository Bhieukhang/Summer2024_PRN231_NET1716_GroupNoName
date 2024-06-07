using System;
using System.Collections.Generic;
using JewelrySalesSystem_NoName_FE.DTOs.Product;

namespace JewelrySalesSystem_NoName_FE;

public partial class CategoryDTO
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public double? PricePressure { get; set; }

    public virtual ICollection<ProductDTO> Products { get; } = new List<ProductDTO>();
}
