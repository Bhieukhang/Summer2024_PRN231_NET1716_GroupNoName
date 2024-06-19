using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// Do Huu Thuan
namespace JSS_BusinessObjects.Payload.Response
{
    public class PurchasePriceResponse
    {
        public int PurchasePriceId { get; set; }
        public string PurchasePrice1 { get; set; }
        public double? Size { get; set; }
        public Guid? CategoryId { get; set; }
        public string Description { get; set; }
        public DateTime? UpsDate { get; set; }
        public bool? Status { get; set; }

        public PurchasePriceResponse(int purchasePriceId, string purchasePrice1, double? size, Guid? categoryId, string description, DateTime? upsDate, bool? status)
        {
            PurchasePriceId = purchasePriceId;
            PurchasePrice1 = purchasePrice1;
            Size = size;
            CategoryId = categoryId;
            Description = description;
            UpsDate = upsDate;
            Status = status;
        }
    }
}
