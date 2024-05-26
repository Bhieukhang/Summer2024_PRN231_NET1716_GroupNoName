using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.Payload.Response
{
    public class ConditionWarrantyResponse
    {
        public ConditionWarrantyResponse(Guid id, string? condition, DateTime? insDate, bool? deflag) 
        {
            Id = id;
            Condition = condition;
            InsDate = insDate;
            Deflag = deflag;
        }
        public Guid Id { get; set; }

        public string? Condition { get; set; }

        public DateTime? InsDate { get; set; }

        public bool? Deflag { get; set; }
    }
}
