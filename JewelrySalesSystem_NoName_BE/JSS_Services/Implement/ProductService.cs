﻿using Firebase.Storage;
using JSS_BusinessObjects;
using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Request;
using JSS_BusinessObjects.Payload.Response;
using JSS_DataAccessObjects;
using JSS_Repositories;
using JSS_Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace JSS_Services.Implement
{
    public class ProductService : BaseService<ProductService>, IProductService
    {
        private readonly string _bucket = "jssimage-253a4.appspot.com";

        public ProductService(IUnitOfWork<JewelrySalesSystemContext> unitOfWork, ILogger<ProductService> logger)
            : base(unitOfWork, logger)
        {
        }

        //get all subid
        public async Task<IPaginate<ProductResponse>> GetProductBySubIdAsync(Guid subId, int page, int size)
        {

                IPaginate<ProductResponse> list = await _unitOfWork.GetRepository<Product>().GetList(
                    selector: x => new ProductResponse(x.Id, x.ImgProduct, x.ProductName, x.Description, x.Size, x.SellingPrice, x.Quantity, x.CategoryId, x.MaterialId, x.Code, x.ImportPrice, x.InsDate, x.ProcessPrice, x.Deflag, x.SubId),
                    predicate: x => x.SubId == subId,
                    orderBy: x => x.OrderByDescending(x => x.Id),
                    page: page,
                    size: size);
                return list;
        }






        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _unitOfWork.GetRepository<Product>().GetListAsync(include: s => s.Include(p => p.Category));
        }

        public async Task<Product> GetProductByIdAsync(Guid id)
        {
            return await _unitOfWork.GetRepository<Product>().FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Product> GetProductByCodeAsync(string code)
        {
            try
            {
                var product = await _unitOfWork.GetRepository<Product>().FirstOrDefaultAsync(p => p.Code == code, include: s => s.Include(p => p.Category));
                return product;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the product by code.");
                throw;
            }
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

            ProductResponse productResponse = new ProductResponse(productItem.Id, productItem.ImgProduct, productItem.ProductName, productItem.Description,
                                                           productItem.Size, productItem.SellingPrice, productItem.Quantity,
                                                           productItem.CategoryId, productItem.MaterialId, productItem.Code,
                                                           productItem.ImportPrice, productItem.InsDate, productItem.ProcessPrice,
                                                           productItem.Deflag, productItem.Tax, productItem.SubId);
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

        private double? CalculateSellingPrice(Product product, double? goldRate)
        {
            return (product.ImportPrice ?? 0) * goldRate + (product.ProcessPrice ?? 0) + (product.Tax ?? 0);
        }
    }
}
