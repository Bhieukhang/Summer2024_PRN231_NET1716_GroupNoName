using System;
using System.Collections.Generic;

namespace JSS_BusinessObjects.Models;

public partial class MemberType
{
    public Guid Id { get; set; }

    public string? Type { get; set; }

    public int? Levels { get; set; }

    public double? NextLevel { get; set; }

    public DateTime? InsDate { get; set; }

    public double? PercentDiscount { get; set; }

    public virtual ICollection<Membership> Memberships { get; } = new List<Membership>();
}
