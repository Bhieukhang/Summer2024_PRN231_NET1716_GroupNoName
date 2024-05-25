using System;
using System.Collections.Generic;

namespace JSS_BusinessObjects.Models;

public partial class Warranty
{
    public Guid Id { get; set; }

    public DateTime? DateOfPurchase { get; set; }

    public DateTime? ExpirationDate { get; set; }

    public int? Period { get; set; }

    public bool? Deflag { get; set; }

    public Guid OrderDetailId { get; set; }

    public string? Phone { get; set; }

    public Guid ConditionWarrantyId { get; set; }

    public string? Status { get; set; }

    public virtual ConditionWarranty ConditionWarranty { get; set; } = null!;

    public virtual OrderDetail OrderDetail { get; set; } = null!;
}
