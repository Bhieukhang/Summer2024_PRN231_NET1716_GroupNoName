using JSS_BusinessObjects.Models;
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
        Task<GoldRate> GetLatestGoldRateAsync();
        Task<GoldRate> UpdateGoldRateAsync(double newRate);
    }
}
