using System;
using System.Collections.Generic;

namespace JSS_BusinessObjects.Models;

public partial class Material
{
    public Guid Id { get; set; }

    public string? MaterialName { get; set; }

    public double? Weight { get; set; }

    public DateTime? InsDate { get; set; }

    public Guid ProductMaterialId { get; set; }

    public bool? Deflag { get; set; }

    public virtual ICollection<ProductMaterial> ProductMaterials { get; } = new List<ProductMaterial>();
}
