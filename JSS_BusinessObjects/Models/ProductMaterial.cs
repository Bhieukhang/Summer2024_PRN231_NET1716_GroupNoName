using System;
using System.Collections.Generic;

namespace JSS_BusinessObjects.Models;

public partial class ProductMaterial
{
    public Guid Id { get; set; }

    public Guid? ProductId { get; set; }

    public Guid? MaterialId { get; set; }

    public DateTime? InsDate { get; set; }

    public virtual Material? Material { get; set; }

    public virtual Product? Product { get; set; }
}
