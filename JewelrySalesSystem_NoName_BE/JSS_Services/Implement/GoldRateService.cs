using Google;
using JSS_BusinessObjects.Models;
using JSS_DataAccessObjects;
using JSS_Services.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_Services.Implement
{
    public class GoldRateService : IGoldRateService
    {
        private readonly JewelrySalesSystemContext _context;

        public GoldRateService(JewelrySalesSystemContext context)
        {
            _context = context;
        }

        public async Task<GoldRate> GetLatestGoldRateAsync()
        {
            return await _context.GoldRates.OrderByDescending(g => g.UpsDate).FirstOrDefaultAsync();
        }

        public async Task<GoldRate> UpdateGoldRateAsync(double newRate)
        {
            var goldRate = new GoldRate
            {
                Id = Guid.NewGuid(),
                Rate = newRate,
                UpsDate = DateTime.UtcNow
            };

            _context.GoldRates.Add(goldRate);
            await _context.SaveChangesAsync();

            // Cập nhật giá nhập vào và giá bán ra của tất cả sản phẩm
            var products = await _context.Products.ToListAsync();
            foreach (var product in products)
            {
                product.ImportPrice = product.ImportPrice * newRate;
                product.SellingPrice = product.SellingPrice * newRate;
            }

            await _context.SaveChangesAsync();

            return goldRate;
        }
    }
}
