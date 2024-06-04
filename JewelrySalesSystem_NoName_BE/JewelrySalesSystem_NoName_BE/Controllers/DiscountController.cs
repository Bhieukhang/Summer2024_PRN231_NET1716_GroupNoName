using JewelrySalesSystem_NoName_BE.Extenstion;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;
using JSS_Services.Interface;
using Microsoft.AspNetCore.Http;
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
    }
}
