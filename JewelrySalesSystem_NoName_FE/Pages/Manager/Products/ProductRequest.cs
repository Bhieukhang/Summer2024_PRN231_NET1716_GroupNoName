
using JewelrySalesSystem_NoName_FE.DTOs.Material;
using JewelrySalesSystem_NoName_FE.DTOs.Product;
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
        public Guid? SubId { get; set; }
        public int? Quantity { get; set; }
        public double? ProcessPrice { get; set; }
        public Guid? MaterialId { get; set; }
        public string Code { get; set; }
        public string ImgProduct { get; set; }
        public double? Tax { get; set; }
        public int? PeriodWarranty { get; set; }
        public CategoryDTO? Category { get; set; }
        public MaterialDTO? Material { get; set; }
        public SubProductsDTO? SubProductsDTO { get; set; }

        [NotMapped] 
        public bool DeflagChecked
        {
            get => Deflag.GetValueOrDefault();
            set => Deflag = value;
        }
    }
}