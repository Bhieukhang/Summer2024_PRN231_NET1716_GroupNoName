using JewelrySalesSystem_NoName_BE.Extenstion;
using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Response;
using JSS_Services.Implement;
using JSS_Services.Interface;
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


        #region UpdatePurchasePrice
        /// <summary>
        /// Update a PurchasePrice by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to update.</param>
        /// <param name="PurchasePrice">The updated category data.</param>
        /// <returns>The updated PurchasePrice.</returns>
        /// POST : api/PurchasePrice
        #endregion
      //  [Authorize(Roles = "Manager")]
        [HttpPut(ApiEndPointConstant.PurchasePrice.PurchasePriceByIdEndpoint)]
        public async Task<ActionResult<PurchasePrice>> UpdatePurchasePriceAsync(int id, PurchasePrice PurchasePrice)
        {
            var updatedPurchasePrice = await _IPurchasePriceService.UpdatePurchasePriceAsync(id, PurchasePrice);
            if (updatedPurchasePrice == null)
            {
                return NotFound();
            }
            return Ok(updatedPurchasePrice);
        }
        #region GetPurchasePriceById
        /// <summary>
        /// Get a PurchasePrice by its ID.
        /// </summary>
        /// <param name="id">The ID of the ProcessPrice to retrieve.</param>
        /// <returns>The stall with the specified ID.</returns>
        // GET: api/PurchasePrice/id
        #endregion
        //   [Authorize(Roles = "Manager, Admin")]
        //  [HttpGet("{id}")]
        [HttpGet(ApiEndPointConstant.PurchasePrice.PurchasePriceByIdEndpoint)]
        public async Task<ActionResult<PurchasePrice>> GetPurchasePriceByIdAsync(int id)
        {
            var PurchasePrice = await _IPurchasePriceService.GetPurchasePriceByIdAsync(id);

            if (PurchasePrice == null)
            {
                return NotFound();
            }

            return Ok(PurchasePrice);
        }
    }
}
