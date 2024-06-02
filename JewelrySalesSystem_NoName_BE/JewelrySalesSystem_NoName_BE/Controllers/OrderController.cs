using JewelrySalesSystem_NoName_BE.Extenstion;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;
using JSS_Services.Implement;
using JSS_Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JewelrySalesSystem_NoName_BE.Controllers
{
    [ApiController]
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
    }
}
