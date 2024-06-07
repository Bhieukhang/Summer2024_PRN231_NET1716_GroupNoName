using Google;
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
    public class GoldRateService : BaseService<GoldRateService>, IGoldRateService
    {
        private readonly JewelrySalesSystemContext _context;

        public GoldRateService(IUnitOfWork<JewelrySalesSystemContext> unitOfWork, ILogger<GoldRateService> logger) : base(unitOfWork, logger)
        {
        }

        public async Task<GoldRate> GetLatestGoldRateAsync()
        {
            return await _unitOfWork.GetRepository<GoldRate>().FirstOrDefaultAsync(orderBy : g => g.OrderByDescending(s => s.UpsDate));
        }

        public async Task<GoldRate> UpdateGoldRateAsync(double newRate)
        {
            var goldRate = new GoldRate
            {
                Id = Guid.NewGuid(),
                Rate = newRate,
                UpsDate = DateTime.UtcNow
            };

            await _unitOfWork.GetRepository<GoldRate>().InsertAsync(goldRate);

            // Cập nhật giá bán ra của tất cả sản phẩm
            var products = await _unitOfWork.GetRepository<Product>().GetListAsync();
            foreach (var product in products)
            {
                product.SellingPrice = CalculateSellingPrice(product, newRate);
                _unitOfWork.GetRepository<Product>().UpdateAsync(product);
            }
            
            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (!isSuccessful)
            {
                throw new Exception("Commit failed, no rows affected.");
            }

            return goldRate;
        }

        // Phương thức tính toán giá bán
        public double? CalculateSellingPrice(Product product, double? goldRate)
        {
            return (product.ImportPrice ?? 0) * goldRate + (product.ProcessPrice ?? 0) + (product.Tax ?? 0) * goldRate;
        }
    }
}
