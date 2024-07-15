namespace JSS_BusinessObjects.Payload.Request
{
    public class DashboardRequest
    {
        public int TotalPromotion { get; set; }
        
        public decimal TotalRevenue { get; set; }

        public List<MonthlyRevenue> MonthlyRevenue { get; set; } = new();
    }

    public class MonthlyRevenue
    {
        public int Month { get; set; }
        public double Value { get; set; }
    }

    public class AccountDashboard
    {
        public int TotalAccount { get; set; }
        public int TotalAdmin { get; set; }
        public int TotalManager { get; set; }
        public int TotalStaff { get; set; }
        public int TotalCustomer { get; set; }
    }
}
