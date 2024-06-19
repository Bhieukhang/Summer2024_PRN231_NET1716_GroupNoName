using System;
using System.Collections.Generic;

namespace JSS_BusinessObjects.Models;

public partial class SubProduct
{
    public Guid Id { get; set; }

    public string? TitleProductName { get; set; }

    public string? Description { get; set; }

    public int? Status { get; set; }
}
