using JewelrySalesSystem_NoName_BE.Extenstion;
using JSS_BusinessObjects.Payload.Request;
using JSS_Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JewelrySalesSystem_NoName_BE.Controllers
{
    [Authorize]
    public class ConditionWarrantyController : ControllerBase
    {
        private readonly IConditionWarrantyServicecs _service;

        public ConditionWarrantyController(IConditionWarrantyServicecs service)
        {
            _service = service;
        }

        #region PostConditionWarranty
        /// <summary>
        /// Create new condition warranty.
        /// </summary>
        /// <returns>Condition warranty item.</returns>
        // POST: api/v1/condition
        #endregion
        [Authorize(Roles = "Staff, Manager")]
        [HttpPost(ApiEndPointConstant.ConditionWarranty.ConditionWarrantyEndpoint)]
        public async Task<ActionResult> PostConditionWarranty([FromBody] ConditionWarrantyRequest conditionData)
        {
            if (conditionData.Condition == null)
            {
                return BadRequest("Vui lòng điền điều kiện bảo hành!!!");
            }
           
            var order = await _service.CreateConditionWarranty(conditionData);
            var result = JsonConvert.SerializeObject(order, Formatting.Indented);
            return Ok(result);

        }

        #region GetListConditionWarranties
        /// <summary>
        /// List condition warranty.
        /// </summary>
        /// <returns>Condition warranty list.</returns>
        // POST: api/v1/condition
        #endregion
        [Authorize(Roles = "Staff, Manager")]
        [HttpGet(ApiEndPointConstant.ConditionWarranty.ConditionWarrantyEndpoint)]
        public async Task<ActionResult> GetListConditionWarranties(int page, int size)
        {
            var order = await _service.GetListConditionWarranties(page, size);
            var result = JsonConvert.SerializeObject(order, Formatting.Indented);
            return Ok(result);

        }

        #region GetConditionWarrantyDetail
        /// <summary>
        /// Detail a condition warranty.
        /// </summary>
        /// <returns>Condition warranty item.</returns>
        // POST: api/v1/condition
        #endregion
        [Authorize(Roles = "Staff, Manager")]
        [HttpGet(ApiEndPointConstant.ConditionWarranty.ConditionWarrantyByIdEndpoint)]
        public async Task<ActionResult> GetConditionWarrantyDetail(Guid id)
        {
            var order = await _service.GetConditionWarranty(id);
            if (order == null) return BadRequest("Điều kiện bảo hành không tồn tại");
            var result = JsonConvert.SerializeObject(order, Formatting.Indented);
            return Ok(result);

        }
    }
}
