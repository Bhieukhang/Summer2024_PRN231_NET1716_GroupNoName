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
    public class OrdersService : BaseService<OrdersService>, IOrderService
    {
        public OrdersService(IUnitOfWork<JewelrySalesSystemContext> unitOfWork, ILogger<OrdersService> logger) : base(unitOfWork, logger)
        {
        }

        public async Task<OrderResponse> CreateOrder(OrderRequest newData)
        {
            Order order = new Order()
            {
                Id = Guid.NewGuid(),
                CustomerId = newData.CustomerId,
                PromotionId = newData.PromotionId,
                Type = "BUY",
                InsDate = DateTime.Now,
                DiscountId = newData.DiscountId,
                TotalPrice = newData.TotalPrice,
                MaterialProcessPrice = newData.MaterialProcessPrice,
            };
            await _unitOfWork.GetRepository<Order>().InsertAsync(order);
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (isSuccessful == false) return null;
            return new OrderResponse(order.Id, order.CustomerId, order.Type, order.InsDate, order.TotalPrice,
                                     order.MaterialProcessPrice, order.Status, order.Discount, order.Promotion);
        }
    }
}
