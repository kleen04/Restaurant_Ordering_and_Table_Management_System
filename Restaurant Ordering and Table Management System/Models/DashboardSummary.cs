using System;
using System.Collections.Generic;

namespace Restaurant_Ordering_and_Management_System.Models
{
    /// <summary>The four headline numbers on the dashboard, worked out from data it already has.</summary>
    public class DashboardSummary
    {
        public int CompletedToday { get; private set; }
        public decimal RevenueToday { get; private set; }
        public int OccupiedTables { get; private set; }
        public int TotalTables { get; private set; }
        public int OpenOrders { get; private set; }

        /// <param name="salesToday">Today's completed-order figures (from the report service).</param>
        /// <param name="recentOrders">The recent orders list; open orders are always among the most recent.</param>
        public static DashboardSummary Build(IEnumerable<RestaurantTable> tables, IEnumerable<Order> recentOrders, SalesSummary salesToday)
        {
            if (tables == null) throw new ArgumentNullException(nameof(tables));
            if (recentOrders == null) throw new ArgumentNullException(nameof(recentOrders));
            if (salesToday == null) throw new ArgumentNullException(nameof(salesToday));

            DashboardSummary summary = new DashboardSummary
            {
                CompletedToday = salesToday.TotalOrders,
                RevenueToday = salesToday.TotalRevenue
            };

            foreach (RestaurantTable table in tables)
            {
                summary.TotalTables++;
                if (table.Status == TableStatus.Occupied)
                {
                    summary.OccupiedTables++;
                }
            }

            foreach (Order order in recentOrders)
            {
                if (order.IsOpen)
                {
                    summary.OpenOrders++;
                }
            }

            return summary;
        }
    }
}
