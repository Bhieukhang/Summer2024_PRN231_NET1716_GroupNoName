using JewelrySalesSystem_NoName_BE.Extenstion;
using JSS_BusinessObjects.Models;
using JSS_Services.Implement;
using JSS_Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JewelrySalesSystem_NoName_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoldRateController : ControllerBase
    {
        private readonly IGoldRateService _goldRateService;

        public GoldRateController(IGoldRateService goldRateService)
        {
            _goldRateService = goldRateService;
        }

        //#region GetLatestGoldRate
        ///// <summary>
        ///// Get Latest Gold Rate.
        ///// </summary>
        ///// <returns>List of Gold Rate.</returns>
        ///// GET : api/GoldRate
        //#endregion
        //[HttpGet((ApiEndPointConstant.GoldRate.GoldRateEndpoint))]
        //public async Task<ActionResult<GoldRate>> GetLatestGoldRate()
        //{
        //    var rate = await _goldRateService.GetLatestGoldRateAsync();
        //    if (rate == null)
        //    {
        //        return NotFound("No gold rate found.");
        //    }

        //    return Ok(rate);
        //}

        #region GetAllGoldRates
        /// <summary>
        /// Get all gold rates.
        /// </summary>
        /// <returns>List of gold rates.</returns>
        /// GET : api/GoldRate
        #endregion
        //[Authorize(Roles = "Manager, Staff")]
        [HttpGet(ApiEndPointConstant.GoldRate.GoldRateEndpoint)]
        public async Task<ActionResult<IEnumerable<GoldRate>>> GetAllGoldRatesAsync(int page, int size)
        {
            var list = await _goldRateService.GetAllGoldRatesAsync(page, size);
            var goldrates = JsonConvert.SerializeObject(list, Formatting.Indented);
            return Ok(goldrates);
        }

        #region UpdateGoldRate
        /// <summary>
        /// Update a gold rate.
        /// </summary>
        /// <param name="newRate">The updated gold rate data.</param>
        /// <returns>The updated gold rate.</returns>
        /// PUT : api/GoldRate
        #endregion
        [HttpPost((ApiEndPointConstant.GoldRate.GoldRateEndpoint))]
        public async Task<ActionResult<GoldRate>> UpdateGoldRate([FromBody] double newRate)
        {
            var updatedRate = await _goldRateService.UpdateGoldRateAsync(newRate);
            return Ok(updatedRate);
        }
    }
}
