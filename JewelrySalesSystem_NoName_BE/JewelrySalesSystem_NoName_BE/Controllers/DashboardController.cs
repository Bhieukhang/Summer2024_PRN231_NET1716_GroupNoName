using JewelrySalesSystem_NoName_BE.Extenstion;
using JSS_BusinessObjects.Payload.Request;
using JSS_Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace JewelrySalesSystem_NoName_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        #region GetAllDashboards
        /// <summary>
        /// Get all Dashboards.
        /// </summary>
        /// <returns>List of Dashboards.</returns>
        /// GET : api/Dashboard
        #endregion
        [Authorize(Roles = "Manager, Staff")]
        [HttpGet(ApiEndPointConstant.Dashboard.DashboardEndpoint)]
        public async Task<ActionResult<DashboardRequest>> GetDashboardsAsync(int year)
        {
            var Dashboards = await _dashboardService.GetDashboardsAsync(year);
            return Ok(Dashboards);
        }

        #region GetDashboardAccount
        /// <summary>
        /// Get static account in pie dashboards.
        /// </summary>
        /// <returns>Total account by role.</returns>
        /// GET : api/Dashboard
        #endregion
        //[Authorize(Roles = "Admin")]
        [HttpGet(ApiEndPointConstant.Dashboard.AccountDashboardEndpoint)]
        public async Task<ActionResult<AccountDashboard>> GetDashboardAccount()
        {
            var data = await _dashboardService.GetDashboardAccount();
            var result = JsonConvert.SerializeObject(data, Formatting.Indented);
            return Ok(result);
        }

    }
}
