using System;
using System.Collections.Generic;

namespace BusinessObjects.Mo
{
    public partial class ProcessPrice
    {
        public int ProcesspriceId { get; set; }
        public double? ProcessPrice1 { get; set; }
        public double? Size { get; set; }
        public Guid? CategoryId { get; set; }
        public string Description { get; set; }
        public DateTime? UpsDate { get; set; }
        public bool? Status { get; set; }
    }
}
