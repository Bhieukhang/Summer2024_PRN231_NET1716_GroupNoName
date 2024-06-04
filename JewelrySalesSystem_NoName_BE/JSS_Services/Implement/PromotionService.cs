using Azure.Messaging;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using JSS_BusinessObjects;
using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Request;
using JSS_DataAccessObjects;
using JSS_Repositories;
using JSS_Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;
using static JSS_BusinessObjects.AppConstant;

namespace JSS_Services.Implement
{
    public class PromotionService : BaseService<ProductService>, IPromotionService
    {
        public PromotionService(IUnitOfWork<JewelrySalesSystemContext> unitOfWork,
            ILogger<ProductService> logger) : base(unitOfWork, logger)
        {
        }

        public async Task<IEnumerable<Promotion>> GetAllPromotionsAsync()
        {
            return await _unitOfWork.GetRepository<Promotion>().GetListAsync();
        }

        public async Task<Promotion> GetPromotionByIdAsync(Guid id)
        {
            return await _unitOfWork.GetRepository<Promotion>().FirstOrDefaultAsync(a => a.Id == id);
        }

        
    }
}
