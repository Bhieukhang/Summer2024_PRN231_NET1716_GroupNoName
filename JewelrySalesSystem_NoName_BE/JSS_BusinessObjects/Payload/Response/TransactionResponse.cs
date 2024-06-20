using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.Payload.Response
{
    public class TransactionResponse
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public string? Description { get; set; }

        public string? Currency { get; set; }

        public double? TotalPrice { get; set; }

        public DateTime? InsDate { get; set; }
        public OrderResponse Order { get; set; }
    }
}
