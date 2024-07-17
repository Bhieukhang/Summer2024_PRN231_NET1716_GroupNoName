using JSS_BusinessObjects.Payload.Response;
using JSS_BusinessObjects;
using JSS_DataAccessObjects;
using JSS_Repositories;
using Microsoft.Extensions.Logging;
using JSS_Services.Interface;
using JSS_BusinessObjects.Models;
using JSS_Repositories.Repo.Interface;


namespace JSS_Services.Implement
{
    public class PurchasePriceService : IPurchasePriceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<PurchasePriceService> _logger;

        public PurchasePriceService(IUnitOfWork unitOfWork, ILogger<PurchasePriceService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<IPaginate<PurchasePriceResponse>> GetListPurchasePriceAsync(int page, int size)
        {
            IPaginate<PurchasePriceResponse> list = await _unitOfWork.PurchasePriceRepository.GetList(
                selector: x => new PurchasePriceResponse(x.PurchasePriceId, x.PurchasePrice1, x.Size, x.CategoryId, x.Description, x.UpsDate, x.Status),
                orderBy: x => x.OrderByDescending(x => x.PurchasePriceId),
                page: page,
                size: size); ;
            return list;
        }
        public async Task<PurchasePrice> UpdatePurchasePriceAsync(int id, PurchasePrice updatedData)
        {
            var existingPurchasePrice = await _unitOfWork.PurchasePriceRepository.FirstOrDefaultAsync(a => a.PurchasePriceId == id);
            if (existingPurchasePrice == null) return null;

            existingPurchasePrice.PurchasePrice1 = updatedData.PurchasePrice1;
            existingPurchasePrice.Size = updatedData.Size;
            existingPurchasePrice.CategoryId = updatedData.CategoryId;
            existingPurchasePrice.Description = updatedData.Description;
            existingPurchasePrice.UpsDate = DateTime.Now;
            existingPurchasePrice.Status = updatedData.Status;


            _unitOfWork.PurchasePriceRepository.UpdateAsync(existingPurchasePrice);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (!isSuccessful) return null;
            return existingPurchasePrice;
        }
        public async Task<PurchasePrice> GetPurchasePriceByIdAsync(int id)
        {
            try
            {
                var purchasePriceRepository = _unitOfWork.PurchasePriceRepository;
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
