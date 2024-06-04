using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.Payload.Response
{
    public class DiscountResponse
    {
        public DiscountResponse(Guid id, Guid orderId, Guid managerId, int? percentDiscount, string? description,
            double? conditionDiscount, string? status, string? note)
        {
            Id = id; 
            OrderId = orderId; 
            ManagerId = managerId;
            PercentDiscount = percentDiscount;
            Description = description;
            ConditionDiscount = conditionDiscount;
            Status = status;
            Note = note;
        }
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public Guid ManagerId { get; set; }

        public int? PercentDiscount { get; set; }

        public string? Description { get; set; }

        public double? ConditionDiscount { get; set; }

        public string? Status { get; set; }

        public DateTime? InsDate { get; set; }

        public DateTime? UpsDate { get; set; }

        public string? Note { get; set; }

    }
}
