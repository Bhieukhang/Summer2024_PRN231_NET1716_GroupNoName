using System;
using System.Collections.Generic;

namespace JSS_BusinessObjects.Models;

public partial class Order
{
    public Guid Id { get; set; }

    public Guid CustomerId { get; set; }

    public string? Type { get; set; }

    public DateTime? InsDate { get; set; }

    public Guid? DiscountId { get; set; }

    public double? TotalPrice { get; set; }

    public double? MaterialProcessPrice { get; set; }

    public virtual Account Customer { get; set; } = null!;

    public virtual Discount? Discount { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();

    public virtual ICollection<Transaction> Transactions { get; } = new List<Transaction>();
}
