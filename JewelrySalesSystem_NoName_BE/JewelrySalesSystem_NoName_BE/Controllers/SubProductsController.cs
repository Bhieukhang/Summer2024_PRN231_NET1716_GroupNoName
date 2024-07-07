using JewelrySalesSystem_NoName_BE.Extenstion;
using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Response;
using JSS_Services.Implement;
using JSS_Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JewelrySalesSystem_NoName_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubProductsController : ControllerBase
    {
        private readonly ISubProductsService _subProService;
        public SubProductsController(ISubProductsService subProducts)
        {
            _subProService = subProducts;
        }

        #region GetAllSubProductslsAsync
        /// <summary>
        /// GetAllSubProductslsAsync.
        /// </summary>
        /// <returns>List of SubProducts.</returns> 
        /// GET : api/SubProducts
        #endregion

        [HttpGet(ApiEndPointConstant.SubProduct.SubProductsEndpoint)]
       // [ProducesResponseType(typeof(SubProductResponse), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SubProducts>>> GetAllSubProductslsAsync()
        {
            var subPro = await _subProService.GetAllSubProductslsAsync();
            return Ok(subPro);
        }


    }
}
