using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;

namespace JSS_Services.Interface
{
    public interface IDiscountService
    {
        Task<DiscountResponse> ConfirmDiscountToManager(DiscountRequest confirmData);
        Task<bool> CreateDiscountAsync(DiscountRequest request);
        Task<bool> AcceptDiscountAsync(DiscountRequest request);
        Task<IEnumerable<Discount>> GetAsync(string search);
    }
}
