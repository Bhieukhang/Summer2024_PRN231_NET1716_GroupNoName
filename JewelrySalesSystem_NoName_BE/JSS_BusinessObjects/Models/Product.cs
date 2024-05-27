using System;
using System.Collections.Generic;

namespace JSS_BusinessObjects.Models;

public partial class Product
{
    public Guid Id { get; set; }

    public string? ProductName { get; set; }

    public string? Description { get; set; }

    public double? ImportPrice { get; set; }

    public double? Size { get; set; }

    public double? TotalPrice { get; set; }

    public DateTime? InsDate { get; set; }

    public bool? Deflag { get; set; }

    public Guid? CategoryId { get; set; }

    public DateTime? UpsDate { get; set; }

    public int? Quantity { get; set; }

    public double? ProcessPrice { get; set; }

    public Guid? ProductMaterialId { get; set; }

    public Guid? AccessoryId { get; set; }

    public string? Code { get; set; }
    public string? ImgProduct { get; set; }
    public virtual Accessory? Accessory { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();

    public virtual ICollection<ProductConditionGroup> ProductConditionGroups { get; } = new List<ProductConditionGroup>();

    public virtual ICollection<ProductMaterial> ProductMaterials { get; } = new List<ProductMaterial>();
}
