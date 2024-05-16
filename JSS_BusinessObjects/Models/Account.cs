using System;
using System.Collections.Generic;

namespace JSS_BusinessObjects.Models;

public partial class Account
{
    public Guid Id { get; set; }

    public string? FullName { get; set; }

    public string? Phone { get; set; }

    public DateOnly? Dob { get; set; }

    public string Password { get; set; } = null!;

    public string? Address { get; set; }

    public string? ImgUrl { get; set; }

    public string? Status { get; set; }

    public bool? Deflag { get; set; }

    public Guid RoleId { get; set; }

    public DateTime? InsDate { get; set; }

    public DateTime? UpsDate { get; set; }

    public virtual ICollection<Membership> Memberships { get; set; } = new List<Membership>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Stall> Stalls { get; set; } = new List<Stall>();
}
