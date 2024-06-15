using JewelrySalesSystem_NoName_BE.Extenstion;
using JSS_BusinessObjects.Models;
using JSS_Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JewelrySalesSystem_NoName_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class DiamondController : ControllerBase
    {
        private readonly IDiamondService _diamondService;

        public DiamondController(IDiamondService diamondService)
        {
            _diamondService = diamondService;
        }

        #region GetAllDiamonds
        /// <summary>
        /// Get all diamonds.
        /// </summary>
        /// <returns>List of diamonds.</returns>
        /// GET : api/Diamond
        #endregion
        [HttpGet(ApiEndPointConstant.Diamond.DiamondEndpoint)]
        public async Task<ActionResult<IEnumerable<Diamond>>> GetAllDiamondsAsync()
        {
            var diamonds = await _diamondService.GetAllDiamondsAsync();
            return Ok(diamonds);
        }

        #region GetDiamondById
        /// <summary>
        /// Get a diamond by its ID.
        /// </summary>
        /// <param name="id">The ID of the diamond to retrieve.</param>
        /// <returns>The diamond with the specified ID.</returns>
        /// GET : api/Diamond/{id}
        #endregion
        [HttpGet(ApiEndPointConstant.Diamond.DiamondByDiamondIdEndpoint)]
        public async Task<ActionResult<Diamond>> GetDiamondByIdAsync(Guid id)
        {
            var diamond = await _diamondService.GetDiamondByIdAsync(id);
            if (diamond == null)
            {
                return NotFound();
            }
            return Ok(diamond);
        }

        #region CreateDiamond
        /// <summary>
        /// Create a new diamond.
        /// </summary>
        /// <param name="diamond">The diamond to create.</param>
        /// <returns>The created diamond.</returns>
        /// POST : api/Diamond
        #endregion
        [HttpPost(ApiEndPointConstant.Diamond.DiamondEndpoint)]
        public async Task<IActionResult> CreateDiamond([FromBody] Diamond diamond, [FromForm] IFormFile image)
        {
            if (image == null || image.Length == 0)
                return BadRequest("No image uploaded.");

            var stream = image.OpenReadStream();
            var createdDiamond = await _diamondService.CreateDiamondAsync(diamond, stream, image.FileName);
            if (createdDiamond == null)
            {
                return StatusCode(500, "An error occurred while creating the diamond.");
            }

            return Ok(createdDiamond);
        }

        #region UpdateDiamond
        /// <summary>
        /// Update a diamond by its ID.
        /// </summary>
        /// <param name="id">The ID of the diamond to update.</param>
        /// <param name="diamond">The updated diamond data.</param>
        /// <returns>The updated diamond.</returns>
        /// PUT : api/Diamond/{id}
        #endregion
        [HttpPut((ApiEndPointConstant.Diamond.DiamondByDiamondIdEndpoint))]
        public async Task<IActionResult> UpdateDiamond(Guid id, [FromBody] Diamond diamond, [FromForm] IFormFile image)
        {
            var stream = image != null ? image.OpenReadStream() : null;
            var updatedDiamond = await _diamondService.UpdateDiamondAsync(id, diamond, stream, image?.FileName);
            if (updatedDiamond == null)
            {
                return NotFound();
            }

            return Ok(updatedDiamond);
        }

        #region DeleteDiamond
        /// <summary>
        /// Delete a diamond by its ID.
        /// </summary>
        /// <param name="id">The ID of the diamond to delete.</param>
        /// <returns>A success message.</returns>
        /// DELETE : api/Diamond/{id}
        #endregion
        [HttpDelete(ApiEndPointConstant.Diamond.DiamondEndpoint)]
        public async Task<IActionResult> DeleteDiamond(Guid id)
        {
            var deleted = await _diamondService.DeleteDiamondAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return Ok("Diamond deleted successfully.");
        }
    }
}
