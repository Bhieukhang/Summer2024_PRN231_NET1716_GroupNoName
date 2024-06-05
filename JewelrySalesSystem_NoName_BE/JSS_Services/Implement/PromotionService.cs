using JSS_BusinessObjects.Models;
using JSS_DataAccessObjects;
using JSS_Repositories;
using JSS_Services.Interface;
using Microsoft.Extensions.Logging;

namespace JSS_Services.Implement
{
    public class PromotionService : BaseService<PromotionService>, IPromotionService
    {
        public PromotionService(IUnitOfWork<JewelrySalesSystemContext> unitOfWork,
            ILogger<PromotionService> logger) : base(unitOfWork, logger)
        {
        }

        public async Task<IEnumerable<Promotion>> GetAllPromotionsAsync(string search)
        {
            var a = await _unitOfWork.GetRepository<Promotion>().GetListAsync();
            return a.Where(i => (i.PromotionName??"").Contains(search)).ToList();
        }

        public async Task<Promotion> GetPromotionByIdAsync(Guid id)
        {
            return await _unitOfWork.GetRepository<Promotion>().FirstOrDefaultAsync(a => a.Id.Equals(id));
        }

        public async Task<Promotion> CreatePromotionAsync(Promotion newData)
        {
            
            try
            {
                await _unitOfWork.GetRepository<Promotion>().InsertAsync(newData);
                bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
                if (!isSuccessful)
                {
                    throw new Exception("Commit failed, no rows affected.");
                }
                return newData;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the Promotion.");
                return null;
            }
        }

        public async Task<Promotion> UpdatePromotionAsync(Promotion updatedData)
        {
            try
            {
                _unitOfWork.GetRepository<Promotion>().UpdateAsync(updatedData);
                bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
                if (!isSuccessful) return null;
                return updatedData;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the Promotion.");
                if (ex.InnerException != null)
                {
                    _logger.LogError(ex.InnerException, "Inner exception details.");
                }
                throw;
            }
        }

        
    }
}
