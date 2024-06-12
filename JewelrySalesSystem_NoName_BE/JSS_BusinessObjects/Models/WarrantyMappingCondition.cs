using System;
using System.Collections.Generic;

namespace JSS_BusinessObjects.Models;

public partial class WarrantyMappingCondition
{
    public Guid Id { get; set; }

    public Guid? WarrantyId { get; set; }

    public Guid? ConditionWarrantyId { get; set; }

    public DateTime? InsDate { get; set; }

    public virtual ConditionWarranty? ConditionWarranty { get; set; }

    public virtual Warranty? Warranty { get; set; }
}
