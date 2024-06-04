using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.Payload.Request
{
    public class DiscountRequest
    {
        public Guid OrderId { get; set; }

        public Guid ManagerId { get; set; }

        public int? PercentDiscount { get; set; }

        public string? Description { get; set; }

        public double? ConditionDiscount { get; set; }

    }
}
