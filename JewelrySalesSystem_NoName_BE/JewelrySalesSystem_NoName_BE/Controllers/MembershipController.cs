using JewelrySalesSystem_NoName_BE.Extenstion;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;
using JSS_Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace JewelrySalesSystem_NoName_BE.Controllers
{
    [Authorize]
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
        [Authorize(Roles = "Manager, Admin")]
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
        [Authorize(Roles = "Manager, Admin")]
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
        [Authorize(Roles = "Manager, Admin")]
        [HttpGet(ApiEndPointConstant.Membership.MembershipByName)]
        [ProducesResponseType(typeof(MembershipResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMemberByName(string name)
        {
            var list = await _service.GetMembershipByName(name);
            return Ok(list);
        }

        #region GetProfileMembershipById
        /// <summary>
        /// Get membership by UserId
        /// </summary>
        /// <returns>Membership by name.</returns>
        // GET: api/v1/membership/{name}
        #endregion
        [Authorize(Roles = "Manager, Admin")]
        [HttpGet(ApiEndPointConstant.Membership.MembershipByUserIdEndpoint)]
        [ProducesResponseType(typeof(ProfileResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetProfileMembershipByUserId([FromQuery]Guid id)
        {
            var member = await _service.GetProfileMembershipById(id);
            var result = JsonConvert.SerializeObject(member, Formatting.Indented);
            return Ok(result);
        }

        #region UpdateUserMoneyByUserId
        /// <summary>
        /// Update UserMoney By UserId
        /// </summary>
        /// <returns>Level user money for membership.</returns>
        // GET: api/v1/membership/userMoney
        #endregion
        //[Authorize(Roles = "Manager, Admin")]
        //[HttpPatch(ApiEndPointConstant.Membership.MembershipUserMoney)]
        //public async Task<IActionResult> UpdateUserMoneyByUserId([FromQuery]Guid userId, double userMoney)
        //{
        //    var membership = await _service.UpdateUserMoney(userId, userMoney);
        //    var result = JsonConvert.SerializeObject(membership, Formatting.Indented);
        //    return Ok(result);
        //}

        #region GetTotalMembership
        /// <summary>
        /// Get total membership
        /// </summary>
        /// <returns>Total membership.</returns>
        // GET: api/v1/membership/userMoney
        #endregion
        [Authorize(Roles = "Manager, Admin")]
        [HttpGet(ApiEndPointConstant.Membership.MembershipTotal)]
        public async Task<IActionResult> GetTotalMembership()
        {
            var membership = await _service.GetTotalMembershipCountAsync();
            var result = JsonConvert.SerializeObject(membership, Formatting.Indented);
            return Ok(result);
        }

        #region GetActiveMembership
        /// <summary>
        /// Get Active Membership
        /// </summary>
        /// <returns>Get active membership.</returns>
        // GET: api/v1/membership/userMoney
        #endregion
        [Authorize(Roles = "Manager, Admin")]
        [HttpGet(ApiEndPointConstant.Membership.MembershipActive)]
        public async Task<IActionResult> GetActiveMembership()
        {
            var membership = await _service.GetActiveMembershipCountAsync();
            var result = JsonConvert.SerializeObject(membership, Formatting.Indented);
            return Ok(result);
        }

        #region GetUnMembership
        /// <summary>
        /// Get un active Membership
        /// </summary>
        /// <returns>Get  inactive membership.</returns>
        // GET: api/v1/membership/userMoney
        #endregion
        [Authorize(Roles = "Manager, Admin")]
        [HttpGet(ApiEndPointConstant.Membership.MembershipUnActive)]
        public async Task<IActionResult> GetUnMembership()
        {
            var membership = await _service.GetUnavailableMembership();
            var result = JsonConvert.SerializeObject(membership, Formatting.Indented);
            return Ok(result);
        }

        #region CreateMembership
        /// <summary>
        /// Create a membership to join system.
        /// </summary>
        /// <returns>Membership information.</returns>
        // GET: api/v1/membership
        #endregion
        [HttpPost(ApiEndPointConstant.Membership.MembershipEndpoint)]
        public async Task<IActionResult> CreateMembership([FromBody] AccountMembership data)
        {
            var membership = await _service.CreateMembership(data.Phone, data.FullName);
            var result = JsonConvert.SerializeObject(membership, Formatting.Indented);
            return Ok(result);
        }
    }
}
