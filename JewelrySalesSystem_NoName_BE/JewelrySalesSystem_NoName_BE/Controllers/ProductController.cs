using JewelrySalesSystem_NoName_BE.Extenstion;
using JSS_BusinessObjects;
using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;
using JSS_Services.Implement;
using JSS_Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace JewelrySalesSystem_NoName_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        #region AutocompleteProducts
        /// <summary>
        /// Autocomplete products based on a query string.
        /// </summary>
        /// <param name="query">The query string to search for products.</param>
        /// <returns>List of products matching the query.</returns>
        /// GET : api/Product/autocomplete
        #endregion
        [HttpGet(ApiEndPointConstant.Product.ProductAutocompleteEndpoint)]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> AutocompleteProducts(string query)
        {
            var results = await _productService.AutocompleteProductsAsync(query);
            return Ok(results);
        }

        #region GetAllProducts
        /// <summary>
        /// Get all products.
        /// </summary>
        /// <param name="page">Page number for pagination.</param>
        /// <param name="size">Page size for pagination.</param>
        /// <returns>List of products.</returns>
        /// GET : api/Product
        #endregion
        //[Authorize(Roles = "Admin, Manager, Staff")]
        [HttpGet(ApiEndPointConstant.Product.ProductEndpoint)]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllProductsAsync(int page, int size)
        {
            var list = await _productService.GetAllProductsAsync(page, size);
            var products = JsonConvert.SerializeObject(list, Formatting.Indented);
            return Ok(products);
        }

        [HttpGet(ApiEndPointConstant.Product.AllProductEndpoint)]
        public async Task<ActionResult<IEnumerable<Product>>> GetAsync()
        {
            var list = await _productService.GetAsync();
            return Ok(list);
        }

        #region GetProductById
        /// <summary>
        /// Get a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The product with the specified ID.</returns>
        /// GET : api/Product
        #endregion
        //[Authorize(Roles = "Admin, Manager, Staff")]
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



        #region ProductBySubIdEndpoint
        /// <summary>
        /// Get a product by its Sub ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>The product with the specified ID.</returns>
        /// GET : api/Product/subid
        #endregion
   //     [Authorize(Roles = "Admin, Manager, Staff")]
        [HttpGet(ApiEndPointConstant.Product.ProductBySubIdEndpoint)]
        public async Task<IActionResult> GetProductBySubIdAsync(Guid subId, int page, int size)
        {
            var list = await _productService.GetProductBySubIdAsync(subId, page, size);
            var pro = JsonConvert.SerializeObject(list, Formatting.Indented);
            return Ok(pro);
        }

        #region CheckProductHasCategory
        /// <summary>
        /// Check Product Has Category.
        /// </summary>
        /// <param name="categoryId">The ID category of the product to check existed.</param>
        /// <returns>The result of checking.</returns>
        /// GET : api/Product/subid
        #endregion
        [HttpGet(ApiEndPointConstant.Product.ProductByCategoryEndpoint)]
        public async Task<ActionResult<bool>> HasProductsWithCategory(Guid categoryId)
        {
            var hasProducts = await _productService.HasProductsWithCategoryAsync(categoryId);
            return Ok(hasProducts);
        }

        #region SearchProductByCode
        /// <summary>
        /// Search a product by its code.
        /// </summary>
        /// <param name="code">The code of the product to search.</param>
        /// <returns>Product with code.</returns>
        /// GET : api/Product/searchCode
        #endregion
        [HttpGet(ApiEndPointConstant.Product.ProductByCodeEndpoint)]
        public async Task<IActionResult> SearchProductByCode(string code)
        {
            var product = await _productService.SearchProductByCodeAsync(code);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        #region SearchProductByName
        /// <summary>
        /// Search a product by its name.
        /// </summary>
        /// <param name="name">The name of the product to search.</param>
        /// <returns>Product with name.</returns>
        /// GET : api/Product/searchName
        #endregion
        [HttpGet(ApiEndPointConstant.Product.ProductByNameEndpoint)]
        public async Task<IActionResult> SearchProductByName(string name)
        {
            var product = await _productService.SearchProductByNameAsync(name);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        #region FilterProducts
        /// <summary>
        /// Get products by category and material.
        /// </summary>
        /// <param name="categoryId">The ID of the category to filter by.</param>
        /// <param name="materialId">The ID of the material to filter by.</param>
        /// <param name="page">Page number for pagination.</param>
        /// <param name="size">Page size for pagination.</param>
        /// <returns>List of filtered products.</returns>
        /// GET : api/Product/filter
        #endregion
        [HttpGet(ApiEndPointConstant.Product.FilterProductEndpoint)]
        public async Task<IActionResult> FilterProducts(Guid? categoryId, Guid? materialId, int? page, int? size)
        {
            var products = await _productService.FilterProductsAsync(categoryId, materialId, page, size);
            return Ok(products);
        }

        #region CreateProduct
        /// <summary>
        /// Create a new product.
        /// </summary>
        /// <param name="product">The product to create.</param>
        /// <returns>The created product.</returns>
        /// POST : api/Product
        #endregion
        //[Authorize(Roles = "Admin, Manager")]
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
                SellingPrice = productRequest.SellingPrice,
                ProcessPrice = productRequest.ProcessPrice,
                ImgProduct = productRequest.ImgProduct,
                ImportPrice = productRequest.ImportPrice,
                Size = productRequest.Size,
                Quantity = productRequest.Quantity,
                InsDate = productRequest.InsDate,
                MaterialId = productRequest.MaterialId,
                Tax = productRequest.Tax,
                PeriodWarranty = productRequest.PeriodWarranty,
                SubId = productRequest.SubId,
            };

            var createdProduct = await _productService.CreateProductAsync(product, stream, "uploadedFileName");
            if (createdProduct == null)
            {
                return StatusCode(500, "An error occurred while creating the product.");
            }

            return Ok(new ProductCEResponse(
                createdProduct.Id,
                createdProduct.ImgProduct,
                createdProduct.ProductName,
                createdProduct.Description,
                createdProduct.Size,
                createdProduct.SellingPrice,
                createdProduct.InsDate,
                createdProduct.Quantity,
                createdProduct.CategoryId,
                createdProduct.MaterialId,
                createdProduct.Code,
                createdProduct.ImportPrice,
                createdProduct.ProcessPrice,
                createdProduct.Deflag,
                createdProduct.Tax,
                createdProduct.SubId,
                createdProduct.Category,
                createdProduct.Material,
                createdProduct.PeriodWarranty
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
        //[Authorize(Roles = "Admin,Manager")]
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
                SellingPrice = productRequest.SellingPrice,
                ProcessPrice = productRequest.ProcessPrice,
                ImgProduct = productRequest.ImgProduct,
                ImportPrice = productRequest.ImportPrice,
                Size = productRequest.Size,
                Code = productRequest.Code,
                Quantity = productRequest.Quantity,
                InsDate = productRequest.InsDate,
                MaterialId = productRequest.MaterialId,
                Tax = productRequest.Tax,
                PeriodWarranty = productRequest.PeriodWarranty,
                SubId = productRequest.SubId,
            };

            var updatedProduct = await _productService.UpdateProductAsync(id, product, stream, "uploadedFileName");
            if (updatedProduct == null)
            {
                return StatusCode(500, "An error occurred while updating the product.");
            }

            return Ok(new ProductCEResponse(
                updatedProduct.Id,
                updatedProduct.ImgProduct,
                updatedProduct.ProductName,
                updatedProduct.Description,
                updatedProduct.Size,
                updatedProduct.SellingPrice,
                updatedProduct.InsDate,
                updatedProduct.Quantity,
                updatedProduct.CategoryId,
                updatedProduct.MaterialId,
                updatedProduct.Code,
                updatedProduct.ImportPrice,
                updatedProduct.ProcessPrice,
                updatedProduct.Deflag,
                updatedProduct.Tax,
                updatedProduct.SubId,
                updatedProduct.Category,
                updatedProduct.Material,
                updatedProduct.PeriodWarranty
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

        #region ListPromotionFromProductCode
        /// <summary>
        /// Search product code by product code to get list promotion
        /// </summary>
        /// <param name="productCode">The Product code of Product.</param>
        /// <returns>Product item and list promotion mapping product.</returns>
        /// POST : api/Product
        #endregion
        [HttpGet((ApiEndPointConstant.Product.ProductByCodePromotionEndpoint))]
        public async Task<IActionResult> ListPromotionFromProductCode(string productCode)
        {
            var listPromotion = await _productService.GetPromotionByProductCode(productCode);
            var result = JsonConvert.SerializeObject(listPromotion, Formatting.Indented);
            return Ok(result);
        }

        #region PromotionFromProductCode
        /// <summary>
        /// Search product code by product code to get promotion item
        /// </summary>
        /// <param name="productCode">The Product code of Product.</param>
        /// <returns>Product code item and list promotion mapping product.</returns>
        /// POST : api/Product
        #endregion
        [HttpGet((ApiEndPointConstant.Product.ProductItemByCodePromotionEndpoint))]
        public async Task<IActionResult> PromotionFromProductCode(string productCode)
        {
            var listPromotion = await _productService.GetPromotionByCode(productCode);
            var result = JsonConvert.SerializeObject(listPromotion, Formatting.Indented);
            return Ok(result);
        }


        //DO Huu Thuan
        #region GetTotalSubProductAsync
        /// <summary>
        /// Get Total SubProductAsync.
        /// </summary>
        /// <returns>List of SubProductAsync.</returns>
        // GET: api/Product
        #endregion
        //   [Authorize(Roles = "Manager, Admin")]
        [HttpGet(ApiEndPointConstant.Product.TotalPurchasePriceEndpoint)]
        public async Task<ActionResult<int>> GetTotalSubProductAsync()
        {
            var TotalPurchasePrice = await _productService.GetTotalSubProductAsync();
            return Ok(TotalPurchasePrice);
        }
    }
}
