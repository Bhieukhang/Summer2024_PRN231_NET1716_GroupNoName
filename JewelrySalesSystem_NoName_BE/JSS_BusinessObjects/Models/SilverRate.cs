using System;
using System.Collections.Generic;

namespace JSS_BusinessObjects.Models;

public partial class SilverRate
{
    public Guid Id { get; set; }

    public double? Rate { get; set; }

    public DateTime? UpsDate { get; set; }
}
