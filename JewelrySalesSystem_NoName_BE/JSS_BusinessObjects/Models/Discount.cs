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

    public string? Note { get; set; }

    public Guid? MembershipId { get; set; }

    public int? LevelDiscount { get; set; }

    public virtual Membership? Membership { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
