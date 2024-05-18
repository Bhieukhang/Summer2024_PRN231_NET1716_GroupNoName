using System;
using System.Collections.Generic;

namespace JSS_BusinessObjects.Models;

public partial class Transaction
{
    public Guid Id { get; set; }

    public Guid? OrderId { get; set; }

    public string? Description { get; set; }

    public string? Currency { get; set; }

    public double? TotalPrice { get; set; }

    public virtual Order? Order { get; set; }
}
