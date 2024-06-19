using JSS_BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.Payload.Response
{
    public class WarrantyResponse
    {
        public WarrantyResponse(Guid id, DateTime? dateOfPurchase, DateTime? expirationDate, int? period,
                                string? status, Guid conditionWarrantyMapId, string? note)
        {
            Id = id;
            DateOfPurchase = dateOfPurchase;
            ExpirationDate = expirationDate;
            Period = period;
            Status = status;
            ConditionnWarrantyId = conditionWarrantyMapId;
            Note = note;
        }

        public WarrantyResponse(Guid id, DateTime? dateOfPurchase, DateTime? expirationDate, int? period,
                                bool? deflag, string? status, Guid conditionWarranty, string? note)
        {
            Id = id;
            DateOfPurchase = dateOfPurchase;
            ExpirationDate = expirationDate;
            Period = period;
            Deflag = deflag;
            Status = status;
            ConditionnWarrantyId = conditionWarranty;
            Note = note;
        }

        public Guid Id { get; set; }

        public DateTime? DateOfPurchase { get; set; }

        public DateTime? ExpirationDate { get; set; }

        public int? Period { get; set; }

        public bool? Deflag { get; set; }


        //public Guid? OrderDetailId { get; set; }

        //public string? Phone { get; set; }
        public Guid ConditionnWarrantyId { get; set; }
        public string? Status { get; set; }
        public string? Note { get; set; }

        //public virtual ConditionWarranty ConditionWarranty { get; set; } = null!;
    }

    public class WarrantyCreateResponse
    {
        public Guid listWarrantyId { get; set; }
    }
}
