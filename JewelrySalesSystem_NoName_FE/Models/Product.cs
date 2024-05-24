﻿using System;
using System.Collections.Generic;

namespace JewelrySalesSystem_NoName_FE;

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

    public string Accessory { get; set; }

    public string Category { get; set; }
}
