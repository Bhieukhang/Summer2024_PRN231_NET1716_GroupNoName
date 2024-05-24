using JSS_BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_BusinessObjects.Payload.Response
{
    public class ProductResponse
    {
        public ProductResponse(Guid id, string? productName, string? description, double? size, 
            double? totalPrice, int? quantity, Guid? accessoryId,Guid? productMaterialId, string? code)
        {
            Id = id;
            ProductName = productName;
            Description = description;
            Size = size;
            TotalPrice = totalPrice;
            Quantity = quantity;
            AccessoryId = accessoryId;
            ProductMaterialId = productMaterialId;
            Code = code;
        }
        public Guid Id { get; set; }

        public string? ProductName { get; set; }

        public string? Description { get; set; }

        public double? ImportPrice { get; set; }

        public double? Size { get; set; }

        public double? TotalPrice { get; set; }

        public DateTime? InsDate { get; set; }

        //public bool? Deflag { get; set; }

        public Guid? CategoryId { get; set; }

        //public DateTime? UpsDate { get; set; }

        public int? Quantity { get; set; }

        public double? ProcessPrice { get; set; }

        public Guid? ProductMaterialId { get; set; }

        public Guid? AccessoryId { get; set; }

        public string? Code { get; set; }

        public virtual Accessory? Accessory { get; set; }

        public virtual Category? Category { get; set; }

        //public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();

        //public virtual ICollection<ProductConditionGroup> ProductConditionGroups { get; } = new List<ProductConditionGroup>();

        public virtual ICollection<ProductMaterial> ProductMaterials { get; } = new List<ProductMaterial>();
    }
}
