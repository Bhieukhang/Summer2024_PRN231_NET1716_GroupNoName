using Firebase.Storage;
using JSS_BusinessObjects.Models;
using JSS_DataAccessObjects;
using JSS_Repositories;
using JSS_Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_Services.Implement
{
    public class ProductService : BaseService<ProductService>, IProductService
    {
        private readonly string _bucket = "jssimage-253a4.appspot.com";
        public ProductService(IUnitOfWork<JewelrySalesSystemContext> unitOfWork, ILogger<ProductService> logger) : base(unitOfWork, logger)
        {
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
                var product = await _unitOfWork.GetRepository<Product>().FirstOrDefaultAsync(p => p.Code == code);
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
            //var imageUrl = await UploadImageToFirebase(imageStream, imageName);
            //newData.Id = Guid.NewGuid();
            //newData.ImgProduct = imageUrl;
            //newData.InsDate = DateTime.Now;
            //await _unitOfWork.GetRepository<Product>().InsertAsync(newData);
            //bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            //if (!isSuccessful) return null;
            //return newData;
            try
            {
                var imageUrl = await UploadImageToFirebase(imageStream, imageName);
                if (string.IsNullOrEmpty(imageUrl))
                {
                    throw new Exception("Image upload failed, URL is empty.");
                }

                newData.Id = Guid.NewGuid();
                newData.ImgProduct = imageUrl;
                newData.InsDate = DateTime.Now;

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
                existingProduct.TotalPrice = updatedData.TotalPrice != default ? updatedData.TotalPrice : existingProduct.TotalPrice;
                existingProduct.Quantity = updatedData.Quantity != default ? updatedData.Quantity : existingProduct.Quantity;
                existingProduct.ProcessPrice = updatedData.ProcessPrice != default ? updatedData.ProcessPrice : existingProduct.ProcessPrice;
                existingProduct.Code = updatedData.Code ?? existingProduct.Code;
                existingProduct.CategoryId = updatedData.CategoryId != Guid.Empty ? updatedData.CategoryId : existingProduct.CategoryId;
                existingProduct.ProductMaterialId = updatedData.ProductMaterialId ?? existingProduct.ProductMaterialId;
                existingProduct.AccessoryId = updatedData.AccessoryId ?? existingProduct.AccessoryId;

                if (imageStream != null)
                {
                    var imageUrl = await UploadImageToFirebase(imageStream, imageName);
                    existingProduct.ImgProduct = imageUrl;
                } else if (imageStream == null)
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
            var existingProduct = await _unitOfWork.GetRepository<Product>().FirstOrDefaultAsync(a => a.Id == id);
            if (existingProduct == null) return false;

            _unitOfWork.GetRepository<Product>().DeleteAsync(existingProduct);
            return await _unitOfWork.CommitAsync() > 0;
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
    }
}
