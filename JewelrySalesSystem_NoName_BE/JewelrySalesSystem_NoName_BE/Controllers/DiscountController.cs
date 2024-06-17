using JewelrySalesSystem_NoName_BE.Extenstion;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;
using JSS_Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace JewelrySalesSystem_NoName_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        #region ConfirmDiscountToManager
        /// <summary>
        /// Confirm discount from manager
        /// </summary>
        /// <returns>Accpet or Denied</returns>
        // POST: api/v1/order/check
        #endregion
        [HttpPost(ApiEndPointConstant.Discount.DiscountConfirmEndpoint)]
        public async Task<DiscountResponse> ConfirmDiscountToManager([FromBody] DiscountRequest confirmData)
        {

            var confirmDiscount = await _discountService.ConfirmDiscountToManager(confirmData);
            return confirmDiscount;
        }

        /// <summary>
        /// Staff create new discount
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(ApiEndPointConstant.Discount.DiscountEndpoint)]
        public async Task<ActionResult> CreateDiscount([FromBody] DiscountRequest request)
        {

            try
            {
                var response = await _discountService.CreateDiscountAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Manager accept discount
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut(ApiEndPointConstant.Discount.DiscountEndpoint)]
        public async Task<ActionResult> AcceptDiscount([FromBody] DiscountRequest request)
        {

            try
            {
                var response = await _discountService.AcceptDiscountAsync(request);
                return Ok(response);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get all discounts
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiEndPointConstant.Discount.DiscountEndpoint)]
        public async Task<ActionResult> GetDiscounts(string search = "")
        {
            try
            {
                var response = await _discountService.GetAsync(search);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
