using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Request;

namespace JSS_Services.Interface
{
    public interface IPromotionService
    {
        Task<IEnumerable<Promotion>> GetAllPromotionsAsync(string search);
        Task<Promotion> CreatePromotionAsync(Promotion newData);
        Task<Promotion> GetPromotionByIdAsync(Guid id);
        Task<Promotion> UpdatePromotionAsync(Promotion updatedData);
        Task<List<ProductConditionGroup>> GetPromotionGroups(Guid id);
    }
}
