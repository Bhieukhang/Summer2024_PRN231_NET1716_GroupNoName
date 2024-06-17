using System;
using System.Collections.Generic;

namespace JSS_BusinessObjects.Models;

public partial class Diamond
{
    public Guid Id { get; set; }

    public string? DiamondName { get; set; }

    public double? Carat { get; set; }

    public string? Color { get; set; }

    public string? Clarity { get; set; }

    public string? Cut { get; set; }

    public double? Price { get; set; }

    public Guid? JewelryId { get; set; }

    public string? ImageDiamond { get; set; }

    public virtual Product? Jewelry { get; set; } = null!;
}
