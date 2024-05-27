﻿using JSS_BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.Payload.Request
{
    public class ProductRequest
    {
        public string? ProductName { get; set; }

        public string? Description { get; set; }

        public double? TotalPrice { get; set; }

        public double? Size { get; set; }

        public double? ImportPrice { get; set; }

        public DateTime? InsDate { get; set; }

        public bool? Deflag { get; set; }

        public Guid CategoryId { get; set; }

        public int? Quantity { get; set; }
        public double? ProcessPrice { get; set; }
        public Guid? AccessoryId { get; set; }

        public Guid? ProductMaterialId { get; set; }
        public string? Code { get; set; }
        public string? ImgProduct { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();

        public virtual ICollection<ProductConditionGroup> ProductConditionGroups { get; } = new List<ProductConditionGroup>();
    }
}
