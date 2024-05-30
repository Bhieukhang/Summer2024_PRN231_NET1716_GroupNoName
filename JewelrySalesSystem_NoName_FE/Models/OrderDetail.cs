using System;
using System.Collections.Generic;
using JewelrySalesSystem_NoName_FE.DTOs.Product;

namespace JewelrySalesSystem_NoName_FE;

public partial class OrderDetail
{
    public Guid Id { get; set; }

    public double? Amount { get; set; }

    public int? Quantity { get; set; }

    public double? Discount { get; set; }

    public double? TotalPrice { get; set; }

    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    public DateTime? InsDate { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual ProductDTO Product { get; set; } = null!;

    public virtual ICollection<Warranty> Warranties { get; } = new List<Warranty>();
}
