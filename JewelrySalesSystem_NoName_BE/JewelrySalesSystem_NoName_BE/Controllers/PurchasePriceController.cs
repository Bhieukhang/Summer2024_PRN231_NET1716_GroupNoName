using JewelrySalesSystem_NoName_BE.Extenstion;
using JSS_BusinessObjects.Payload.Response;
using JSS_Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JewelrySalesSystem_NoName_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class PurchasePriceController : ControllerBase
    {
        private readonly IPurchasePriceService _IPurchasePriceService;
        public PurchasePriceController(IPurchasePriceService IPurchasePriceService)
        {
            _IPurchasePriceService = IPurchasePriceService;
        }
        #region GetPurchasePrice
        /// <summary>
        /// Get all PurchasePrice.
        /// </summary>
        /// <returns>List of PurchasePrice.</returns>
        // GET: api/PurchasePrice
        #endregion
   //     [Authorize(Roles = "Manager, Admin, Staff")]
        [HttpGet(ApiEndPointConstant.PurchasePrice.PurchasePriceEndpoint)]
        [ProducesResponseType(typeof(PurchasePriceResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetListPurchasePriceAsync(int page, int size)
        {
            var list = await _IPurchasePriceService.GetListPurchasePriceAsync(page, size);
            var PurchasePrice = JsonConvert.SerializeObject(list, Formatting.Indented);
            return Ok(PurchasePrice);
        }
    }
}
