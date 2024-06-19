using JSS_BusinessObjects.Payload.Response;
using JSS_BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.Mo;


// Do Huu Thuan
namespace JSS_Services.Interface
{
    public interface IPurchasePriceService
    {
        public Task<IPaginate<PurchasePriceResponse>> GetListPurchasePriceAsync(int page, int size);
        Task<PurchasePrice> UpdatePurchasePriceAsync(int id, PurchasePrice updatedData);
        Task<PurchasePrice> GetPurchasePriceByIdAsync(int id);
    }
}
