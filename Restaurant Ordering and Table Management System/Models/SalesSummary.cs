namespace Restaurant_Ordering_and_Management_System.Models
{
    /// <summary>Headline figures for a date range (Completed orders only).</summary>
    public class SalesSummary
    {
        public int TotalOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal AverageOrderValue { get; set; }
    }
}
