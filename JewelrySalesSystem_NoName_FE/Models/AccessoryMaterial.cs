using System;
using System.Collections.Generic;

namespace JewelrySalesSystem_NoName_FE;

public partial class AccessoryMaterial
{
    public Guid Id { get; set; }

    public Guid? AccessoryId { get; set; }

    public Guid? MaterialId { get; set; }

    public DateTime? InsDate { get; set; }

    public virtual Accessory? Accessory { get; set; }

    public virtual Material? Material { get; set; }
}
