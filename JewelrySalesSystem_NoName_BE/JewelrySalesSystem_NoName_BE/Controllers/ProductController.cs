using JewelrySalesSystem_NoName_BE.Extenstion;
using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;
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
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService=categoryService;
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
        //public async Task<IActionResult> CreateProduct([FromForm] ProductRequest productRequest, [FromForm] IFormFile file)
        //{
        //    if (file == null || file.Length == 0)
        //        return BadRequest("No file uploaded.");

        //    using (var stream = file.OpenReadStream())
        //    {
        //        var product = new Product
        //        {   
        //            Code = productRequest.Code,
        //            ProductName = productRequest.ProductName,
        //            Description = productRequest.Description,
        //            Deflag = productRequest.Deflag,
        //            CategoryId = productRequest.CategoryId,
        //            ImgProduct = productRequest.ImgProduct,
        //            ImportPrice = productRequest.ImportPrice,
        //            Size = productRequest.Size,
        //            Quantity = productRequest.Quantity,
        //            InsDate = productRequest.InsDate,
        //            ProductMaterialId = productRequest.ProductMaterialId,
        //        };

        //        var createdProduct = await _productService.CreateProductAsync(product, stream, file.FileName);
        //        if (createdProduct == null)
        //        {
        //            return StatusCode(500, "An error occurred while creating the product.");
        //        }

        //        return Ok(new ProductResponse(
        //            createdProduct.Id,
        //            createdProduct.ImgProduct,
        //            createdProduct.ProductName,
        //            createdProduct.Description,
        //            createdProduct.Size,
        //            createdProduct.TotalPrice,
        //            createdProduct.Quantity,
        //            createdProduct.AccessoryId,
        //            createdProduct.ProductMaterialId,
        //            createdProduct.Code
        //        ));
        //    }
        //}
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
                ProductMaterialId = productRequest.ProductMaterialId
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
                createdProduct.AccessoryId,
                createdProduct.ProductMaterialId,
                createdProduct.Code
            ));
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
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductRequest productRequest)
        {
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
                ProductMaterialId = productRequest.ProductMaterialId
            };

            var updatedProduct = await _productService.UpdateProductAsync(id, product, stream, "updatedFileName");
            if (updatedProduct == null)
            {
                return StatusCode(500, "An error occurred while updating the product.");
            }
            if (updatedProduct.ImgProduct == null)
            {
                updatedProduct.ImgProduct = productRequest.ImgProduct;
            }

            return Ok(new ProductResponse(
                updatedProduct.Id,
                updatedProduct.ImgProduct,
                updatedProduct.ProductName,
                updatedProduct.Description,
                updatedProduct.Size,
                updatedProduct.TotalPrice,
                updatedProduct.Quantity,
                updatedProduct.AccessoryId,
                updatedProduct.ProductMaterialId,
                updatedProduct.Code
            ));
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
