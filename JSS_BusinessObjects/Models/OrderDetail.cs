﻿using System;
using System.Collections.Generic;

namespace JSS_BusinessObjects.Models;

public partial class OrderDetail
{
    public Guid Id { get; set; }

    public double? Amount { get; set; }

    public int? Quantity { get; set; }

    public decimal? Discount { get; set; }

    public decimal? TotalPrice { get; set; }

    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    public DateTime? InsDate { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<Warranty> Warranties { get; set; } = new List<Warranty>();
}
