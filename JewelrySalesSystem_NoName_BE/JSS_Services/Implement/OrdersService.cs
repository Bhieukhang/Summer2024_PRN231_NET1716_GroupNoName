using JSS_BusinessObjects;
using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;
using JSS_DataAccessObjects;
using JSS_Repositories;
using JSS_Services.Interface;
using LinqKit;
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
            double totalPayable = 0;
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
                    PromotionId =  orderDetail.PromotionId.HasValue ? orderDetail.PromotionId.Value : (Guid?)null,
                    ProductId = orderDetail.ProductId,
                    InsDate = DateTime.Now
                };
                //totalPrice += detail.TotalPrice;
                listOrderDetail.Add(detail);
                await _unitOfWork.GetRepository<OrderDetail>().InsertRangeAsync(listOrderDetail);
            }
            
            //Update quantity product in store
            foreach (var orderDetail in productList)
            {
                var product = await _unitOfWork.GetRepository<Product>().FirstOrDefaultAsync(p => p.Id == orderDetail.ProductId);
                product.Quantity = product.Quantity - orderDetail.Quantity;
                _unitOfWork.GetRepository<Product>().UpdateAsync(product);
            }

            //Update usermoney for membership
            var membership = await _unitOfWork.GetRepository<Membership>().FirstOrDefaultAsync(x => x.UserId == customer.Id);
            membership.UsedMoney += order.TotalPrice;
            _unitOfWork.GetRepository<Membership>().UpdateAsync(membership);

            //Transaction
            Transaction tran = new Transaction()
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                Description = $"Đã thanh toán đơn {order.Id}",
                Currency = "đ",
                TotalPrice = order.TotalPrice,
                InsDate = DateTime.Now,
            };


            //Save order
            await _unitOfWork.GetRepository<Order>().InsertAsync(order);
            //Save transaction
            _unitOfWork.GetRepository<Transaction>().InsertAsync(tran);

            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (isSuccessful == false) return null;
            return new OrderResponse(order.Id, order.CustomerId, order.Type, order.InsDate, order.TotalPrice,
                                     order.MaterialProcessPrice, order.DiscountId);
        }
        public async Task<IEnumerable<OrderResponse>> SearchOrders(Guid? id, Guid? customerId, DateTime? startDate)
        {
            var orderRepository = _unitOfWork.GetRepository<Order>();

            var predicate = PredicateBuilder.New<Order>(true);

            if (id.HasValue)
            {
                predicate = predicate.And(o => o.Id == id.Value);
            }

            if (customerId.HasValue)
            {
                predicate = predicate.And(o => o.CustomerId == customerId.Value);
            }

            if (startDate.HasValue)
            {
                predicate = predicate.And(o => o.InsDate >= startDate.Value);
            }

            var orders = await orderRepository.GetListAsync(predicate: predicate);

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
        public async Task<IEnumerable<OrderResponse>> GetAllOrders()
        {
            var orders = await _unitOfWork.GetRepository<Order>().GetListAsync(
                orderBy: o => o.OrderByDescending(o => o.InsDate));


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
                                                                         include: a => a.Include(a => a.OrderDetails)
                                                                         .ThenInclude(a => a.Product));
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
                        ProductName = item.Product.ProductName,
                        InsDate= item.InsDate,
                        OrderDetailId = item.Id,
                        PeriodWarranty = item.Product.PeriodWarranty,
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