using JSS_BusinessObjects;
using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;
using JSS_DataAccessObjects;
using JSS_Repositories;
using JSS_Repositories.Repo.Interface;
using JSS_Services.Interface;
using Microsoft.Extensions.Logging;
using static JSS_BusinessObjects.AppConstant;

namespace JSS_Services.Implement
{
    public class DiscountService : IDiscountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DiscountService> _logger;

        public DiscountService(IUnitOfWork unitOfWork, ILogger<DiscountService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<bool> CreateDiscountAsync(DiscountRequest request)
        {
            var account = await _unitOfWork.DiscountRepository.FirstOrDefaultAsync();

            var discountItem = new Discount()
            {
                Id = Guid.NewGuid(),
                OrderId = request.OrderId,
                ManagerId = account.ManagerId,
                PercentDiscount = request.PercentDiscount,
                Description = request.Description,
                ConditionDiscount = request.ConditionDiscount,
                Status = DiscountStatus.Pending,
                Note = null,
                InsDate = DateTime.Now,
                UpsDate = DateTime.Now,
            };
            await _unitOfWork.DiscountRepository.InsertAsync(discountItem);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> AcceptDiscountAsync(DiscountRequest request)
        {
            var discountItem = await _unitOfWork
                .DiscountRepository
                .FirstOrDefaultAsync(x => x.Id == request.Id);
            discountItem.Status = request.Status;
            discountItem.OrderId = request.OrderId;
            discountItem.PercentDiscount = request.PercentDiscount;
            discountItem.ConditionDiscount = request.ConditionDiscount;
            discountItem.Description = request.Description;
            discountItem.UpsDate = DateTime.Now;
            discountItem.Note = request.Note;
            _unitOfWork.DiscountRepository.UpdateAsync(discountItem);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<IEnumerable<Discount>> GetAsync(string search)
        {
            var a = await _unitOfWork.DiscountRepository.GetListAsync();
            return a.Where(i => (i.Description ?? "").Contains(search)).ToList();
        }

        public async Task<Discount> FindAsync(Guid id) => await _unitOfWork.DiscountRepository
            .FirstOrDefaultAsync(x => x.Id == id);

        public async Task<DiscountResponse> ConfirmDiscountToManager(DiscountRequest confirm)
        {
            if (confirm.OrderId == null) { }
            var checkOrder = await _unitOfWork.DiscountRepository.FirstOrDefaultAsync(o => o.OrderId == confirm.OrderId);
            if (checkOrder != null) { throw new AppConstant.MessageError((int)AppConstant.ErrCode.Conflict, AppConstant.ErrMessage.DiscountExit); }
            Discount discountItem = new Discount()
            {
                Id = Guid.NewGuid(),
                OrderId = confirm.OrderId,
                ManagerId = (Guid)confirm.ManagerId,
                PercentDiscount = confirm.PercentDiscount,
                Description = "Chiết khấu đơn" + confirm.OrderId,
                ConditionDiscount = confirm.ConditionDiscount,
                Status = "Pending",
                Note = null,
                InsDate = DateTime.Now,
                UpsDate = DateTime.Now,
            };

            await _unitOfWork.DiscountRepository.InsertAsync(discountItem);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (isSuccessful == false) return null;
            return new DiscountResponse(discountItem.Id, discountItem.OrderId, discountItem.ManagerId, discountItem.PercentDiscount,
                                    discountItem.Description, discountItem.ConditionDiscount, discountItem.Status, discountItem.Note);
        }

        public async Task<DiscountResponse> GetDiscountAccept(Guid id)
        {
            var discount = await _unitOfWork.DiscountRepository.FirstOrDefaultAsync(a => a.OrderId == id);
                                                                    //&& a.Status == DiscountStatus.Accepted);
            if (discount == null)
            {
                return new DiscountResponse { };
            }
            else
            {
                if (discount.Status == DiscountStatus.Accepted || discount.Status == DiscountStatus.Rejected)
                {
                    return new DiscountResponse(discount.Id, discount.OrderId, discount.ManagerId, discount.PercentDiscount,
               discount.Description, discount.ConditionDiscount, discount.Status, discount.Note);
                }                
            }
            return new DiscountResponse(discount.Id, discount.OrderId, discount.ManagerId, discount.PercentDiscount,
               discount.Description, discount.ConditionDiscount, discount.Status, discount.Note);

        }
        public static class DiscountStatus
        {
            public const string Pending = "Pending";
            public const string Accepted = "Accepted";
            public const string Rejected = "Rejected";
        }

    }
}
