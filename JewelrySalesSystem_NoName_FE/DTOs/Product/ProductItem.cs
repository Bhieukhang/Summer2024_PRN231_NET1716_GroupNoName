namespace JewelrySalesSystem_NoName_FE.DTOs.Product
{
    public class ProductItem
    {
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
        public string? Tax { get; set; }
    }

    public class ProductResponse
    {
        public ProductItem Product { get; set; }
        public List<Promotion> Promotions { get; set; }
    }

}
