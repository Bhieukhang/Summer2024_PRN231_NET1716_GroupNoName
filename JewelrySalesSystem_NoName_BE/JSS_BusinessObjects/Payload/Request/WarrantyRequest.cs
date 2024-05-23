using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.Payload.Request
{
    public class WarrantyRequest
    {
        public DateTime? DateOfPurchase { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public int? Period { get; set; }

        public Guid? OrderDetailId { get; set; }
        public Guid ConditionWarrantyId { get; set; }

    }
}
