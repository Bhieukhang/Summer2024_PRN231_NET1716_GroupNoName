using System;
using System.Collections.Generic;

namespace  JewelrySalesSystem_NoName_FE;

public partial class Category
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public string? Type { get; set; }

    public string? Description { get; set; }

    public double? PricePressure { get; set; }

    public virtual ICollection<Accessory> Accessories { get; } = new List<Accessory>();

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
