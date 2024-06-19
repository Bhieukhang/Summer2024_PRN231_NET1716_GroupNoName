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
        public ProductResponse() { }

        public ProductResponse(Guid id, string? imgProduct, string? productName, string? description, double? size,
            double? sellingPrice, int? quantity, Guid? categoryId, Guid? materialId, string? code,
            double? importPrice, DateTime? insDate, double? processPrice, bool? deflag, double? tax, Guid? subId, Category? category)
        {
            Id = id;
            ImgProduct = imgProduct;
            ProductName = productName;
            Description = description;
            Size = size;
            SellingPrice = sellingPrice;
            Quantity = quantity;
            CategoryId = categoryId;
            MaterialId = materialId;
            Code = code;
            ImportPrice = importPrice;
            InsDate = insDate;
            ProcessPrice = processPrice;
            Deflag = deflag;
            Tax = tax;
            SubId = subId;
            Category = category;
        }
        public Guid Id { get; set; }

        public string? ProductName { get; set; }

        public string? Description { get; set; }

        public double? ImportPrice { get; set; }

        public double? Size { get; set; }

        public double? SellingPrice { get; set; }

        public DateTime? InsDate { get; set; }

        public bool? Deflag { get; set; }

        public Guid? CategoryId { get; set; }

        public DateTime? UpsDate { get; set; }

        public int? Quantity { get; set; }

        public double? ProcessPrice { get; set; }

        public Guid? MaterialId { get; set; }

        public string? Code { get; set; }

        public string? ImgProduct { get; set; }
        public double? Tax { get; set; }
        public Guid? SubId { get; set; }
        public virtual Category? Category { get; set; }
        public virtual Material? Material { get; set; }

        //public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();

        //public virtual ICollection<ProductConditionGroup> ProductConditionGroups { get; } = new List<ProductConditionGroup>();
        public virtual ICollection<Diamond> Diamonds { get; } = new List<Diamond>();
    }
}
