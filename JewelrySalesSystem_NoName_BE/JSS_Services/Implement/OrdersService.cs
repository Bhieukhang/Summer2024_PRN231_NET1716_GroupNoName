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
using System.Data.Entity;
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
            }
            order.TotalPrice = totalPrice;
            //Save order
            await _unitOfWork.GetRepository<Order>().InsertAsync(order);

            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (isSuccessful == false) return null;
            return new OrderResponse(order.Id, order.CustomerId, order.Type, order.InsDate, order.TotalPrice,
                                     order.MaterialProcessPrice, order.DiscountId);
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
            foreach (var orderDetail in productList)
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
                                     order.MaterialProcessPrice, order.DiscountId);
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

            return orders.Select(o => new OrderResponse(o.Id, o.CustomerId, o.Type, o.InsDate, o.TotalPrice,
                o.MaterialProcessPrice, o.DiscountId));
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
                    order.DiscountId
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

        public async Task<Dictionary<int, int>> GetTotalOrdersByYearGroupedByMonth(int year)
        {
            var orders = await _unitOfWork.GetRepository<Order>()
                              .Where(order => order.OrderDate.Year == year)
                              .GroupBy(order => order.OrderDate.Month)
                              .Select(group => new
                              {
                                  Month = group.Key,
                                  TotalOrders = group.Count()
                              })
                              .ToDictionaryAsync(g => g.Month, g => g.TotalOrders);

            for (int month = 1; month <= 12; month++)
            {
                if (!orders.ContainsKey(month))
                {
                    orders[month] = 0;
                }
            }

            return orders;
        }

        public async Task<IEnumerable<OrderResponse>> GetAllOrders()
        {
            var orders = await _unitOfWork.GetRepository<Order>().GetListAsync();

            // Chuyển đổi danh sách đơn hàng sang danh sách OrderResponse
            var orderResponses = orders.Select(o => new OrderResponse(
                o.Id,
                o.CustomerId,
                o.Type,
                o.InsDate,
                o.TotalPrice,
                o.MaterialProcessPrice,
                o.DiscountId
            ));

            return orderResponses;
        }
        public async Task<CustomerOrderResponse?> GetListOrderByCustomerPhone(string phone)
        {
            var customer = await _unitOfWork.GetRepository<Account>().FirstOrDefaultAsync(c => c.Phone == phone, include: c => c.Include(c => c.Orders));
            if (customer != null)
            {
                var inforCustomer = new InforCustomer(customer.Id, customer.FullName, customer.Phone, customer.Address, customer.ImgUrl);

                var listOrder = new List<OrderResponse>();
                foreach (var order in customer.Orders)
                {
                    var orderItem = new OrderResponse()
                    {
                        Id = order.Id,
                        CustomerId = order.CustomerId,
                        Type = order.Type,
                        InsDate = order.InsDate,
                        TotalPrice = order.TotalPrice,
                        MaterialProcessPrice = order.MaterialProcessPrice
                    };
                    listOrder.Add(orderItem);
                }

                // Tạo đối tượng CustomerOrderResponse và gán giá trị
                var customerOrder = new CustomerOrderResponse(inforCustomer, listOrder);

                return customerOrder;
            }
            return null;
        }
        public async Task<OrderDetailList> GetListOrderDetailByIdAsync(Guid id)
        {
            try
            {
                var order = await _unitOfWork.GetRepository<Order>().FirstOrDefaultAsync(a => a.Id == id,
                                                                         include: a => a.Include(a => a.OrderDetails));
                if (order == null)
                {
                    return null;
                }
                OrderDetailList orderItem = new OrderDetailList();
                orderItem.OrderId = id;
                List<OrderDetailResponse> listDetail = new List<OrderDetailResponse>();
                foreach (var item in order.OrderDetails)
                {
                    OrderDetailResponse o = new OrderDetailResponse()
                    {
                        Id = item.Id,
                        Amount = item.Amount,
                        Quantity = item.Quantity,
                        Discount = item.Discount,
                        TotalPrice = item.TotalPrice,
                        OrderId = item.OrderId,
                        ProductId = item.ProductId,
                        InsDate = item.InsDate,
                        OrderDetailId = item.Id
                    };
                    listDetail.Add(o);
                }
                orderItem.ListOrder = listDetail;

                return orderItem;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting the order by ID");
                throw;
            }
        }
    }
}