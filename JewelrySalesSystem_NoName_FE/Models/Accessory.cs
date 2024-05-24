using System;
using System.Collections.Generic;

namespace JewelrySalesSystem_NoName_FE;

public partial class Accessory
{
    public Guid Id { get; set; }

    public string? AccessoryName { get; set; }

    public string? Description { get; set; }

    public double? ImportPrice { get; set; }

    public double? Size { get; set; }

    public double? TotalPrice { get; set; }

    public DateTime? InsDate { get; set; }

    public bool? Deflag { get; set; }

    public DateTime? UpsDate { get; set; }

    public Guid? CategoryId { get; set; }

    public int? Quantity { get; set; }

    public double? ProcessPrice { get; set; }

    public Guid? AccessoryMaterialId { get; set; }

    public virtual ICollection<AccessoryMaterial> AccessoryMaterials { get; } = new List<AccessoryMaterial>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
