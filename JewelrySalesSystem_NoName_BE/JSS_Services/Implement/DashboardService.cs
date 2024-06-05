using Azure.Messaging;
using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using JSS_BusinessObjects;
using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Request;
using JSS_DataAccessObjects;
using JSS_Repositories;
using JSS_Services.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Net;
using static JSS_BusinessObjects.AppConstant;

namespace JSS_Services.Implement
{
    public class DashboardService : BaseService<DashboardService>, IDashboardService
    {
        public DashboardService(IUnitOfWork<JewelrySalesSystemContext> unitOfWork,
            ILogger<DashboardService> logger) : base(unitOfWork, logger)
        {
        }

        public async Task<IEnumerable<DashboardRequest>> GetAllDashboardsAsync()
        {
            
            var promotions = await _unitOfWork.GetRepository<Promotion>().GetListAsync();
            var orders = await _unitOfWork.GetRepository<Order>().GetListAsync();
            int totalPromotion = promotions == null?0:promotions.Count;
            decimal totalRevenue = orders == null?0: decimal.Parse(orders.Sum(p => p.TotalPrice).ToString()??"0");
            var dash =  new DashboardRequest
            {
                TotalPromotion = totalPromotion,
                TotalRevenue = totalRevenue
            };
            return (IEnumerable<DashboardRequest>)dash;
        }

        
        
    }
}
