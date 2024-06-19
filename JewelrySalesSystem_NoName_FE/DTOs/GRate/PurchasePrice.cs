using System;
using System.Collections.Generic;

namespace BusinessObjects.Mo
{
    public partial class PurchasePriceDTO
    {
        public int PurchasePriceId { get; set; }
        public string PurchasePrice1 { get; set; }
        public double? Size { get; set; }
        public Guid? CategoryId { get; set; }
        public string Description { get; set; }
        public DateTime? UpsDate { get; set; }
        public bool? Status { get; set; }
    }
}
