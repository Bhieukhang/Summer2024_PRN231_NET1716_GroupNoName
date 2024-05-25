using JSS_BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.Payload.Request
{
    public class OrderDetailRequest
    {
        public double? Amount { get; set; }

        public int? Quantity { get; set; }

        public Guid ProductId { get; set; }
    }
}
