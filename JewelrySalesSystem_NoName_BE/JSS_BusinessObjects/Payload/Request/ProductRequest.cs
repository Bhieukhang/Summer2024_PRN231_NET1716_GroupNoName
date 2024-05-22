using JSS_BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.Payload.Request
{
    public class ProductRequest
    {
        public Guid Id { get; set; }

        public string? ProductName { get; set; }

        public string? Description { get; set; }

        public double? CaptitalPrice { get; set; }

        public double? Size { get; set; }

        public double? Price { get; set; }

        public DateTime? InsDate { get; set; }

        public bool? Deflag { get; set; }

        public Guid CategoryId { get; set; }

        public DateTime? UpsDate { get; set; }

        public int? Quantity { get; set; }

        public double? Accessory { get; set; }

        public Guid? ProductMaterialId { get; set; }

        public string? Code { get; set; }

        public virtual Category Category { get; set; } = null!;

        public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();

        public virtual ICollection<ProductConditionGroup> ProductConditionGroups { get; } = new List<ProductConditionGroup>();

        public virtual ICollection<ProductMaterial> ProductMaterials { get; } = new List<ProductMaterial>();
    }
}
