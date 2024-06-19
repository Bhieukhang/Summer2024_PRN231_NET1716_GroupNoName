using BusinessObjects.Mo;
using JewelrySalesSystem_NoName_BE.Extenstion;
using JSS_BusinessObjects.Payload.Response;
using JSS_Services.Implement;
using JSS_Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JewelrySalesSystem_NoName_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class ProcessPriceController : ControllerBase
    {
        private readonly IProcessPriceService _IProcessPriceService;
        public ProcessPriceController(IProcessPriceService IProcessPriceService)
        {
            _IProcessPriceService = IProcessPriceService;
        }
        #region GetProcessPrice
        /// <summary>
        /// Get all GetProcessPrice.
        /// </summary>
        /// <returns>List of GetProcessPrice.</returns>
        // GET: api/ProcessPrice
        #endregion
     //   [Authorize(Roles = "Manager, Admin, Staff")]
        [HttpGet(ApiEndPointConstant.ProcessPrice.ProcessPriceEndpoint)]
        [ProducesResponseType(typeof(ProcessPriceResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetListProcessPriceAsync(int page, int size)
        {
            var list = await _IProcessPriceService.GetListProcessPriceAsync(page, size);
            var ProcessPrice = JsonConvert.SerializeObject(list, Formatting.Indented);
            return Ok(ProcessPrice);
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
        [HttpPut(ApiEndPointConstant.ProcessPrice.ProcessPriceByIdEndpoint)]
        public async Task<ActionResult<ProcessPrice>> UpdateProcessPriceAsync(int id, ProcessPrice ProcessPrice)
        {
            var updatedProcessPrice = await _IProcessPriceService.UpdateProcessPriceAsync(id, ProcessPrice);
            if (updatedProcessPrice == null)
            {
                return NotFound();
            }
            return Ok(updatedProcessPrice);
        }
    }
}
