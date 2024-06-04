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
    [Authorize]
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
        [Authorize(Roles = "Staff")]
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

    }
}
