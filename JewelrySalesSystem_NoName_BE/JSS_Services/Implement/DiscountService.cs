using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;
using JSS_DataAccessObjects;
using JSS_Repositories;
using JSS_Services.Interface;
using Microsoft.Extensions.Logging;

namespace JSS_Services.Implement
{
    public class DiscountService : BaseService<DiscountService>, IDiscountService
    {
        public DiscountService(IUnitOfWork<JewelrySalesSystemContext> unitOfWork, ILogger<DiscountService> logger) : base(unitOfWork, logger)
        {
        }

        public async Task<bool> CreateDiscountAsync(DiscountRequest request)
        {
            var discountItem = new Discount()
            {
                Id = Guid.NewGuid(),
                OrderId = request.OrderId,
                ManagerId = request.ManagerId,
                PercentDiscount = request.PercentDiscount,
                Description = request.Description,
                ConditionDiscount = request.ConditionDiscount,
                Status = DiscountStatus.Pending,
                Note = null,
                InsDate = DateTime.Now,
                UpsDate = DateTime.Now,
            };
            await _unitOfWork.GetRepository<Discount>().InsertAsync(discountItem);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> AcceptDiscountAsync(DiscountRequest request)
        {
            var discountItem = await _unitOfWork
                .GetRepository<Discount>()
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            discountItem.Status = DiscountStatus.Accepted;
            _unitOfWork.GetRepository<Discount>().UpdateAsync(discountItem);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<IEnumerable<Discount>> GetAsync(string search)
        {
            var a = await _unitOfWork.GetRepository<Discount>().GetListAsync();
            return a.Where(i => (i.Description ?? "").Contains(search)).ToList();
        }

        public async Task<DiscountResponse> ConfirmDiscountToManager(DiscountRequest confirm)
        {
            Discount discountItem = new Discount()
            {
                Id = Guid.NewGuid(),
                OrderId = confirm.OrderId,
                ManagerId = confirm.ManagerId,
                PercentDiscount = confirm.PercentDiscount,
                Description = confirm.Description,
                ConditionDiscount = confirm.ConditionDiscount,
                Status = "Pending",
                Note = null,
                InsDate = DateTime.Now,
                UpsDate = DateTime.Now,
            };
            await _unitOfWork.GetRepository<Discount>().InsertAsync(discountItem);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (isSuccessful == false) return null;
            return new DiscountResponse(discountItem.Id, discountItem.OrderId, discountItem.ManagerId, discountItem.PercentDiscount,
                                    discountItem.Description, discountItem.ConditionDiscount, discountItem.Status, discountItem.Note);
        }

        public static class DiscountStatus
        {
            public const string Pending = "Pending";
            public const string Accepted = "Accepted";
        }

    }
}
