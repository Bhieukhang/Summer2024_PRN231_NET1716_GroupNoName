using JewelrySalesSystem_NoName_BE.Extenstion;
using JSS_BusinessObjects.Models;
using JSS_Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JewelrySalesSystem_NoName_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        #region GetListRole
        /// <summary>
        /// Get all roles.
        /// </summary>
        /// <returns>List of roles.</returns> 
        /// GET : api/Role
        #endregion
        [Authorize(Roles = "Admin, Manager")]
        [HttpGet(ApiEndPointConstant.Role.RoleEndpoint)]
        public async Task<ActionResult<IEnumerable<Role>>> GetListRoleAsync()
        {
            var roles = await _roleService.GetAllRolesAsync();
            return Ok(roles);
        }


        #region GetRoleById
        /// <summary>
        /// Get a Role by its ID.
        /// </summary>
        /// <param name="id">The ID of the Role.</param>
        /// <returns>The Role with the specified ID.</returns>
        /// GET : api/Role
        #endregion
        [Authorize(Roles = "Admin, Manager")]
        [HttpGet(ApiEndPointConstant.Role.RoleByIdEndpoint)]
        public async Task<ActionResult<Role>> GetRoleByIdAsync(Guid id)
        {
            var role = await _roleService.GetRoleByIdAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }
    }
}
