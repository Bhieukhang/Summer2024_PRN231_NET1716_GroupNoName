namespace JewelrySalesSystem_NoName_FE.DTOs
{
    public class DashboardDTO
    {
        public int TotalPromotion { get; set; } = 0;

        public decimal TotalRevenue { get; set; } = 0;

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
