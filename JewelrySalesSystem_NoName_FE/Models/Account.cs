using System;
using System.Collections.Generic;

namespace JewelrySalesSystem_NoName_FE;

public partial class Account
{
    public Guid Id { get; set; }

    public string? FullName { get; set; }

    public string Phone { get; set; } = null!;

    public DateTime? Dob { get; set; }

    public string Password { get; set; } = null!;

    public string? Address { get; set; }

    public string? ImgUrl { get; set; }

    public string? Status { get; set; }

    public bool? Deflag { get; set; }

    public Guid RoleId { get; set; }

    public DateTime? InsDate { get; set; }

    public DateTime? UpsDate { get; set; }

    public virtual ICollection<Membership> Memberships { get; } = new List<Membership>();

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Stall> Stalls { get; } = new List<Stall>();
}
