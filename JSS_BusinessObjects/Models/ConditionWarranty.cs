﻿using System;
using System.Collections.Generic;

namespace JSS_BusinessObjects.Models;

public partial class ConditionWarranty
{
    public Guid Id { get; set; }

    public string? Condition { get; set; }

    public DateTime? InsDate { get; set; }

    public virtual ICollection<Warranty> Warranties { get; set; } = new List<Warranty>();
}
