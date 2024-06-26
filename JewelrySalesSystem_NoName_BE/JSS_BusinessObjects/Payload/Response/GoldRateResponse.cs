using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.Payload.Response
{
    public class GoldRateResponse
    {
        public GoldRateResponse(Guid id, double? rate, DateTime? upsDate)
        {
            Id = id;
            Rate = rate;
            UpsDate = upsDate;
        }
        public Guid Id { get; set; }

        public double? Rate { get; set; }

        public DateTime? UpsDate { get; set; }
    }
}
