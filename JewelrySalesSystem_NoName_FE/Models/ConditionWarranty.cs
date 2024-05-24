using System;
using System.Collections.Generic;

namespace JewelrySalesSystem_NoName_FE;

public partial class ConditionWarranty
{
    public Guid Id { get; set; }

    public string? Condition { get; set; }

    public DateTime? InsDate { get; set; }

    public bool? Deflag { get; set; }

    public virtual ICollection<Warranty> Warranties { get; } = new List<Warranty>();
}
