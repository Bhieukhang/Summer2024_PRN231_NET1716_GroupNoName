using System;
using System.Collections.Generic;

namespace JSS_BusinessObjects.Models;

public partial class GoldRate
{
    public Guid Id { get; set; }

    public double? Rate { get; set; }

    public DateTime? UpsDate { get; set; }
}
