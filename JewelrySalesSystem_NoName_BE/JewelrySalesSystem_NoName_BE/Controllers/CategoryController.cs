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
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        #region GetAllCategories
        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <returns>List of categories.</returns> 
        /// GET : api/Category
        #endregion
        //[Authorize(Roles = "Manager, Staff")]
        [HttpGet(ApiEndPointConstant.Category.CategoryEndpoint)]
        public async Task<ActionResult<IEnumerable<Category>>> GetAllCategoriesAsync()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }


        #region GetCategoryById
        /// <summary>
        /// Get a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>The category with the specified ID.</returns>
        /// GET : api/Category
        #endregion
        //[Authorize(Roles = "Manager, Staff")]
        [HttpGet(ApiEndPointConstant.Category.CategoryByIdEndpoint)]
        public async Task<ActionResult<Category>> GetCategoryByIdAsync(Guid id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }


        #region CreateCategory
        /// <summary>
        /// Create a new category.
        /// </summary>
        /// <param name="category">The category to create.</param>
        /// <returns>The created category.</returns>
        /// POST : api/Category
        #endregion
        //[Authorize(Roles = "Manager")]
        [HttpPost(ApiEndPointConstant.Category.CategoryEndpoint)]
        public async Task<ActionResult<CategoryResponse>> CreateCategoryAsync(CategoryRequest category)
        {
            var createdCategory = await _categoryService.CreateCategoryAsync(category);
            if (createdCategory == null)
            {
                return BadRequest();
            }
            return createdCategory;
        }


        #region UpdateCategory
        /// <summary>
        /// Update a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to update.</param>
        /// <param name="category">The updated category data.</param>
        /// <returns>The updated category.</returns>
        /// POST : api/Category
        #endregion
        //[Authorize(Roles = "Manager")]
        [HttpPut(ApiEndPointConstant.Category.CategoryByIdEndpoint)]
        public async Task<ActionResult<Category>> UpdateCategoryAsync(Guid id, Category category)
        {
            var updatedCategory = await _categoryService.UpdateCategoryAsync(id, category);
            if (updatedCategory == null)
            {
                return NotFound();
            }
            return Ok(updatedCategory);
        }


        #region DeleteCategory
        /// <summary>
        /// Delete a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to delete.</param>
        /// <returns>A response indicating the result of the delete operation.</returns>
        /// POST : api/Category
        #endregion
        //[Authorize(Roles = "Manager")]
        [HttpDelete(ApiEndPointConstant.Category.CategoryByIdEndpoint)]
        public async Task<IActionResult> DeleteCategoryAsync(Guid id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        
    }
}
