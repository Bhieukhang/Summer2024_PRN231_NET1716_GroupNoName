using JewelrySalesSystem_NoName_BE.Extenstion;
using JSS_BusinessObjects.Payload.Response;
using JSS_BusinessObjects;
using JSS_Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using JSS_BusinessObjects.Models;

namespace JewelrySalesSystem_NoName_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpGet(ApiEndPointConstant.Stall.StallEndpoint)]
        public async Task<ActionResult<IEnumerable<Stall>>> GetAllStallsAsync()
        {
            var stalls = await _stallService.GetAllStallsAsync();
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
