using JewelrySalesSystem_NoName_BE.Extenstion;
using JSS_BusinessObjects.Models;
using JSS_Services.Interface;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JewelrySalesSystem_NoName_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        #region GetAllProducts
        /// <summary>
        /// Get all products.
        /// </summary>
        /// <returns>List of products.</returns>
        /// GET : api/Product
        #endregion
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


        #region CreateProduct
        /// <summary>
        /// Create a new product.
        /// </summary>
        /// <param name="product">The product to create.</param>
        /// <returns>The created product.</returns>
        /// POST : api/Product
        #endregion
        [HttpPost(ApiEndPointConstant.Product.ProductEndpoint)]
        public async Task<ActionResult<Product>> CreateProductAsync(Product product)
        {
            var createdProduct = await _productService.CreateProductAsync(product);
            if (createdProduct == null)
            {
                return BadRequest();
            }
            return createdProduct;
        }
        

        #region UpdateProduct
        /// <summary>
        /// Update a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to update.</param>
        /// <param name="product">The updated product data.</param>
        /// <returns>The updated product.</returns>
        /// POST : api/Product
        #endregion
        [HttpPut((ApiEndPointConstant.Product.ProductByIdEndpoint))]
        public async Task<ActionResult<Product>> UpdateProductAsync(Guid id, Product product)
        {
            var updatedProduct = await _productService.UpdateProductAsync(id, product);
            if (updatedProduct == null)
            {
                return NotFound();
            }
            return Ok(updatedProduct);
        }


        #region DeleteProduct
        /// <summary>
        /// Delete a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to delete.</param>
        /// <returns>A response indicating the result of the delete operation.</returns>
        /// POST : api/Product

        [HttpDelete(ApiEndPointConstant.Product.ProductEndpoint)]
        public async Task<IActionResult> DeleteProductAsync(Guid id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return NoContent();
        }
        #endregion
    }
}
