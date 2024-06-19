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
        public async Task<PurchasePrice> UpdatePurchasePriceAsync(int id, PurchasePrice updatedData)
        {
            var existingPurchasePrice = await _unitOfWork.GetRepository<PurchasePrice>().FirstOrDefaultAsync(a => a.PurchasePriceId == id);
            if (existingPurchasePrice == null) return null;

            existingPurchasePrice.PurchasePrice1 = updatedData.PurchasePrice1;
            existingPurchasePrice.Size = updatedData.Size;
            existingPurchasePrice.CategoryId = updatedData.CategoryId;
            existingPurchasePrice.Description = updatedData.Description;
            existingPurchasePrice.UpsDate = DateTime.Now;
            existingPurchasePrice.Status = updatedData.Status;


            _unitOfWork.GetRepository<PurchasePrice>().UpdateAsync(existingPurchasePrice);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (!isSuccessful) return null;
            return existingPurchasePrice;
        }
        public async Task<PurchasePrice> GetPurchasePriceByIdAsync(int id)
        {
            try
            {
                var purchasePriceRepository = _unitOfWork.GetRepository<PurchasePrice>();
                var PurchasePrice = await purchasePriceRepository.FirstOrDefaultAsync(s => s.PurchasePriceId == id);
                return PurchasePrice;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting stall by ID");
                throw;
            }
        }
    }
}
