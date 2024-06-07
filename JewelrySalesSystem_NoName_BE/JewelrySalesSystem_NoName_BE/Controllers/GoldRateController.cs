using JewelrySalesSystem_NoName_BE.Extenstion;
using JSS_BusinessObjects.Models;
using JSS_Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet("latest")]
        public async Task<ActionResult<GoldRate>> GetLatestGoldRate()
        {
            var rate = await _goldRateService.GetLatestGoldRateAsync();
            if (rate == null)
            {
                return NotFound("No gold rate found.");
            }

            return Ok(rate);
        }

        #region UpdateGoldRate
        /// <summary>
        /// Update a gold rate by its ID.
        /// </summary>
        /// <param name="id">The ID of the gold rate to update.</param>
        /// <param name="newRate">The updated gold rate data.</param>
        /// <returns>The updated gold rate.</returns>
        /// PUT : api/GoldRate/id
        [HttpPost((ApiEndPointConstant.GoldRate.GoldRateByIdEndpoint))]
        public async Task<ActionResult<GoldRate>> UpdateGoldRate([FromBody] double newRate)
        {
            var updatedRate = await _goldRateService.UpdateGoldRateAsync(newRate);
            return Ok(updatedRate);
        }
    }
}
