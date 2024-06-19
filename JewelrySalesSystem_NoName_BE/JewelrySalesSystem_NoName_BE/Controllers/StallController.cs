using JewelrySalesSystem_NoName_BE.Extenstion;
using JSS_BusinessObjects.Payload.Response;
using JSS_BusinessObjects;
using JSS_Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JSS_BusinessObjects.Models;
using JSS_Services.Implement;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace JewelrySalesSystem_NoName_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StallController : ControllerBase
    {
        private readonly IStallService _stallService;

        public StallController(IStallService stallService)
        {
            _stallService = stallService;
        }
        #region GetStall
        /// <summary>
        /// Get all stalls.
        /// </summary>
        /// <returns>List of stalls.</returns>
        // GET: api/Stall
        #endregion
        [Authorize(Roles = "Manager, Admin")]
        [HttpGet(ApiEndPointConstant.Stall.StallEndpoint)]
        [ProducesResponseType(typeof(StallResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetListStallAsync(int page, int size)
        {
            var list = await _stallService.GetListStallAsync(page, size);
            var stalls = JsonConvert.SerializeObject(list, Formatting.Indented);
            return Ok(stalls);
        }

        #region GetStallById
        /// <summary>
        /// Get a stall by its ID.
        /// </summary>
        /// <param name="id">The ID of the stall to retrieve.</param>
        /// <returns>The stall with the specified ID.</returns>
        // GET: api/Stall/{id}
        #endregion
        [Authorize(Roles = "Manager, Admin")]
        //  [HttpGet("{id}")]
        [HttpGet(ApiEndPointConstant.Stall.StallByIdEndpoint)]
        public async Task<ActionResult<Stall>> GetStallByIdAsync(Guid id)
        {
            var stall = await _stallService.GetStallByIdAsync(id);

            if (stall == null)
            {
                return NotFound();
            }

            return Ok(stall);
        }
    }
}
