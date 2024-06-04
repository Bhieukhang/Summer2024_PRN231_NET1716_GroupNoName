using JewelrySalesSystem_NoName_BE.Extenstion;
using JSS_BusinessObjects.Models;
using JSS_Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace JewelrySalesSystem_NoName_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _promotionService;

        public PromotionController(IPromotionService PromotionService)
        {
            _promotionService = PromotionService;
        }

        #region GetAllPromotions
        /// <summary>
        /// Get all promotions.
        /// </summary>
        /// <returns>List of promotions.</returns>
        /// GET : api/Promotion
        #endregion
        [HttpGet(ApiEndPointConstant.Promotion.PromotionEndpoint)]
        public async Task<ActionResult<IEnumerable<Promotion>>> GetAllPromotionsAsync()
        {
            var promotions = await _promotionService.GetAllPromotionsAsync();
            return Ok(promotions);
        }

        #region GetPromotionById
        /// <summary>
        /// Get a promotion by its ID.
        /// </summary>
        /// <param name="id">The ID of the promotion to retrieve.</param>
        /// <returns>The promotion with the specified ID.</returns>
        /// GET : api/Promotion
        #endregion
        [HttpGet(ApiEndPointConstant.Promotion.PromotionByIdEndpoint)]
        public async Task<ActionResult<Promotion>> GetPromotionByIdAsync(Guid id)
        {
            var promotion = await _promotionService.GetPromotionByIdAsync(id);
            if (promotion == null)
            {
                return NotFound();
            }
            return Ok(promotion);
        }

    }
}
