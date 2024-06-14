using JewelrySalesSystem_NoName_BE.Extenstion;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;
using JSS_Services.Implement;
using JSS_Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JewelrySalesSystem_NoName_BE.Controllers
{
    [ApiController]
    //[Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        #region PostOrder
        /// <summary>
        /// Create new order in stall.
        /// </summary>
        /// <returns>Order item in stall.</returns>
        // POST: api/v1/order
        #endregion
        //[Authorize(Roles = "Staff")]
        [HttpPost(ApiEndPointConstant.Order.OrderEndpoint)]
        public async Task<ActionResult> PostOrder([FromBody] OrderRequest orderData)
        {
            if (orderData.PromotionId == Guid.Empty)
            {
                orderData.PromotionId = null;
            }
            if (orderData.DiscountId == Guid.Empty)
            {
                orderData.DiscountId = null;
            }
            var order = await _service.CreateOrder(orderData);
            var result = JsonConvert.SerializeObject(order, Formatting.Indented);
            return Ok(result);

        }

        #region PostOrderList
        /// <summary>
        /// Create new order in stall.
        /// </summary>
        /// <returns>Order item in stall.</returns>
        // POST: api/v1/order
        #endregion
        //[Authorize(Roles = "Staff")]
        [HttpPost(ApiEndPointConstant.Order.OrderEndpointList)]
        public async Task<ActionResult> PostOrderList([FromBody] OrderRequestList orderData)
        {
            if (orderData.DiscountId == Guid.Empty)
            {
                orderData.DiscountId = null;
            }
            var order = await _service.CreateOrderList(orderData);
            var result = JsonConvert.SerializeObject(order, Formatting.Indented);
            return Ok(result);

        }

        #region SearchOrders
        /// <summary>
        /// Search orders by customer ID and start date.
        /// </summary>
        /// <param name="customerId">The customer ID to search for.</param>
        /// <param name="startDate">The start date to search for.</param>
        /// <returns>List of orders that match the search criteria.</returns>
        // GET: api/Order/Search
        #endregion
        [Authorize(Roles = "Staff, Manager")]
        [HttpGet(ApiEndPointConstant.Order.SearchOrderEndpoint)]
        [ProducesResponseType(typeof(IEnumerable<OrderResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchOrders([FromQuery] Guid? customerId, [FromQuery] DateTime? startDate/*, [FromQuery] DateTime? endDate*/)
        {
            var orders = await _service.SearchOrders(customerId, startDate/*, endDate*/);
            return Ok(orders);
        }

        #region GetOrderByIdAsync
        /// <summary>
        /// Get order by id .
        /// </summary>
        /// <returns>Order item in .</returns>
        // GET: api/v1/Order
        #endregion
        [HttpGet(ApiEndPointConstant.Order.OrderByIdEndpoint)]
        [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult> GetOrderByIdAsync(Guid id)
        {

            var result = await _service.GetOrderByIdAsync(id);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        #region PostCheckProduct
        /// <summary>
        /// Check product in list group promotion
        /// </summary>
        /// <returns>True or false.</returns>
        // POST: api/v1/order/check
        #endregion
        [HttpPost(ApiEndPointConstant.Order.OrderCheckPromotionEndpoint)]
        public async Task<bool> PostCheckProduct([FromBody] OrderRequest orderData)
        {
            List<OrderDetailRequest> productList = orderData.Details;
            var isCheck = await _service.CheckPromotion(orderData.PromotionId.Value, productList);
            return isCheck;
        }

        #region StaticOrder
        /// <summary>
        /// Statics order in day
        /// </summary>
        /// <returns>Amount order in day</returns>
        // POST: api/v1/order/static
        #endregion
        [HttpGet(ApiEndPointConstant.Order.OrderStatic)]
        public async Task<ActionResult> StaticOrderByDay([FromQuery] DateTime time)
        {
            var result = await _service.GetTotalOrdersByDay(time);
            return Ok(result);
        }

        #region StaticOrderByMonth
        /// <summary>
        /// Statics order in month and year.
        /// </summary>
        /// <returns>Amount order in month.</returns>
        // POST: api/v1/order/static/month
        #endregion
        [HttpGet(ApiEndPointConstant.Order.OrderStaticMonth)]
        public async Task<ActionResult> StaticOrderByMonth([FromQuery] int month, int year)
        {
            var result = await _service.GetTotalOrdersByMonth(month, year);
            return Ok(result);
        }

        #region StaticOrderByYear
        /// <summary>
        /// Statics order in year
        /// </summary>
        /// <returns>Amount order in year <returns>
        // POST: api/v1/order/static/year
        #endregion
        [HttpGet(ApiEndPointConstant.Order.OrderStaticYear)]
        public async Task<ActionResult> StaticOrderByYear([FromQuery] int year)
        {
            var result = await _service.GetTotalOrdersByYear(year);
            return Ok(result);
        }

        #region GetAllOrders
        /// <summary>
        /// Get all order 
        /// </summary>
        /// <returns>Get all orders <returns>
        // POST: api/v1/order/order/GetListOrders
        #endregion
        [HttpGet(ApiEndPointConstant.Order.AllOrdersEndpoint)]
        [ProducesResponseType(typeof(IEnumerable<OrderResponse>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _service.GetAllOrders();
            return Ok(orders);
        }

        #region GetListOrderByCustomerPhone
        /// <summary>
        /// List order search by phone customer 
        /// </summary>
        /// <returns>List order <returns>
        // POST: api/v1/order/order/customer
        #endregion
        [HttpGet(ApiEndPointConstant.Order.OrderListCusomerPhone)]
        public async Task<ActionResult> GetListOrderByCustomerPhone(string phone)
        {
            var result = await _service.GetListOrderByCustomerPhone(phone);
            var item = JsonConvert.SerializeObject(result, Formatting.Indented);
            return Ok(item);
        }

        #region GetListOrderDetailById
        /// <summary>
        /// List order detail search by id customer 
        /// </summary>
        /// <returns>List order detail<returns>
        // POST: api/v1/order/order/detail
        #endregion
        [HttpGet(ApiEndPointConstant.Order.OrderListDetail)]
        public async Task<ActionResult> GetListOrderDetailById(Guid id)
        {
            var result = await _service.GetListOrderDetailByIdAsync(id);
            var item = JsonConvert.SerializeObject(result, Formatting.Indented);
            return Ok(item);
        }
    }
}
