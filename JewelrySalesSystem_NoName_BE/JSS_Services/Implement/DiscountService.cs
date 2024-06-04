using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;
using JSS_DataAccessObjects;
using JSS_Repositories;
using JSS_Services.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_Services.Implement
{
    public class DiscountService : BaseService<DiscountService>, IDiscountService
    {
        public DiscountService(IUnitOfWork<JewelrySalesSystemContext> unitOfWork, ILogger<DiscountService> logger) : base(unitOfWork, logger)
        {
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
    }
}
