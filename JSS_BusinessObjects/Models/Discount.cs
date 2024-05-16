using System;
using System.Collections.Generic;

namespace JSS_BusinessObjects.Models;

public partial class Discount
{
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    public Guid ManagerId { get; set; }

    public int? PercentDiscount { get; set; }

    public string? Description { get; set; }

    public double? ConditionDiscount { get; set; }

    public string? Status { get; set; }

    public DateTime? InsDate { get; set; }

    public DateTime? UpsDate { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
