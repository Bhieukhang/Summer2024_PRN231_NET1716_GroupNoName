using JewelrySalesSystem_NoName_BE.Extenstion;
using JSS_BusinessObjects.Payload.Response;
using JSS_Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JewelrySalesSystem_NoName_BE.Controllers
{
    public class MembershipController : ControllerBase
    {
        private readonly IMembershipService _service;

        public MembershipController(IMembershipService service)
        {
            _service = service;
        }

        #region GetListMembership
        /// <summary>
        /// List of membership.
        /// </summary>
        /// <returns>List of membership.</returns>
        // GET: api/v1/membership
        #endregion
        [HttpGet(ApiEndPointConstant.Membership.MembershipEndpoint)]
        [ProducesResponseType(typeof(MembershipResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetListMembership(int page, int size)
        {
            var list = await _service.GetListMembership(page, size);
            var result = JsonConvert.SerializeObject(list, Formatting.Indented);
            return Ok(result);
        }

        #region GetListMembershipExpired
        /// <summary>
        /// List of membership expired.
        /// </summary>
        /// <returns>List of membership expired.</returns>
        // GET: api/v1/membership/expired
        #endregion
        [HttpGet(ApiEndPointConstant.Membership.MembershipExpired)]
        [ProducesResponseType(typeof(MembershipResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetListMembershipExpired(int page, int size)
        {
            var list = await _service.GetListMembershipExpired(page, size);
            var result = JsonConvert.SerializeObject(list, Formatting.Indented);
            return Ok(result);
        }

        #region GetMemberByName
        /// <summary>
        /// Get membership by name
        /// </summary>
        /// <returns>Membership by name.</returns>
        // GET: api/v1/membership/{name}
        #endregion
        [HttpGet(ApiEndPointConstant.Membership.MembershipByName)]
        [ProducesResponseType(typeof(MembershipResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMemberByName(string name)
        {
            var list = await _service.GetMembershipByName(name);
            return Ok(list);
        }

        [HttpPatch(ApiEndPointConstant.Membership.MembershipUserMoney)]
        public async Task<IActionResult> UpdateUserMoneyByUserId([FromQuery]Guid userId, double userMoney)
        {
            var membership = await _service.UpdateUserMoney(userId, userMoney);
            var result = JsonConvert.SerializeObject(membership, Formatting.Indented);
            return Ok(result);
        }
    }
}
