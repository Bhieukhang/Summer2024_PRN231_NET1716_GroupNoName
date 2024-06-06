using System;
using System.Collections.Generic;

namespace JSS_BusinessObjects.Models;

public partial class Category
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public double? PricePressure { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
