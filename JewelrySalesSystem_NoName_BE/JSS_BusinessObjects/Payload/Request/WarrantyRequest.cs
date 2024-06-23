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
        public Guid ConditionWarrantyId { get; set; }
        public Guid? OrderDetailId { get; set; }
        public string? Note { get; set; }
        public List<ConditionMap> ConditionMap { get; set; }
    }

    public class ConditionMap
    {
        public Guid ConditionWarrantyId { get; set; }
    }

    public class WarrantyUpdateRequest
    {
        public DateTime? DateOfPurchase { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public int? Period { get; set; }
        public bool Deflag { get; set; }
        public string Status { get; set; }
        public Guid ConditionWarrantyId { get; set; }

        public Guid? OrderDetailId { get; set; }
        public string? Note { get; set; }
    }
}
