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
using System.Linq.Expressions;
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
            List<OrderDetail> listOrderDetail = new List<OrderDetail>();
            double? totalPrice = 0;
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
            foreach (var orderDetail in newData.Details)
            {
                OrderDetail detail = new OrderDetail()
                {
                    Id = Guid.NewGuid(),
                    Amount = orderDetail.Amount,
                    Quantity = orderDetail.Quantity,
                    Discount = 0,
                    TotalPrice = orderDetail.Amount * orderDetail.Quantity,
                    OrderId = order.Id,
                    ProductId = orderDetail.ProductId,
                    InsDate = DateTime.Now
                };
                totalPrice += detail.TotalPrice;
                listOrderDetail.Add(detail);
                await _unitOfWork.GetRepository<OrderDetail>().InsertRangeAsync(listOrderDetail);
            }
            order.TotalPrice = totalPrice;
            await _unitOfWork.GetRepository<Order>().InsertAsync(order);

            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (isSuccessful == false) return null;
            return new OrderResponse(order.Id, order.CustomerId, order.Type, order.InsDate, order.TotalPrice,
                                     order.MaterialProcessPrice, order.Discount, order.Promotion);
        }

        public async Task<IEnumerable<OrderResponse>> SearchOrders(Guid? customerId, DateTime? startDate/*, DateTime? endDate*/)
        {
            var orders = await _unitOfWork.GetRepository<Order>().GetListAsync(
                predicate: o => (!customerId.HasValue || o.CustomerId == customerId) &&
                                (!startDate.HasValue || o.InsDate >= startDate)
                                //(!endDate.HasValue || o.InsDate <= endDate)
                //include: source => source.Include(o => o.Discount)
                //                         .Include(o => o.Promotion)
            );

            return orders.Select(o => new OrderResponse(o.Id, o.CustomerId, o.Type, o.InsDate, o.TotalPrice, o.MaterialProcessPrice, o.Discount, o.Promotion));
        }
        public async Task<OrderResponse> GetOrderByIdAsync(Guid id)
        {
            try
            {
                var orderRepository = _unitOfWork.GetRepository<Order>();
                var order = await orderRepository.FirstOrDefaultAsync(a => a.Id == id);

                if (order == null)
                {
                    return null;
                }

                var orderResponse = new OrderResponse(
                    order.Id,
                    order.CustomerId,
                    order.Type,
                    order.InsDate,
                    order.TotalPrice,
                    order.MaterialProcessPrice,
                    order.Discount,
                    order.Promotion
                );

                return orderResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting the order by ID");
                throw;
            }
        }


    }
}
