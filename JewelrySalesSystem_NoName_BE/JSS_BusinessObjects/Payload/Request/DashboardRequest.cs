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
}
