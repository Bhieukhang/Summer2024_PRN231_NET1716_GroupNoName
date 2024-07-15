using JSS_BusinessObjects.Models;
using JSS_BusinessObjects.Payload.Request;

namespace JSS_Services.Interface
{
    public interface IDashboardService
    {
        Task<DashboardRequest> GetDashboardsAsync(int year);
        Task<AccountDashboard> GetDashboardAccount();
    }
}
