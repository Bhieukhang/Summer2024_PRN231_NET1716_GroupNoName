﻿using JewelrySalesSystem_NoName_BE.Extenstion;
using JSS_BusinessObjects.Helper;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;
using JSS_BusinessObjects.SignalR;
using JSS_Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace JewelrySalesSystem_NoName_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiscountController : ControllerBase
    {
        private readonly IDiscountService _discountService;
        private readonly IHubContext<SignalRServer> _hubContext;

        public DiscountController(IDiscountService discountService, IHubContext<SignalRServer> hubContext)
        {
            _discountService = discountService;
            _hubContext = hubContext;
        }

        #region ConfirmDiscountToManager
        /// <summary>
        /// Confirm discount from manager
        /// </summary>
        /// <returns>Accpet or Denied</returns>
        // POST: api/v1/order/check
        #endregion
        [HttpPost(ApiEndPointConstant.Discount.DiscountConfirmEndpoint)]
        public async Task<DiscountResponse> ConfirmDiscountToManager([FromBody] DiscountRequest confirmData)
        {

            var confirmDiscount = await _discountService.ConfirmDiscountToManager(confirmData);
            await _hubContext.Clients.All.SendAsync("ReceiveDiscountNotification", confirmDiscount);
            return confirmDiscount;
        }


        /// <summary>
        /// Get all discounts
        /// </summary>
        /// <returns></returns>
        [HttpGet(ApiEndPointConstant.Discount.DiscountEndpoint)]
        public async Task<ActionResult> GetDiscounts(string search = "")
        {
            try
            {
                var response = await _discountService.GetAsync(search);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Staff create new discount
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost(ApiEndPointConstant.Discount.DiscountEndpoint)]
        public async Task<ActionResult> CreateDiscount([FromBody] DiscountRequest request)
        {

            try
            {
                var response = await _discountService.CreateDiscountAsync(request);

                if (!response) throw new Exception("Create discount failed.");
                return Ok(new ApiResponse
                {
                    Message = "Create discount successful",
                    Success = true
                });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Manager accept discount
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut(ApiEndPointConstant.Discount.DiscountEndpoint)]
        public async Task<ActionResult> UpdateDiscount(DiscountRequest request)
        {
            try
            {
                var response = await _discountService.AcceptDiscountAsync(request);
                if (!response) throw new Exception("Update discount failed.");
                return Ok(new ApiResponse
                {
                    Message = "Update discount successful",
                    Success = true
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet(ApiEndPointConstant.Discount.DiscountByIdEndpoint)]
        public async Task<ActionResult> FindDiscountById(Guid id)
        {
            try
            {
                var response = await _discountService.FindAsync(id);
                await _hubContext.Clients.All.SendAsync("ReceiveMessage", "Discount data has been updated.");
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Get all discounts accepted
        /// </summary>
        /// <returns></returns>

        [HttpGet(ApiEndPointConstant.Discount.DiscountAccepted)]
        public async Task<ActionResult> GetDiscountAccept(Guid id)
        {
            try
            {
                var result = await _discountService.GetDiscountAccept(id);
                if (result.Id == Guid.Parse("00000000-0000-0000-0000-000000000000") || result.Status == "Pending")
                {
                    if (result.Status == "Pending")
                        return Ok(new { message = "Chưa có phản hồi từ quản lí" });
                }
                var response = JsonConvert.SerializeObject(result, Formatting.Indented);
                await _hubContext.Clients.All.SendAsync("DiscountAccpetNotification", response);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}