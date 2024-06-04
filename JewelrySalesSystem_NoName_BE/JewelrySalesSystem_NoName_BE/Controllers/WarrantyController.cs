﻿using JewelrySalesSystem_NoName_BE.Extenstion;
using JSS_BusinessObjects;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;
using JSS_Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;

namespace JewelrySalesSysmte_NoName_BE.Controllers
{
    [ApiController]
    [Authorize]
    public class WarrantyController : ControllerBase
    {
        private readonly IWarrantyService _service;

        public WarrantyController(IWarrantyService service)
        {
            _service = service;
        }

        #region GetWarranty
        /// <summary>
        /// List of warranty.
        /// </summary>
        /// <returns>List of warranty.</returns>
        // GET: api/warranty
        #endregion
        [Authorize(Roles = "Manager, Staff")]
        [HttpGet(ApiEndPointConstant.Warranty.WarrantyEndpoint)]
        [ProducesResponseType(typeof(WarrantyResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWarrantiesNo(int page, int size)
        {
            var warranty = await _service.GetWarranties(page, size);
            var result = JsonConvert.SerializeObject(warranty, Formatting.Indented);
            return Ok(result);
        }

        #region GetWarrantytDetail
        /// <summary>
        /// Get detail of warranty.
        /// </summary>
        /// <returns>IWarranty detail</returns>
        // GET: api/warranty
        #endregion
        [Authorize(Roles = "Manager, Staff")]
        [HttpGet(ApiEndPointConstant.Warranty.WarrantyByIdEndpoint)]
        public async Task<ActionResult> GetWarrantytDetail(Guid id)
        {
            var warranty = await _service.GetWarrantyDetail(id);
            var result = JsonConvert.SerializeObject(warranty, Formatting.Indented);
            return Ok(result);
        }

        #region CreateWarranty
        /// <summary>
        /// Create new warranty.
        /// </summary>
        /// <returns>Warraty item</returns>
        // POST: api/v1/warranty
        #endregion
        [Authorize(Roles = "Manager, Staff")]
        [HttpPost(ApiEndPointConstant.Warranty.WarrantyEndpoint)]
        public async Task<ActionResult> CreateWarranty(WarrantyRequest warranty)
        {
            var newWarranty = await _service.CreateWarranty(warranty);
            return Ok(newWarranty);
        }
    }
}
