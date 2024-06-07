
using System.ComponentModel.DataAnnotations.Schema;

namespace JewelrySalesSystem_NoName_FE.Pages.Manager.Products
{
    internal class ProductRequest
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public double? SellingPrice { get; set; }
        public double? Size { get; set; }
        public double? ImportPrice { get; set; }
        public DateTime? InsDate { get; set; }
        public bool? Deflag { get; set; }
        public Guid? CategoryId { get; set; }
        public int? Quantity { get; set; }
        public double? ProcessPrice { get; set; }
        public Guid? MaterialId { get; set; }
        public string Code { get; set; }
        public string ImgProduct { get; set; }
        public double? Tax { get; set; }
        public CategoryDTO? Category { get; set; }

        [NotMapped] 
        public bool DeflagChecked
        {
            get => Deflag.GetValueOrDefault();
            set => Deflag = value;
        }
    }
}