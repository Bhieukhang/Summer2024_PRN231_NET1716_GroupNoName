﻿namespace JewelrySalesSystem_NoName_FE.DTOs
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

    public class MembershipDashboard
    {
        public int TotalMember { get; set; }
        public int NewMember { get; set; }
        public int Gold { get; set; }
        public int Bronze { get; set; }
        public int Silver { get; set; }
    }
}
