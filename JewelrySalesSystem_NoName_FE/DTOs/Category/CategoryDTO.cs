using System;
using System.Collections.Generic;
using JewelrySalesSystem_NoName_FE.DTOs.Product;

namespace JewelrySalesSystem_NoName_FE;

public partial class CategoryDTO
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public string? Description { get; set; }

    public double? PricePressure { get; set; }

    public virtual ICollection<Accessory> Accessories { get; } = new List<Accessory>();

    public virtual ICollection<ProductDTO> Products { get; } = new List<ProductDTO>();
}
