using System;
using System.Data;
using Restaurant_Ordering_and_Management_System.Models;

namespace Restaurant_Ordering_and_Management_System.Interfaces
{
    public interface IReportService
    {
        /// <summary>Total revenue, completed-order count and average order value.</summary>
        SalesSummary GetSalesSummary(DateTime startDate, DateTime endDate);

        // The four report grids are tabular read-only results, so they are
        // returned as DataTables and bound straight to the report grid.
        DataTable GetDailySales(DateTime startDate, DateTime endDate);
        DataTable GetOrderHistory(DateTime startDate, DateTime endDate);
        DataTable GetInventoryStatus();
        DataTable GetStaffPerformance(DateTime startDate, DateTime endDate);
    }
}
