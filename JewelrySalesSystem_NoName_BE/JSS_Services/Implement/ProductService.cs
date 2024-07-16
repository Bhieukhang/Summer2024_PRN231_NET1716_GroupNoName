using Firebase.Storage;
using JSS_BusinessObjects;
using JSS_BusinessObjects.DTO;
using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Response;
using JSS_DataAccessObjects;
using JSS_Repositories;
using JSS_Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace JSS_Services.Implement
{
    public class ProductService : BaseService<ProductService>, IProductService
    {
        private readonly string _bucket = "jssimage-253a4.appspot.com";
        private readonly List<Guid> excludeIds = new List<Guid>
        {
            Guid.Parse("b0aae9d9-96f5-43fd-b0ae-379b1fb3f7a1")
        };
        public ProductService(IUnitOfWork<JewelrySalesSystemContext> unitOfWork, ILogger<ProductService> logger)
            : base(unitOfWork, logger)
        {
        }

        //Do Huu Thuan
        public async Task<IPaginate<ProductResponse>> GetProductBySubIdAsync(Guid subId, int page, int size)
        {

            IPaginate<ProductResponse> list = await _unitOfWork.GetRepository<Product>().GetList(
                selector: x => new ProductResponse(x.Id, x.ImgProduct, x.ProductName, x.Description, x.Size, x.SellingPrice, x.Quantity
                , x.CategoryId, x.MaterialId, x.Code, x.ImportPrice, x.InsDate, x.ProcessPrice, x.Deflag, x.Tax, x.SubId, new CategoryResponse(x.Category.Id, x.Category.Name),
                    new MaterialResponse(x.Material.Id, x.Material.MaterialName, x.Material.InsDate), x.PeriodWarranty),
                predicate: x => x.SubId == subId,
                orderBy: x => x.OrderByDescending(x => x.Id),
                page: page,
                size: size);
            return list;
        }
        //Do Huu Thuan
        public async Task<int> GetTotalSubProductAsync()
        {
            var proRepository = _unitOfWork.GetRepository<Product>();
            return await proRepository.CountAsync(x => excludeIds.Contains((Guid)x.SubId));
        }

        //public async Task<IEnumerable<Product>> GetAllProductsAsync()
        //{
        //    return await _unitOfWork.GetRepository<Product>().GetListAsync(include: s => s.Include(p => p.Category).Include(c => c.Material));
        //}

        public async Task<IPaginate<ProductResponse>> GetAllProductsAsync(int page, int size)
        {
            IPaginate<ProductResponse> list = await _unitOfWork.GetRepository<Product>().GetList(
                selector: x => new ProductResponse(x.Id, x.ImgProduct, x.ProductName, x.Description, x.Size, x.SellingPrice, x.Quantity,
                    x.CategoryId, x.MaterialId, x.Code, x.ImportPrice, x.InsDate, x.ProcessPrice, x.Deflag, x.Tax, x.SubId, new CategoryResponse(x.Category.Id, x.Category.Name),
                    new MaterialResponse(x.Material.Id, x.Material.MaterialName, x.Material.InsDate), x.PeriodWarranty),
                predicate: x => x.Deflag == true,
                orderBy: x => x.OrderByDescending(x => x.Id),
                page: page,
                size: size);
            return list;
        }

        public async Task<int> GetTotalProductCountAsync()
        {
            var productRepository = _unitOfWork.GetRepository<Product>();
            return await productRepository.CountAsync();
        }

        public async Task<List<CategoryProductCountResponseDTO>> GetProductCountByCategoryAsync()
        {
            var productRepository = _unitOfWork.GetRepository<Product>();

            var products = await productRepository.GetListAsync(
                include: query => query.Include(p => p.Category)
            );

            var categoryProductCounts = products
                .GroupBy(p => p.CategoryId)
                .Select(g => new CategoryProductCountResponseDTO
                {
                    CategoryId = g.Key,
                    CategoryName = g.First().Category.Name,
                    ProductCount = g.Count(),
                    Products = g.Select(p => new ProductDTO
                    {
                        ProductId = p.Id,
                        ProductName = p.ProductName,
                    }).ToList()
                }).ToList();

            return categoryProductCounts;
        }

        public async Task<IEnumerable<Product>> GetAsync()
        {
            var products = await _unitOfWork.GetRepository<Product>().GetListAsync();
            return products;
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _unitOfWork.GetRepository<Product>().FirstOrDefaultAsync(a => a.Id == id);
        }

        //public async Task<IPaginate<ProductResponse>> SearchAndFilterProductsAsync(string? code, Guid? categoryId, Guid? materialId, int? page, int? size)
        //{
        //    if (!string.IsNullOrEmpty(code))
        //    {
        //        var product = await _unitOfWork.GetRepository<Product>().FirstOrDefaultAsync(
        //            p => p.Code == code,
        //            include: s => s.Include(p => p.Category).Include(c => c.Material)
        //        );

        //        var productList = new List<ProductResponse>();
        //        if (product != null)
        //        {
        //            productList.Add(new ProductResponse(product.Id, product.ImgProduct, product.ProductName, product.Description, product.Size, product.SellingPrice, product.Quantity,
        //            product.CategoryId, product.MaterialId, product.Code, product.ImportPrice, product.InsDate, product.ProcessPrice, product.Deflag, product.Tax, product.SubId, product.Category, product.Material, product.PeriodWarranty));
        //        }

        //        var paginatedProductList = new Paginate<ProductResponse>
        //        {
        //            Items = productList,
        //            Total = productList.Count,
        //            Page = 1,
        //            Size = 12,
        //            TotalPages = productList.Count
        //        };

        //        return paginatedProductList;
        //    }
        //    else
        //    {
        //        IPaginate<ProductResponse> list = await _unitOfWork.GetRepository<Product>().GetList(
        //            selector: x => new ProductResponse(x.Id, x.ImgProduct, x.ProductName, x.Description, x.Size, x.SellingPrice, x.Quantity,
        //                x.CategoryId, x.MaterialId, x.Code, x.ImportPrice, x.InsDate, x.ProcessPrice, x.Deflag, x.Tax, x.SubId, x.Category, x.Material, x.PeriodWarranty),
        //            predicate: x => (categoryId == null || x.CategoryId == categoryId) && (materialId == null || x.MaterialId == materialId),
        //            orderBy: x => x.OrderByDescending(x => x.Id),
        //            page: page ?? 1,
        //            size: size ?? 12);
        //        return list;
        //    }
        //}

        public async Task<ProductResponse> SearchProductByCodeAsync(string code)
        {
            if (string.IsNullOrEmpty(code) || code.Length < 8)
            {
                return null;
            }

            var product = await _unitOfWork.GetRepository<Product>().FirstOrDefaultAsync(
                p => p.Code == code,
                include: s => s.Include(p => p.Category).Include(c => c.Material)
            );

            if (product != null)
            {
                return new ProductResponse(product.Id, product.ImgProduct, product.ProductName, product.Description, product.Size, product.SellingPrice, product.Quantity,
                product.CategoryId, product.MaterialId, product.Code, product.ImportPrice, product.InsDate, product.ProcessPrice, product.Deflag, product.Tax, product.SubId, new CategoryResponse(product.Category.Id, product.Category.Name),
                    new MaterialResponse(product.Material.Id, product.Material.MaterialName, product.Material.InsDate), product.PeriodWarranty);
            }

            return null;
        }

        public async Task<IPaginate<ProductResponse>> FilterProductsAsync(Guid? categoryId, Guid? materialId, int? page, int? size)
        {
            var productRepository = _unitOfWork.GetRepository<Product>();

            var products = await productRepository.GetList(
                selector: x => new ProductResponse(x.Id, x.ImgProduct, x.ProductName, x.Description, x.Size, x.SellingPrice, x.Quantity,
                    x.CategoryId, x.MaterialId, x.Code, x.ImportPrice, x.InsDate, x.ProcessPrice, x.Deflag, x.Tax, x.SubId, new CategoryResponse(x.Category.Id, x.Category.Name), 
                    new MaterialResponse(x.Material.Id, x.Material.MaterialName, x.Material.InsDate), x.PeriodWarranty),
                predicate: x => (categoryId == null || x.CategoryId == categoryId) && (materialId == null || x.MaterialId == materialId),
                orderBy: x => x.OrderByDescending(x => x.Id),
                page: page ?? 1,
                size: size ?? 12);
            return products;
        }

        public async Task<Product> CreateProductAsync(Product newData, Stream imageStream, string imageName)
        {
            try
            {
                var goldRate = await _unitOfWork.GetRepository<GoldRate>().FirstOrDefaultAsync(orderBy: g => g.OrderByDescending(s => s.UpsDate));

                if (goldRate == null)
                {
                    throw new InvalidOperationException("Current gold rate is not available.");
                }

                var imageUrl = await UploadImageToFirebase(imageStream, imageName);
                if (string.IsNullOrEmpty(imageUrl))
                {
                    throw new Exception("Image upload failed, URL is empty.");
                }

                newData.Id = Guid.NewGuid();
                newData.ImgProduct = imageUrl;
                newData.InsDate = DateTime.Now;

                if (!newData.Tax.HasValue)
                {
                    newData.Tax = newData.ImportPrice * 0.1;
                }

                newData.SellingPrice = CalculateSellingPrice(newData, goldRate.Rate);

                await _unitOfWork.GetRepository<Product>().InsertAsync(newData);
                bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
                if (!isSuccessful)
                {
                    throw new Exception("Commit failed, no rows affected.");
                }

                return newData;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating the product.");
                return null;
            }
        }

        public async Task<Product> UpdateProductAsync(Guid id, Product updatedData, Stream imageStream, string imageName)
        {
            try
            {
                var existingProduct = await _unitOfWork.GetRepository<Product>().FirstOrDefaultAsync(a => a.Id == id);
                if (existingProduct == null) return null;

                existingProduct.ProductName = updatedData.ProductName ?? existingProduct.ProductName;
                existingProduct.Description = updatedData.Description ?? existingProduct.Description;
                existingProduct.ImportPrice = updatedData.ImportPrice != default ? updatedData.ImportPrice : existingProduct.ImportPrice;
                existingProduct.Size = updatedData.Size != default ? updatedData.Size : existingProduct.Size;
                existingProduct.Quantity = updatedData.Quantity != default ? updatedData.Quantity : existingProduct.Quantity;
                existingProduct.ProcessPrice = updatedData.ProcessPrice != default ? updatedData.ProcessPrice : existingProduct.ProcessPrice;
                existingProduct.Code = updatedData.Code ?? existingProduct.Code;
                existingProduct.CategoryId = updatedData.CategoryId != Guid.Empty ? updatedData.CategoryId : existingProduct.CategoryId;
                existingProduct.MaterialId = updatedData.MaterialId ?? existingProduct.MaterialId;
                existingProduct.PeriodWarranty = updatedData.PeriodWarranty ?? existingProduct.PeriodWarranty;

                if (!updatedData.Tax.HasValue)
                {
                    existingProduct.Tax = existingProduct.ImportPrice * 0.1;
                }
                else
                {
                    existingProduct.Tax = updatedData.Tax;
                }

                var goldRate = await _unitOfWork.GetRepository<GoldRate>().FirstOrDefaultAsync(orderBy: g => g.OrderByDescending(s => s.UpsDate));
                if (goldRate == null)
                {
                    throw new InvalidOperationException("Current gold rate is not available.");
                }

                existingProduct.SellingPrice = CalculateSellingPrice(existingProduct, goldRate.Rate);

                if (imageStream != null)
                {
                    var imageUrl = await UploadImageToFirebase(imageStream, imageName);
                    existingProduct.ImgProduct = imageUrl;
                }
                else if (imageStream == null)
                {
                    updatedData.ImgProduct = existingProduct.ImgProduct;
                }

                existingProduct.UpsDate = DateTime.Now;

                _unitOfWork.GetRepository<Product>().UpdateAsync(existingProduct);
                bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
                if (!isSuccessful) return null;
                return existingProduct;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the product.");
                if (ex.InnerException != null)
                {
                    _logger.LogError(ex.InnerException, "Inner exception details.");
                }
                throw;
            }
        }

        public async Task<bool> DeleteProductAsync(Guid id)
        {
            try
            {
                var orderDetails = await _unitOfWork.GetRepository<OrderDetail>().GetListAsync(selector: od => od.ProductId == id);
                if (orderDetails.Any())
                {
                    await _unitOfWork.GetRepository<OrderDetail>().DeleteRangeAsync((IEnumerable<OrderDetail>)orderDetails);
                }

                var existingProduct = await _unitOfWork.GetRepository<Product>().FirstOrDefaultAsync(a => a.Id == id);
                if (existingProduct == null) return false;

                _unitOfWork.GetRepository<Product>().DeleteAsync(existingProduct);
                return await _unitOfWork.CommitAsync() > 0;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the product.");
                throw;
            }
        }

        private async Task<string> UploadImageToFirebase(Stream imageStream, string imageName)
        {
            var storage = new FirebaseStorage(_bucket);
            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(imageName);

            var uploadTask = storage
                .Child("uploads")
                .Child(uniqueFileName)
                .PutAsync(imageStream);

            return await uploadTask;
        }

        public async Task<ProductMapPromotion> GetPromotionByProductCode(string productCode)
        {
            ProductMapPromotion promotionMapProduct = new ProductMapPromotion();

            var productItem = await _unitOfWork.GetRepository<Product>()
                                               .FirstOrDefaultAsync(p => p.Code == productCode,
                                                                    include: p => p.Include(p => p.ProductConditionGroups));
            if (productItem == null)
            {
                return promotionMapProduct;
            }

            ProResponse productResponse = new ProResponse(productItem.Id, productItem.ImgProduct, productItem.ProductName, productItem.Description,
                                                           productItem.Size, productItem.SellingPrice, productItem.Quantity,
                                                           productItem.CategoryId, productItem.MaterialId, productItem.Code,
                                                           productItem.ImportPrice, productItem.ProcessPrice,
                                                           productItem.Deflag, productItem.Tax, productItem.SubId, productItem.Category, productItem.Material, productItem.PeriodWarranty);
            promotionMapProduct.Product = productResponse;
            List<ProductConditionGroup> listProductMapPromotion = productItem.ProductConditionGroups.ToList();

            foreach (var conditionGroup in listProductMapPromotion)
            {
                if (conditionGroup != null)
                {
                    var promotionItem = await _unitOfWork.GetRepository<Promotion>()
                                                         .FirstOrDefaultAsync(p => p.Id == conditionGroup.PromotionId &&
                                                                                   p.StartDate < DateTime.Now &&
                                                                                   p.EndDate > DateTime.Now);
                    if (promotionItem != null)
                    {
                        promotionMapProduct.Promotions.Add(promotionItem);
                    }
                }
            }
            return promotionMapProduct;
        }

        public async Task<ProductMapPromotionItem> GetPromotionByCode(string productCode)
        {
            ProductMapPromotionItem promotionMapProduct = new ProductMapPromotionItem();

            var productItem = await _unitOfWork.GetRepository<Product>()
                                               .FirstOrDefaultAsync(p => p.Code == productCode,
                                                                    include: p => p.Include(p => p.ProductConditionGroups));
            if (productItem == null)
            {
                return promotionMapProduct;
            }
            promotionMapProduct.ProductCode = productItem.Code;
            List<ProductConditionGroup> listProductMapPromotion = productItem.ProductConditionGroups.ToList();

            foreach (var conditionGroup in listProductMapPromotion)
            {
                if (conditionGroup != null)
                {
                    var promotionItem = await _unitOfWork.GetRepository<Promotion>()
                                                         .FirstOrDefaultAsync(p => p.Id == conditionGroup.PromotionId &&
                                                                                   p.StartDate < DateTime.Now &&
                                                                                   p.EndDate > DateTime.Now);
                    if (promotionItem != null)
                    {
                        promotionMapProduct.Promotions.Add(promotionItem);
                    }
                }
            }
            return promotionMapProduct;
        }

        private double? CalculateSellingPrice(Product product, double? goldRate)
        {
            return (product.ImportPrice ?? 0) * goldRate + (product.ProcessPrice ?? 0) + (product.Tax ?? 0);
        }

        public async Task<IEnumerable<ProductResponse>> AutocompleteProductsAsync(string query)
        {
            var products = await _unitOfWork.GetRepository<Product>().GetListAsync(
                selector: p => new ProductResponse(p.Id, p.ImgProduct, p.ProductName, p.Description, p.Size, p.SellingPrice, p.Quantity,
                                                   p.CategoryId, p.MaterialId, p.Code, p.ImportPrice, p.InsDate, p.ProcessPrice, p.Deflag, p.Tax, p.SubId, new CategoryResponse(p.Category.Id, p.Category.Name),
                    new MaterialResponse(p.Material.Id, p.Material.MaterialName, p.Material.InsDate), p.PeriodWarranty),
                predicate: p => p.ProductName.Contains(query) || p.Code.Contains(query),
                orderBy: p => p.OrderBy(p => p.ProductName)
            );

            return products;
        }

        public async Task AddProductConditionGroup(Guid productId, Guid promotionId)
        {
            var condition = new ProductConditionGroup()
            {
                Id = Guid.NewGuid(),
                InsDate = DateTime.Now,
                ProductId = productId,
                PromotionId = promotionId,
                Quantity = 1,
            };

            await _unitOfWork.GetRepository<ProductConditionGroup>().InsertAsync(condition);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteProductConditionGroup(Guid promotionId)
        {
            var range = await _unitOfWork.GetRepository<ProductConditionGroup>().GetListAsync();
            range = range.Where(x => x.PromotionId == promotionId).ToList();

            await _unitOfWork.GetRepository<ProductConditionGroup>().DeleteRangeAsync(range);
            await _unitOfWork.CommitAsync();
        }
    }
}
