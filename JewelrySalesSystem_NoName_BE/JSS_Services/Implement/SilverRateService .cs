using Google;
using JSS_BusinessObjects;
using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Response;
using JSS_DataAccessObjects;
using JSS_Repositories;
using JSS_Repositories.Repo.Interface;
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
    public class SilverRateService : ISilverRateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<SilverRateService> _logger;

        public SilverRateService(IUnitOfWork unitOfWork, ILogger<SilverRateService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<SilverRate> UpdateSilverRateAsync(double newRate)
        {
            var silverRate = new SilverRate
            {
                Id = Guid.NewGuid(),
                Rate = newRate,
                UpsDate = DateTime.Now
            };

            await _unitOfWork.SilverRateRepository.InsertAsync(silverRate);

            var allowedMaterials = new List<string> { "Bạc" };

            var products = await _unitOfWork.ProductRepository
                .GetListAsync(predicate: p => allowedMaterials.Contains(p.Material.MaterialName));

            foreach (var product in products)
            {
                product.SellingPrice = CalculateSellingPrice(product, newRate);
                _unitOfWork.ProductRepository.UpdateAsync(product);
            }

            bool isSuccessful = await _unitOfWork.CommitAsync() > 0;
            if (!isSuccessful)
            {
                throw new Exception("Commit failed, no rows affected.");
            }

            return silverRate;
        }

        public double? CalculateSellingPrice(Product product, double? silverRate)
        {
            return (product.ImportPrice ?? 0) * silverRate + (product.ProcessPrice ?? 0) + (product.Tax ?? 0) * silverRate;
        }

        public async Task<IPaginate<SilverRateResponse>> GetAllSilverRatesAsync(int page, int size)
        {
             IPaginate<SilverRateResponse> list = await _unitOfWork.SilverRateRepository.GetList(
                  selector: x => new SilverRateResponse(x.Id, x.Rate, x.UpsDate),
                orderBy: x => x.OrderByDescending(x => x.UpsDate),
                page: page,
                size: size);
            return list;
        }
    }
}
