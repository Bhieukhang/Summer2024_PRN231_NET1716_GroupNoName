using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.Payload.Request
{
    public class MembershipRequest
    {
        public string? Name { get; set; }

        public int? Level { get; set; }

        public int? Point { get; set; }

        public int? RedeemPoint { get; set; }

        public Guid UserId { get; set; }

        public double? UsedMoney { get; set; }

        public bool? Deflag { get; set; }
    }
}
