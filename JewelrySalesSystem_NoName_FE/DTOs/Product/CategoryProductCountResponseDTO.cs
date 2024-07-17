namespace JewelrySalesSystem_NoName_FE.DTOs.Product
{
    public class CategoryProductCountResponseDTO
    {
        public Guid? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ProductCount { get; set; }
        public List<ProductDTO> Products { get; set; }
    }
}
