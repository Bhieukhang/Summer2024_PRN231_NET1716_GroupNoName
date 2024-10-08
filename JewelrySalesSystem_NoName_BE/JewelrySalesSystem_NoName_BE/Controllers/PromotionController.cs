﻿using JewelrySalesSystem_NoName_BE.Extenstion;
using JSS_BusinessObjects.Helper;
using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Request;
using JSS_Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JewelrySalesSystem_NoName_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionService _promotionService;
        private readonly IProductService _productService;

        public PromotionController(IPromotionService PromotionService, IProductService productService)
        {
            _promotionService = PromotionService;
            _productService = productService;
        }

        #region GetAllPromotions
        /// <summary>
        /// Get all promotions.
        /// </summary>
        /// <returns>List of promotions.</returns>
        /// GET : api/Promotion
        #endregion
        //[Authorize(Roles = "Manager, Staff")]
        [HttpGet(ApiEndPointConstant.Promotion.PromotionEndpoint)]
        public async Task<ActionResult<IEnumerable<Promotion>>> GetAllPromotionsAsync(string? search)
        {
            var promotions = await _promotionService.GetAllPromotionsAsync(search ?? "");
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
        //[Authorize(Roles = "Manager, Staff")]
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

        [HttpGet(ApiEndPointConstant.Promotion.PromotionGroupEndpoint)]
        public async Task<IActionResult> GetPromotionGroups(Guid id)
        {
            var groups = await _promotionService.GetPromotionGroups(id);
            if (groups == null)
            {
                return NotFound();
            }
            return Ok(groups);
        }

        #region UpdatePromotion
        /// <summary>
        /// Update a Promotion by its ID.
        /// </summary>
        /// <param name="id">The ID of the Promotion to update.</param>
        /// <param name="Promotion">The updated Promotion data.</param>
        /// <returns>The updated Promotion.</returns>
        /// PUT : api/Promotion
        #endregion
        [HttpPut((ApiEndPointConstant.Promotion.PromotionEndpoint))]
        public async Task<IActionResult> UpdatePromotion([FromBody] PromotionRequest promotionRequest)
        {

            var existingPromotion = await _promotionService.GetPromotionByIdAsync(promotionRequest.Id ?? new Guid());
            if (existingPromotion == null)
            {
                return Ok(new ApiResponse
                {
                    Message = "Promotion not found",
                    Success = false
                });
            }

            var promotion = new Promotion
            {
                Id = existingPromotion.Id,
                PromotionName = promotionRequest.PromotionName,
                Type = promotionRequest.Type,
                Description = promotionRequest.Description,
                ProductQuantity = promotionRequest.ProductQuantity,
                Percentage = promotionRequest.Percentage,
                InsDate = existingPromotion.StartDate,
                UpsDate = DateTime.Now,
                StartDate = promotionRequest.StartDate,
                EndDate = promotionRequest.EndDate,
            };

            var updatedPromotion = await _promotionService.UpdatePromotionAsync(promotion);
            if (updatedPromotion == null)
            {
                return Ok(new ApiResponse
                {
                    Message = "Update fail",
                    Success = false
                });
            }

            await _productService.DeleteProductConditionGroup(promotion.Id);
            foreach (var item in promotionRequest.ProductIds ?? new())
            {
                await _productService.AddProductConditionGroup(item, promotion.Id);
            }

            return Ok(new ApiResponse
            {
                Message = "Update success",
                Success = true
            });
        }

        [HttpPost(ApiEndPointConstant.Promotion.PromotionEndpoint)]
        public async Task<IActionResult> CreatePromotion([FromBody] PromotionRequest promotionRequest)
        {
            if (promotionRequest.StartDate > promotionRequest.EndDate)
            {
                return Ok(new ApiResponse
                {
                    Message = "Ngày kết thúc phải sau ngày bắt đầu.",
                    Success = false
                });
            }
            var promotion = new Promotion
            {
                Id = Guid.NewGuid(),
                PromotionName = promotionRequest.PromotionName,
                Type = promotionRequest.Type,
                Description = promotionRequest.Description,
                ProductQuantity = promotionRequest.ProductQuantity,
                Percentage = promotionRequest.Percentage,
                InsDate = DateTime.Now,
                UpsDate = DateTime.Now,
                StartDate = promotionRequest.StartDate,
                EndDate = promotionRequest.EndDate,
            };

            var createdPromotion = await _promotionService.CreatePromotionAsync(promotion);
            if (createdPromotion == null)
            {
                return Ok(new ApiResponse
                {
                    Message = "Create fail",
                    Success = false
                });
            }

            foreach (var item in promotionRequest.ProductIds ?? new())
            {
                await _productService.AddProductConditionGroup(item, createdPromotion.Id);
            }

            return Ok(new ApiResponse
            {
                Message = "Create success",
                Success = true
            });
        }

    }
}   