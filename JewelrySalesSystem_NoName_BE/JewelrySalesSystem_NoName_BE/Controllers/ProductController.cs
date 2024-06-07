using JewelrySalesSystem_NoName_BE.Extenstion;
using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;
using JSS_Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JewelrySalesSystem_NoName_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        #region GetAllProducts
        /// <summary>
        /// Get all products.
        /// </summary>
        /// <returns>List of products.</returns>
        /// GET : api/Product
        #endregion
        [Authorize(Roles = "Admin, Manager, Staff")]
        [HttpGet(ApiEndPointConstant.Product.ProductEndpoint)]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProductsAsync()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        #region GetProductById
        /// <summary>
        /// Get a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The product with the specified ID.</returns>
        /// GET : api/Product
        #endregion
        [Authorize(Roles = "Admin, Manager, Staff")]
        [HttpGet(ApiEndPointConstant.Product.ProductByIdEndpoint)]
        public async Task<ActionResult<Product>> GetProductByIdAsync(Guid id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        #region SearchProductByCode
        /// <summary>
        /// Search a product by its code.
        /// </summary>
        /// <param name="code">The code of the product to search.</param>
        /// <returns>The product with the specified code.</returns>
        /// GET : api/Product/search
        #endregion
        [HttpGet(ApiEndPointConstant.Product.ProductByCodeEndpoint)]
        public async Task<ActionResult<Product>> SearchProductByCode(string code)
        {
            var product = await _productService.GetProductByCodeAsync(code);
            if (product == null)
            {
                return NotFound();
            }

            var searchproduct = new Product
            {
                Id = product.Id,
                ProductName = product.ProductName,
                Description = product.Description,
                Code = product.Code,
                Deflag = product.Deflag,
                ImportPrice = product.ImportPrice,
                InsDate = product.InsDate,
                ProcessPrice = product.ProcessPrice,
                TotalPrice = product.TotalPrice,
                Size = product.Size,
                Quantity = product.Quantity,
                ImgProduct = product.ImgProduct,
                CategoryId = product.CategoryId,
                MaterialId = product.MaterialId,
                Category = product.Category,
            };
            return Ok(searchproduct);
        }

        #region CreateProduct
        /// <summary>
        /// Create a new product.
        /// </summary>
        /// <param name="product">The product to create.</param>
        /// <returns>The created product.</returns>
        /// POST : api/Product
        #endregion
        [Authorize(Roles = "Manager")]
        [HttpPost(ApiEndPointConstant.Product.ProductEndpoint)]
        public async Task<IActionResult> CreateProduct([FromBody] ProductRequest productRequest)
        {
            if (string.IsNullOrEmpty(productRequest.ImgProduct))
                return BadRequest("No image uploaded.");

            var stream = new MemoryStream(Convert.FromBase64String(productRequest.ImgProduct));
            var category = await _categoryService.GetCategoryByIdAsync(productRequest.CategoryId);
            if (category == null)
            {
                return NotFound("Category not found.");
            }
            var product = new Product
            {
                Code = productRequest.Code,
                ProductName = productRequest.ProductName,
                Description = productRequest.Description,
                Deflag = productRequest.Deflag,
                CategoryId = productRequest.CategoryId,
                TotalPrice = productRequest.TotalPrice,
                ProcessPrice = productRequest.ProcessPrice,
                ImgProduct = productRequest.ImgProduct,
                ImportPrice = productRequest.ImportPrice,
                Size = productRequest.Size,
                Quantity = productRequest.Quantity,
                InsDate = productRequest.InsDate,
                MaterialId = productRequest.MaterialId
            };

            var createdProduct = await _productService.CreateProductAsync(product, stream, "uploadedFileName");
            if (createdProduct == null)
            {
                return StatusCode(500, "An error occurred while creating the product.");
            }

            return Ok(new ProductResponse(
                createdProduct.Id,
                createdProduct.ImgProduct,
                createdProduct.ProductName,
                createdProduct.Description,
                createdProduct.Size,
                createdProduct.TotalPrice,
                createdProduct.Quantity,
                createdProduct.CategoryId,
                createdProduct.MaterialId,
                createdProduct.Code,
                createdProduct.ImportPrice,
                createdProduct.InsDate,
                createdProduct.ProcessPrice,
                createdProduct.Deflag
            ));
        }


        #region UpdateProduct
        /// <summary>
        /// Update a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="product">The updated product data.</param>
        /// <returns>The updated product.</returns>
        /// PUT : api/Product/id
        #endregion
        [Authorize(Roles = "Manager")]
        [HttpPut((ApiEndPointConstant.Product.ProductByIdEndpoint))]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductRequest productRequest)
        {
            Stream stream = null;
            if (!string.IsNullOrEmpty(productRequest.ImgProduct))
            {
                stream = new MemoryStream(Convert.FromBase64String(productRequest.ImgProduct));
            }
            var existingProduct = await _productService.GetProductByIdAsync(id);
            if (existingProduct == null)
            {
                return NotFound("Product not found.");
            }

            if (productRequest.CategoryId != Guid.Empty && productRequest.CategoryId != existingProduct.CategoryId)
            {
                var category = await _categoryService.GetCategoryByIdAsync(productRequest.CategoryId);
                if (category == null)
                {
                    productRequest.CategoryId = existingProduct.CategoryId;
                }
            }

            var product = new Product
            {
                ProductName = productRequest.ProductName,
                Description = productRequest.Description,
                Deflag = productRequest.Deflag,
                CategoryId = productRequest.CategoryId,
                TotalPrice = productRequest.TotalPrice,
                ProcessPrice = productRequest.ProcessPrice,
                ImgProduct = productRequest.ImgProduct,
                ImportPrice = productRequest.ImportPrice,
                Size = productRequest.Size,
                Code = productRequest.Code,
                Quantity = productRequest.Quantity,
                InsDate = productRequest.InsDate,
                MaterialId = productRequest.MaterialId
            };

            var updatedProduct = await _productService.UpdateProductAsync(id, product, stream, "uploadedFileName");
            if (updatedProduct == null)
            {
                return StatusCode(500, "An error occurred while updating the product.");
            }

            return Ok(new ProductResponse(
                updatedProduct.Id,
                updatedProduct.ImgProduct,
                updatedProduct.ProductName,
                updatedProduct.Description,
                updatedProduct.Size,
                updatedProduct.TotalPrice,
                updatedProduct.Quantity,
                updatedProduct.CategoryId,
                updatedProduct.MaterialId,
                updatedProduct.Code,
                updatedProduct.ImportPrice,
                updatedProduct.InsDate,
                updatedProduct.ProcessPrice,
                updatedProduct.Deflag
            ));
        }

        #region DeleteProduct
        /// <summary>
        /// Delete a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>A response indicating the result of the delete operation.</returns>
        /// POST : api/Product
        #endregion
        [Authorize(Roles = "Manager")]
        [HttpDelete(ApiEndPointConstant.Product.ProductByIdEndpoint)]
        public async Task<IActionResult> DeleteProductAsync(Guid id)
        {
            try
            {
                var result = await _productService.DeleteProductAsync(id);
                if (!result)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Error = ex.Message, TimeStamp = DateTime.UtcNow });
            }
        }

    }
}
