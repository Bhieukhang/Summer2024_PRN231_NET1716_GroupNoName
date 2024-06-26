using JewelrySalesSystem_NoName_BE.Extenstion;
using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Response;
using JSS_Services.Implement;
using JSS_Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JewelrySalesSystem_NoName_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;

        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }
        #region GetAllOrderDetailsAsync
        /// <summary>
        /// Get all orderdetalis .
        /// </summary>
        /// <returns>List of OrderDetail.</returns> 
        /// GET : api/OrderDetail
        #endregion

        [HttpGet(ApiEndPointConstant.OrderDetail.OrderDetailEndpoint)]
     //   [ProducesResponseType(typeof(OrderDetailsResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<OrderDetail>>> GetAllOrderDetailsAsync()
        {
            var orderDetail = await _orderDetailService.GetAllOrderDetailsAsync();
            return Ok(orderDetail);
        }

        #region GetOrderDetailById
        /// <summary>
        /// Get a orderDetail by its ID.
        /// </summary>
        /// <param name="id">The ID of the OrderDetail to retrieve.</param>
        /// <returns>The OrderDetail with the specified ID.</returns>
        /// GET : api/OrderDetail
        #endregion
        [HttpGet(ApiEndPointConstant.OrderDetail.OrderDetailByIdEndpoint)]
       // [ProducesResponseType(typeof(OrderDetailsResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<OrderDetail>> GetOrderDetailByIdAsync(Guid id)
        {
            var orderDetail = await _orderDetailService.GetOrderDetailByIdAsync(id);
            if (orderDetail == null)
            {
                return NotFound();
            }
            return Ok(orderDetail);
        }

    }
}
