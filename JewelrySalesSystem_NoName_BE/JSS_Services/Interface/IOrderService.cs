using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_Services.Interface
{
    public interface IOrderService
    {
        public Task<OrderResponse> CreateOrder(OrderRequest newData);
        Task<IEnumerable<OrderResponse>> SearchOrders(Guid? id, DateTime? insDate, string? phone);

        public Task<OrderResponse> GetOrderByIdAsync(Guid id);

        Task<bool> CheckPromotion(Guid PromotionId, List<OrderDetailRequest> listProducts);
        Task<double> CalculateTotalPriceByPromotion(Guid PromotionId, double price);
        Task<int> GetTotalOrdersByDay(DateTime date);
        Task<int> GetTotalOrdersByMonth(int year, int month);
        Task<int> GetTotalOrdersByYear(int year);
        Task<IEnumerable<OrderResponse>> GetAllOrders();
        Task<OrderResponse> CreateOrderList(OrderRequestList newData);
        Task<CustomerOrderResponse?> GetListOrderByCustomerPhone(string phone);
        Task<OrderDetailList> GetListOrderDetailByIdAsync(Guid id);
    }
}
