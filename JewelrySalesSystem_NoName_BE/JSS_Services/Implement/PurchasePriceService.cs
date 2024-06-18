using BusinessObjects.Mo;
using JSS_BusinessObjects.Payload.Response;
using JSS_BusinessObjects;
using JSS_DataAccessObjects;
using JSS_Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JSS_Services.Interface;


// Do Huu Thuan
namespace JSS_Services.Implement
{
    public class PurchasePriceService : BaseService<PurchasePrice>, IPurchasePriceService
    {
        public PurchasePriceService(IUnitOfWork<JewelrySalesSystemContext> unitOfWork, ILogger<PurchasePrice> logger) : base(unitOfWork, logger)
        {
        }

        public async Task<IPaginate<PurchasePriceResponse>> GetListPurchasePriceAsync(int page, int size)
        {
            IPaginate<PurchasePriceResponse> list = await _unitOfWork.GetRepository<PurchasePrice>().GetList(
                selector: x => new PurchasePriceResponse(x.PurchasePriceId, x.PurchasePrice1, x.Size, x.CategoryId, x.Description, x.UpsDate, x.Status),
                orderBy: x => x.OrderByDescending(x => x.PurchasePriceId),
                page: page,
                size: size); ;
            return list;
        }
    }
}
