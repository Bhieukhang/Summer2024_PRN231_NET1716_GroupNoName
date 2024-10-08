﻿namespace JewelrySalesSystem_NoName_FE;

public partial class Promotion
{
    public Guid Id { get; set; }

    public string? PromotionName { get; set; }

    public string? Type { get; set; }

    public string? Description { get; set; }

    public int? ProductQuantity { get; set; }

    public int? Percentage { get; set; }

    public DateTime? InsDate { get; set; }

    public DateTime? UpsDate { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual ICollection<ProductConditionGroup> ProductConditionGroups { get; } = new List<ProductConditionGroup>();

    public virtual ICollection<Program> Programs { get; } = new List<Program>();
}
