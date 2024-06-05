using JewelrySalesSystem_NoName_BE.Extenstion;
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
        public async Task<ActionResult<DashboardRequest>> GetDashboardsAsync()
        {
            var Dashboards = await _dashboardService.GetDashboardsAsync();
            return Ok(Dashboards);
        }

        

    }
}
