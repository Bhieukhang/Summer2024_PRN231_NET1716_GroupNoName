using JewelrySalesSystem_NoName_FE.DTOs.Diamonds;

namespace JewelrySalesSystem_NoName_FE.Responses
{
    public class DiamondListResponse
    {
        public int Size { get; set; }
        public int Page { get; set; }
        public int TotalPages { get; set; }
        public List<DiamondDTO> DiamondList { get; set; }
    }
}
