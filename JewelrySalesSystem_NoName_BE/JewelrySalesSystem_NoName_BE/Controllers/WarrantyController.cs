using JewelrySalesSystem_NoName_BE.Extenstion;
using JSS_BusinessObjects;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;
using JSS_Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace JewelrySalesSysmte_NoName_BE.Controllers
{
    [ApiController]
    public class WarrantyController : ControllerBase
    {
        private readonly IWarrantyService _service;

        public WarrantyController(IWarrantyService service)
        {
            _service = service;
        }


        //[HttpGet(ApiEndPointConstant.Warranty.WarrantyEndpoint)]
        //[ProducesResponseType(typeof(WarrantyResponse), StatusCodes.Status200OK)]
        //public async Task<IActionResult> GetWarranties(int page, int size)
        //{
        //    var warranty = await _service.GetWarranties(page, size);
        //    return Ok(warranty);
        //}

        #region GetWarranty
        /// <summary>
        /// List of warranty.
        /// </summary>
        /// <returns>List of warranty.</returns>
        // GET: api/warranty
        #endregion
        [HttpGet(ApiEndPointConstant.Warranty.WarrantyEndpoint)]
        [ProducesResponseType(typeof(WarrantyResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetWarrantiesNo()
        {
            var warranty = await _service.GetWarrantiesNo(1, 10);
            return Ok(warranty);
        }

        #region GetWarrantytDetail
        /// <summary>
        /// Get detail of warranty.
        /// </summary>
        /// <returns>IWarranty detail</returns>
        // GET: api/warranty
        #endregion
        [HttpGet(ApiEndPointConstant.Warranty.WarrantyByIdEndpoint)]
        public async Task<ActionResult> GetWarrantytDetail(Guid id)
        {
            var warranty = await _service.GetWarrantyDetail(id);
            return Ok(warranty);
        }

        #region CreateWarranty
        /// <summary>
        /// Create new warranty.
        /// </summary>
        /// <returns>Warraty item</returns>
        // GET: api/warranty
        #endregion
        [HttpPost(ApiEndPointConstant.Warranty.WarrantyEndpoint)]
        public async Task<ActionResult> CreateWarranty(WarrantyRequest warranty)
        {
            var newWarranty = await _service.CreateWarranty(warranty);
            return Ok(newWarranty);
        }
    }
}
