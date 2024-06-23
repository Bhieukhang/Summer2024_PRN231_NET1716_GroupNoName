using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.Payload.Response
{
    public class MembershipResponse
    {
        public MembershipResponse() { }
        public MembershipResponse(Guid id, string? name, int? point, int? redeemPoint, Guid userId, double? userMoney,
            bool? deflag)
        {
            Id = id;
            Name = name;
            Point = point;
            RedeemPoint = redeemPoint;
            UserId = userId;
            UsedMoney = userMoney;
            Deflag = deflag;
        }
        public MembershipResponse(Guid id, string? name, int? point, int? redeemPoint, Guid userId, double? userMoney,
            bool? deflag, string level) 
        {
            Id = id;
            Name = name;
            Point = point;
            RedeemPoint = redeemPoint;
            UserId = userId;
            UsedMoney = userMoney;
            Deflag = deflag;
            Level = level;
        }
        public Guid Id { get; set; }

        public string? Name { get; set; }

        public string Level { get; set; }

        public int? Point { get; set; }

        public int? RedeemPoint { get; set; }

        public Guid UserId { get; set; }

        public double? UsedMoney { get; set; }

        public bool? Deflag { get; set; }
    }
}