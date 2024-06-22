using JewelrySalesSystem_NoName_FE.DTOs.Product;

namespace JewelrySalesSystem_NoName_FE.DTOs.Material
{
    public class MaterialDTO
    {
        public Guid Id { get; set; }

        public string? MaterialName { get; set; }

        public DateTime? InsDate { get; set; }

        public virtual ICollection<ProductDTO> Products { get; } = new List<ProductDTO>();
    }
}
