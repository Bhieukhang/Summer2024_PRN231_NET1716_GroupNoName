﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.Payload.Request
{
    public class PromotionRequest
    {
        public Guid? Id { get; set; }
        public string? PromotionName { get; set; }

        public string? Type { get; set; }

        public string? Description { get; set; }

        public int? ProductQuantity { get; set; }

        public int? Percentage { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public bool? Deflag { get; set; }
    }
}
