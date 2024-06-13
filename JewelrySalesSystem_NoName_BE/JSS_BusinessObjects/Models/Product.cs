using System;
using System.Collections.Generic;

namespace JSS_BusinessObjects.Models;

public partial class Product
{
    public Guid Id { get; set; }

    public string? ProductName { get; set; }

    public string? Description { get; set; }

    public double? ImportPrice { get; set; }

    public double? Size { get; set; }

    public double? SellingPrice { get; set; }

    public DateTime? InsDate { get; set; }

    public bool? Deflag { get; set; }

    public Guid? CategoryId { get; set; }

    public DateTime? UpsDate { get; set; }

    public int? Quantity { get; set; }

    public double? ProcessPrice { get; set; }

    public Guid? MaterialId { get; set; }

    public string? Code { get; set; }

    public string? ImgProduct { get; set; }

    public double? Tax { get; set; }
    public Guid? SubId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Diamond> Diamonds { get; } = new List<Diamond>();

    public virtual Material? Material { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();

    public virtual ICollection<ProductConditionGroup> ProductConditionGroups { get; } = new List<ProductConditionGroup>();

    // Tính giá nhập vào
    //public double CalculateImportPrice()
    //{
    //    return (ImportPrice ?? 0);
    //}
}
