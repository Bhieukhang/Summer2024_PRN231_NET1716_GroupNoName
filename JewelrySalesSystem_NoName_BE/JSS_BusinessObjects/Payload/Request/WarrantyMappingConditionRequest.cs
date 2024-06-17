using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.Payload.Request
{
    public class WarrantyMappingConditionRequest
    {
        public Guid? WarrantyId { get; set; }
        public Guid? ConditionWarrantyId { get; set; }
    }
}
