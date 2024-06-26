using JewelrySalesSystem_NoName_BE.Extenstion;
using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;
using JSS_Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JewelrySalesSystem_NoName_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialService _materialService;

        public MaterialController(IMaterialService materialService)
        {
            _materialService = materialService;
        }

        #region GetAllMaterials
        /// <summary>
        /// Get all materials.
        /// </summary>
        /// <returns>List of materials.</returns>
        /// GET : api/Material
        #endregion
        //[Authorize(Roles = "Manager, Staff")]
        [HttpGet(ApiEndPointConstant.Material.MaterialEndpoint)]
        public async Task<ActionResult<IEnumerable<Material>>> GetAllMaterialsAsync()
        {
            var materials = await _materialService.GetAllMaterialsAsync();
            return Ok(materials);
        }


        #region GetMaterialById
        /// <summary>
        /// Get a material by its ID.
        /// </summary>
        /// <param name="id">The ID of the material to retrieve.</param>
        /// <returns>The material with the specified ID.</returns>
        /// GET : api/Material
        #endregion
        //[Authorize(Roles = "Manager, Staff")]
        [HttpGet(ApiEndPointConstant.Material.MaterialByIdEndpoint)]
        public async Task<ActionResult<Material>> GetMaterialByIdAsync(Guid id)
        {
            var material = await _materialService.GetMaterialByIdAsync(id);
            if (material == null)
            {
                return NotFound();
            }
            return Ok(material);
        }


        #region CreateMaterial
        /// <summary>
        /// Create a new material.
        /// </summary>
        /// <param name="material">The material to create.</param>
        /// <returns>The created material.</returns>
        /// POST : api/Material
        #endregion
        //[Authorize(Roles = "Manager")]
        [HttpPost(ApiEndPointConstant.Material.MaterialEndpoint)]
        public async Task<ActionResult<MaterialResponse>> CreateMaterialAsync(MaterialRequest material)
        {
            var createdMaterial = await _materialService.CreateMaterialAsync(material);
            if (createdMaterial == null)
            {
                return BadRequest();
            }
            return createdMaterial;
        }


        #region UpdateMaterial
        /// <summary>
        /// Update a material by its ID.
        /// </summary>
        /// <param name="id">The ID of the material to update.</param>
        /// <param name="material">The updated material data.</param>
        /// <returns>The updated material.</returns>
        /// POST : api/Material
        #endregion
        //[Authorize(Roles = "Manager")]
        [HttpPut(ApiEndPointConstant.Material.MaterialByIdEndpoint)]
        public async Task<ActionResult<Material>> UpdateMaterialAsync(Guid id, Material material)
        {
            var updatedMaterial = await _materialService.UpdateMaterialAsync(id, material);
            if (updatedMaterial == null)
            {
                return NotFound();
            }
            return Ok(updatedMaterial);
        }


        #region DeleteMaterial
        /// <summary>
        /// Delete a material by its ID.
        /// </summary>
        /// <param name="id">The ID of the material to delete.</param>
        /// <returns>A response indicating the result of the delete operation.</returns>
        /// POST : api/Material
        #endregion
        [Authorize(Roles = "Manager")]
        [HttpDelete(ApiEndPointConstant.Material.MaterialByIdEndpoint)]
        public async Task<IActionResult> DeleteMaterialAsync(Guid id)
        {
            var result = await _materialService.DeleteMaterialAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
