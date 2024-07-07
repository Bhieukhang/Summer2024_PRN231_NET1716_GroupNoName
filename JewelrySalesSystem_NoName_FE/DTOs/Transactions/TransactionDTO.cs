namespace JewelrySalesSystem_NoName_FE.DTOs.Transactions
{
    public class TransactionDTO
    {
        public Guid Id { get; set; }

        public Guid OrderId { get; set; }

        public string? Description { get; set; }

        public string? Currency { get; set; }

        public double? TotalPrice { get; set; }
        public DateTime? InsDate { get; set; }

        public virtual Order Order { get; set; } = null!;
    }

    public class TransactionOrderDTO
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public string Description { get; set; }
        public string Currency { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime InsDate { get; set; }
        public OrderTransactionDTO Order { get; set; }
    }

    public class OrderTransactionDTO
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public string Type { get; set; }
        public DateTime InsDate { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal MaterialProcessPrice { get; set; }
        public Guid Discount { get; set; }
        public Guid Promotion { get; set; }
    }
}
