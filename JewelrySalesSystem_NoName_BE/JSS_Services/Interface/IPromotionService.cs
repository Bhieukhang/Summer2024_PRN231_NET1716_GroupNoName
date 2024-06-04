using JSS_BusinessObjects.Models;

namespace JSS_Services.Interface
{
    public interface IPromotionService
    {
        Task<IEnumerable<Promotion>> GetAllPromotionsAsync(string search);
        Task<Promotion> CreatePromotionAsync(Promotion newData);
        Task<Promotion> GetPromotionByIdAsync(Guid id);
        Task<Promotion> UpdatePromotionAsync(Promotion updatedData);
    }
}
