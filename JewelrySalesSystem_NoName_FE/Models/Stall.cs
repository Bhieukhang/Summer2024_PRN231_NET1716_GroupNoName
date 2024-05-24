using System;
using System.Collections.Generic;

namespace JewelrySalesSystem_NoName_FE;

public partial class Stall
{
    public Guid Id { get; set; }

    public string? Name { get; set; }

    public int? Number { get; set; }

    public Guid StaffId { get; set; }

    public bool? Deflag { get; set; }

    public virtual Account Staff { get; set; } = null!;
}
