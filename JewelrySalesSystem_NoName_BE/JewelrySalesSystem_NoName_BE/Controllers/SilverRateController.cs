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
    public class SilverRateController : ControllerBase
    {
        private readonly ISilverRateService _silverRateService;

        public SilverRateController(ISilverRateService silverRateService)
        {
            _silverRateService = silverRateService;
        }

        #region GetAllSilverRates
        /// <summary>
        /// Get all silver rates.
        /// </summary>
        /// <returns>List of silver rates.</returns>
        /// GET : api/SilverRate
        #endregion
        //[Authorize(Roles = "Manager, Staff")]
        [HttpGet(ApiEndPointConstant.SilverRate.SilverRateEndpoint)]
        public async Task<ActionResult<IEnumerable<SilverRate>>> GetAllSilverRatesAsync(int page, int size)
        {
            var list = await _silverRateService.GetAllSilverRatesAsync(page, size);
            var silverrates = JsonConvert.SerializeObject(list, Formatting.Indented);
            return Ok(silverrates);
        }

        #region UpdateSilverRate
        /// <summary>
        /// Update a silver rate.
        /// </summary>
        /// <param name="newRate">The updated silver rate data.</param>
        /// <returns>The updated silver rate.</returns>
        /// PUT : api/SilverRate
        #endregion
        [HttpPost((ApiEndPointConstant.SilverRate.SilverRateEndpoint))]
        public async Task<ActionResult<SilverRate>> UpdateSilverRate([FromBody] double newRate)
        {
            var updatedRate = await _silverRateService.UpdateSilverRateAsync(newRate);
            return Ok(updatedRate);
        }
    }
}
