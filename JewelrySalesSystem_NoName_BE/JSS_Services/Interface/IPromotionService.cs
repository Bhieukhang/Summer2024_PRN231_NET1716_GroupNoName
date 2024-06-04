using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Request;

namespace JSS_Services.Interface
{
    public interface IPromotionService
    {
        Task<IEnumerable<Promotion>> GetAllPromotionsAsync();
        Task<Promotion> GetPromotionByIdAsync(Guid id);
    }
}
