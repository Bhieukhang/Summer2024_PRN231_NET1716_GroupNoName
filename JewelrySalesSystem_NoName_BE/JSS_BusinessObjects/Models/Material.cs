﻿using System;
using System.Collections.Generic;

namespace JSS_BusinessObjects.Models;

public partial class Material
{
    public Guid Id { get; set; }

    public string? MaterialName { get; set; }

    public DateTime? InsDate { get; set; }

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
