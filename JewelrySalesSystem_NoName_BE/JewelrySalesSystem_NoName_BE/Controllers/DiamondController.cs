using JewelrySalesSystem_NoName_BE.Extenstion;
using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;
using JSS_Services.Implement;
using JSS_Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

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

        #region AutocompleteProducts
        /// <summary>
        /// Autocomplete diamonds based on a query string.
        /// </summary>
        /// <param name="query">The query string to search for diamonds.</param>
        /// <returns>List of diamonds matching the query.</returns>
        /// GET : api/Diamond/autocomplete
        #endregion
        [HttpGet(ApiEndPointConstant.Diamond.DiamondAutocompleteEndpoint)]
        public async Task<ActionResult<IEnumerable<DiamondResponse>>> AutocompleteDiamonds(string query)
        {
            var results = await _diamondService.AutocompleteDiamondsAsync(query);
            return Ok(results);
        }

        #region GetAllDiamonds
        /// <summary>
        /// Get all diamonds.
        /// </summary>
        /// <returns>List of diamonds.</returns>
        /// GET : api/Diamond
        #endregion
        [HttpGet(ApiEndPointConstant.Diamond.DiamondEndpoint)]
        public async Task<ActionResult<IEnumerable<Diamond>>> GetAllDiamondsAsync(int page, int size)
        {
            var list = await _diamondService.GetAllDiamondsAsync(page, size);
            var diamonds = JsonConvert.SerializeObject(list, Formatting.Indented);
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

        #region SearchDiamondByCode
        /// <summary>
        /// Search a diamond by its code.
        /// </summary>
        /// <param name="code">The code of the diamond to search.</param>
        /// <returns>Diamond with code.</returns>
        /// GET : api/Diamond/search
        #endregion
        [HttpGet(ApiEndPointConstant.Diamond.DiamondByCodeEndpoint)]
        public async Task<IActionResult> SearchDiamondByCode(string code)
        {
            var product = await _diamondService.SearchDiamondByCodeAsync(code);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
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
        public async Task<IActionResult> CreateDiamond([FromBody] DiamondRequest diamondRequest)
        {
            if (string.IsNullOrEmpty(diamondRequest.ImageDiamond))
                return BadRequest("No image uploaded.");

            var stream = new MemoryStream(Convert.FromBase64String(diamondRequest.ImageDiamond));

            var diamond = new Diamond
            {
                DiamondName = diamondRequest.DiamondName,
                Code = diamondRequest.Code,
                Carat = diamondRequest.Carat,
                Color = diamondRequest.Color,
                Clarity = diamondRequest.Clarity,
                Cut = diamondRequest.Cut,
                Price = diamondRequest.Price,
                ImageDiamond = diamondRequest.ImageDiamond,
                Quantity = diamondRequest.Quantity,
                Deflag = diamondRequest.Deflag,
                PeriodWarranty = diamondRequest.PeriodWarranty,
            };

            var createdDiamond = await _diamondService.CreateDiamondAsync(diamond, stream, "uploadedFileName");
            if (createdDiamond == null)
            {
                return StatusCode(500, "An error occurred while creating the diamond.");
            }

            return Ok(new DiamondResponse(
                createdDiamond.Id,
                createdDiamond.DiamondName,
                createdDiamond.Code,
                createdDiamond.Carat,
                createdDiamond.Color,
                createdDiamond.Clarity,
                createdDiamond.Cut,
                createdDiamond.Price,
                createdDiamond.ImageDiamond,
                createdDiamond.Quantity,
                createdDiamond.InsDate,
                createdDiamond.PeriodWarranty,
                createdDiamond.Deflag
            ));
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
        public async Task<IActionResult> UpdateDiamond(Guid id, [FromBody] DiamondRequest diamondRequest)
        {
            Stream stream = null;
            if (!string.IsNullOrEmpty(diamondRequest.ImageDiamond))
            {
                stream = new MemoryStream(Convert.FromBase64String(diamondRequest.ImageDiamond));
            }
            var existingDiamond = await _diamondService.GetDiamondByIdAsync(id);
            if (existingDiamond == null)
            {
                return NotFound("Product not found.");
            }

            var diamond = new Diamond
            {
                DiamondName = diamondRequest.DiamondName,
                Code = diamondRequest.Code,
                Carat = diamondRequest.Carat,
                Color = diamondRequest.Color,
                Clarity = diamondRequest.Clarity,
                Cut = diamondRequest.Cut,
                Price = diamondRequest.Price,
                ImageDiamond = diamondRequest.ImageDiamond,
                Quantity = diamondRequest.Quantity,
                Deflag = diamondRequest.Deflag,
                PeriodWarranty = diamondRequest.PeriodWarranty,
            };

            var updatedDiamond = await _diamondService.UpdateDiamondAsync(id, diamond, stream, "uploadedFileName");
            if (updatedDiamond == null)
            {
                return NotFound();
            }

            return Ok(new DiamondResponse(
               updatedDiamond.Id,
               updatedDiamond.DiamondName,
               updatedDiamond.Code,
               updatedDiamond.Carat,
               updatedDiamond.Color,
               updatedDiamond.Clarity,
               updatedDiamond.Cut,
               updatedDiamond.Price,
               updatedDiamond.ImageDiamond,
               updatedDiamond.Quantity,
               updatedDiamond.InsDate,
               updatedDiamond.PeriodWarranty,
               updatedDiamond.Deflag
           ));
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
