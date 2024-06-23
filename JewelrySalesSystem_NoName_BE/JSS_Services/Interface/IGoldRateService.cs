using JSS_BusinessObjects;
using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSS_Services.Interface
{
    public interface IGoldRateService
    {
        double? CalculateSellingPrice(Product newData, double? rate);
        Task<IPaginate<GoldRateResponse>> GetAllGoldRatesAsync(int page, int size);
        //Task<GoldRate> GetLatestGoldRateAsync();
        Task<GoldRate> UpdateGoldRateAsync(double newRate);
    }
}
