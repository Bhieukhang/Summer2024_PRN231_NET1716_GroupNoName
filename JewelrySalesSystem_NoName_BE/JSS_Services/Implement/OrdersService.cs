using JSS_BusinessObjects;
using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;
using JSS_DataAccessObjects;
using JSS_Repositories;
using JSS_Services.Interface;
using Microsoft.EntityFrameworkCore;
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
            List<OrderDetailRequest> productList = newData.Details;
            bool isCheckPromotion = false;
            if (newData.PromotionId.HasValue)
            {
                isCheckPromotion = await CheckPromotion(newData.PromotionId.Value, productList);
            }
            //Check promotion - true => TotalPrice = TotalPrice - (TotalPrice*Percentage/100)
            List<OrderDetail> listOrderDetail = new List<OrderDetail>();
            double? totalPrice = 0;
            var customer = await _unitOfWork.GetRepository<Account>().FirstOrDefaultAsync(a => a.Phone == newData.CustomerPhone);
            Order order = new Order()
            {
                Id = Guid.NewGuid(),
                CustomerId = customer.Id,
                PromotionId = null,
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
            if (isCheckPromotion)
            {
                //Caculate total price by promotion
                totalPrice = await CalculateTotalPriceByPromotion((Guid)newData.PromotionId, (double)totalPrice);
                order.PromotionId = newData.PromotionId;
            }
            order.TotalPrice = totalPrice;
            //Save order
            await _unitOfWork.GetRepository<Order>().InsertAsync(order);

            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (isSuccessful == false) return null;
            return new OrderResponse(order.Id, order.CustomerId, order.Type, order.InsDate, order.TotalPrice,
                                     order.MaterialProcessPrice, order.DiscountId, order.PromotionId);
        }

        public async Task<OrderResponse> CreateOrderList(OrderRequestList newData)
        {
            List<OrderDetailPromotionRequest> productList = newData.Details;
            bool isCheckPromotion = false;
            //if (newData.PromotionId.HasValue)
            //{
            //    isCheckPromotion = await CheckPromotion(newData.PromotionId.Value, productList);
            //}
            //Check promotion - true => TotalPrice = TotalPrice - (TotalPrice*Percentage/100)
            List<OrderDetail> listOrderDetail = new List<OrderDetail>();
            double? totalPrice = 0;
            var customer = await _unitOfWork.GetRepository<Account>().FirstOrDefaultAsync(a => a.Phone.Equals(newData.CustomerPhone));
            Order order = new Order()
            {
                Id = Guid.NewGuid(),
                CustomerId = customer.Id,
                PromotionId = null,
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
            //Caculate total price by promotion
            foreach(var orderDetail in productList)
            {
                totalPrice += await CalculateTotalPriceByPromotion((Guid)orderDetail.PromotionId, (double)totalPrice);
            }

            //order.PromotionId = newData.PromotionId;

            order.TotalPrice = totalPrice;
            //Save order
            await _unitOfWork.GetRepository<Order>().InsertAsync(order);

            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (isSuccessful == false) return null;
            return new OrderResponse(order.Id, order.CustomerId, order.Type, order.InsDate, order.TotalPrice,
                                     order.MaterialProcessPrice, order.DiscountId, order.PromotionId);
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

            return orders.Select(o => new OrderResponse(o.Id, o.CustomerId, o.Type, o.InsDate, o.TotalPrice, o.MaterialProcessPrice, o.DiscountId, o.PromotionId));
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
                    order.DiscountId,
                    order.PromotionId
                );

                return orderResponse;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting the order by ID");
                throw;
            }
        }

        //Check product in promotion
        public async Task<bool> CheckPromotion(Guid PromotionId, List<OrderDetailRequest> listProducts)
        {
            List<ProductConditionGroup> listProductConditionGroup = new List<ProductConditionGroup>();
            int countProduct = 0;
            //Case: Expiring promotion
            var promotion = await _unitOfWork.GetRepository<Promotion>().FirstOrDefaultAsync(p => p.Id == PromotionId && p.StartDate <= DateTime.Now && p.EndDate >= DateTime.Now,
                                                                         include: p => p.Include(p => p.ProductConditionGroups));
            if (promotion == null) return false;
            foreach (var itemProduct in listProducts)
            {
                foreach (var product in promotion.ProductConditionGroups)
                {
                    if (itemProduct.ProductId == product.ProductId)
                    {
                        countProduct++;
                    }
                }
            }
            if (countProduct == promotion.ProductQuantity)
            {
                return true;
            }
            else if (countProduct == 0) return false;
            else
            {
                throw new AppConstant.MessageError((int)AppConstant.ErrCode.Internal_Server_Error, AppConstant.ErrMessage.PromotionIllegal);
                return false;
            }
        }

        public async Task<double> CalculateTotalPriceByPromotion(Guid PromotionId, double price)
        {
            var promotion = await _unitOfWork.GetRepository<Promotion>().FirstOrDefaultAsync(x => x.Id == PromotionId);

            int p = (int)promotion.Percentage;
            double per = (double)promotion.Percentage / 100.0;
            double descreadPrice = price * per;
            double TotalPrice = price - descreadPrice;
            return TotalPrice;
        }

        public async Task<int> GetTotalOrdersByDay(DateTime date)
        {
            var orders = await _unitOfWork.GetRepository<Order>().GetListAsync();
            var totalOrders = orders.Count(o => o.InsDate.HasValue && o.InsDate.Value.Date == date.Date);

            return totalOrders;
        }

        public async Task<int> GetTotalOrdersByMonth(int month, int year)
        {
            var repository = _unitOfWork.GetRepository<Order>();
            var totalOrders = await repository.CountAsync(o => o.InsDate.HasValue && o.InsDate.Value.Month == month && o.InsDate.Value.Year == year);
            return totalOrders;
        }

        public async Task<int> GetTotalOrdersByYear(int year)
        {
            var orders = await _unitOfWork.GetRepository<Order>().GetListAsync();
            var totalOrders = orders.Count(o => o.InsDate.HasValue && o.InsDate.Value.Year == year);
            return totalOrders;
        }

    }
}