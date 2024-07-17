using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Request;
using JSS_DataAccessObjects;
using JSS_Repositories;
using JSS_Repositories.Repo.Interface;
using JSS_Services.Interface;
using Microsoft.Extensions.Logging;
using System.Data.Entity;

namespace JSS_Services.Implement
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DashboardService> _logger;

        public DashboardService(IUnitOfWork unitOfWork, ILogger<DashboardService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<DashboardRequest> GetDashboardsAsync(int year)
        {

            var promotions = await _unitOfWork.PromotionRepository.GetListAsync();
            var orders = await _unitOfWork.OrderRepository.GetListAsync();
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
            var listAccount = await _unitOfWork.AccountRepository.GetListAsync();
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

        public async Task<MembershipDashboard> GetMemberDashboard()
        {
            var staticMember = await _unitOfWork.MembershipRepository.GetListAsync();
            var newMemberCount = staticMember.Count(a => a.MemberTypeId == Guid.Parse("18095960-ACB3-4FD4-BCD3-646D9DF3E6E1"));
            var goldCount = staticMember.Count(a => a.MemberTypeId == Guid.Parse("CAC7BD96-530A-4184-989E-71218B2AAA0D"));
            var bronzeCount = staticMember.Count(a => a.MemberTypeId == Guid.Parse("BBC11F4D-06F4-4A43-976E-99BBD01ADAB4"));
            var silverCount = staticMember.Count(a => a.MemberTypeId == Guid.Parse("37FB42FF-F7D7-4952-8C3F-B10E72445A67"));
            var dashboard = new MembershipDashboard
            {
                TotalMember = staticMember.Count,
                Bronze = bronzeCount,
                Silver = silverCount,
                Gold = goldCount,
                NewMember = newMemberCount,
            };
            return dashboard;
        }

        public async Task<CategoryDashboard> GetCategoriesDashboard()
        {
            var staticCategory = await _unitOfWork.CategoryRepository.GetListAsync();
            var staticProduct = await _unitOfWork.ProductRepository.GetListAsync();
            var ringCount = staticProduct.Count(a => a.CategoryId == Guid.Parse("B2C3D4E5-6789-1011-1213-141516171819"));
            var braceletCount = staticProduct.Count(a => a.CategoryId == Guid.Parse("C3D4E5F6-7890-1011-1213-141516171819"));
            var necklaceCount = staticProduct.Count(a => a.CategoryId == Guid.Parse("A1B2C3D4-5678-9101-1121-314151617181"));
            var earringCount = staticProduct.Count(a => a.CategoryId == Guid.Parse("B31FB460-A2E2-4FDC-9413-4AD6FCBD2FEB"));
            var dashboard = new CategoryDashboard
            {
                TotalCategory = staticCategory.Count,
                TotalRing = ringCount,
                TotalBracelet = braceletCount,
                TotalNecklace = necklaceCount,
                TotalEarring = earringCount,
            };
            return dashboard;
        }

        public async Task<List<MonthlyOrderCountDto>> GetMonthlyOrderCountsAsync()
        {
            var orders = await _unitOfWork.OrderRepository.GetListAsync();
            var monthlyOrderCounts = orders
                   .Where(o => o.InsDate.HasValue)
                   .GroupBy(o => new { Year = o.InsDate.Value.Year, Month = o.InsDate.Value.Month })
                   .Select(g => new MonthlyOrderCountDto
                   {
                       Year = g.Key.Year,
                       Month = g.Key.Month,
                       OrderCount = g.Count()
                   })
                   .OrderBy(m => m.Year).ThenBy(m => m.Month)
                   .ToList();


            return monthlyOrderCounts;
        }
    }
}
