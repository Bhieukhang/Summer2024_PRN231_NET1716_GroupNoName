using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Request;
using JSS_DataAccessObjects;
using JSS_Repositories;
using JSS_Services.Interface;
using Microsoft.Extensions.Logging;

namespace JSS_Services.Implement
{
    public class DashboardService : BaseService<DashboardService>, IDashboardService
    {

        public DashboardService(IUnitOfWork<JewelrySalesSystemContext> unitOfWork,
            ILogger<DashboardService> logger) : base(unitOfWork, logger)
        {
        }


        public async Task<DashboardRequest> GetDashboardsAsync(int year)
        {

            var promotions = await _unitOfWork.GetRepository<Promotion>().GetListAsync();
            var orders = await _unitOfWork.GetRepository<Order>().GetListAsync();
            int totalPromotion = promotions == null ? 0 : promotions.Count;
            decimal totalRevenue = orders == null ? 0 : decimal.Parse(orders.Sum(p => p.TotalPrice).ToString() ?? "0");

            var dash = new DashboardRequest
            {
                TotalPromotion = totalPromotion,
                TotalRevenue = totalRevenue
            };

            if (orders != null && orders.Any())
            {
                var monthlyRevenue = orders
                        .Where(x => x.InsDate?.Year == year)
                        .GroupBy(x => x.InsDate?.Month)
                        .Select(x => new MonthlyRevenue()
                        {
                            Month = x.Key ?? 0,
                            Value = x.Sum(r => r.TotalPrice) ?? 0
                        })
                        .ToList();

                dash.MonthlyRevenue = monthlyRevenue;
            }

            return dash;
        }

        public async Task<AccountDashboard> GetDashboardAccount()
        {
            var listAccount = await _unitOfWork.GetRepository<Account>().GetListAsync();
            var adminCount = listAccount.Count(a => a.RoleId == Guid.Parse("0F8FAD5B-D9CB-469F-A165-70867728950E"));
            var managerCount = listAccount.Count(a => a.RoleId == Guid.Parse("7C9E6679-7425-40DE-944B-E07FC1F90AE7"));
            var staffCount = listAccount.Count(a => a.RoleId == Guid.Parse("7C9E6679-7425-40DE-944B-E07FC1F90AE8"));
            var customerCount = listAccount.Count(a => a.RoleId == Guid.Parse("7C9E6679-7425-40DE-944B-E07FC1F90AE9"));
            var dashboard = new AccountDashboard
            {
                TotalAccount = listAccount.Count,
                TotalAdmin = adminCount,
                TotalManager = managerCount,
                TotalStaff = staffCount,
                TotalCustomer = customerCount,
            };

            return dashboard;
        }

    }
}
