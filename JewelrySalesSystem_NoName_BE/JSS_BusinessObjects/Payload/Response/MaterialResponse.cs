﻿using JSS_BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.Payload.Response
{
    public class MaterialResponse
    {
        public MaterialResponse(Guid id, string? materialName, double? weight)
        {
            Id = id;
            MaterialName = materialName;
            Weight = weight;
        }

        public Guid Id { get; set; }

        public string? MaterialName { get; set; }

        public double? Weight { get; set; }

        public DateTime? InsDate { get; set; }

        public Guid ProductMaterialId { get; set; }

        public bool? Deflag { get; set; }

        public virtual ICollection<ProductMaterial> ProductMaterials { get; } = new List<ProductMaterial>();
    }
}
