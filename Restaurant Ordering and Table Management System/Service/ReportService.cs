using System;
using System.Data;
using MySql.Data.MySqlClient;
using Restaurant_Ordering_and_Management_System.Helper;
using Restaurant_Ordering_and_Management_System.Interfaces;
using Restaurant_Ordering_and_Management_System.Models;

namespace Restaurant_Ordering_and_Management_System.Service
{
    // ============================================================
    // OWNER: Inventory & Reports module member — backs FormReports.
    // Stored procedures: sp_Report_GetSummary, sp_Report_GetDailySales,
    // sp_Report_GetOrderHistory, sp_Report_GetInventoryStatus,
    // sp_Report_GetStaffPerformance (see Database/stored_procedures.sql).
    // ============================================================
    public class ReportService : IReportService
    {
        private readonly DbHelper _dbHelper;

        public ReportService(DbHelper dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
        }

        public SalesSummary GetSalesSummary(DateTime startDate, DateTime endDate)
        {
            DataTable table = _dbHelper.ExecuteQuery("sp_Report_GetSummary", DateRange(startDate, endDate));

            if (table.Rows.Count == 0)
            {
                return new SalesSummary();
            }

            DataRow row = table.Rows[0];
            return new SalesSummary
            {
                TotalOrders = Convert.ToInt32(row["TotalOrders"]),
                TotalRevenue = Convert.ToDecimal(row["TotalRevenue"]),
                AverageOrderValue = Convert.ToDecimal(row["AvgOrderValue"])
            };
        }

        public DataTable GetDailySales(DateTime startDate, DateTime endDate)
        {
            return _dbHelper.ExecuteQuery("sp_Report_GetDailySales", DateRange(startDate, endDate));
        }

        public DataTable GetOrderHistory(DateTime startDate, DateTime endDate)
        {
            return _dbHelper.ExecuteQuery("sp_Report_GetOrderHistory", DateRange(startDate, endDate));
        }

        public DataTable GetInventoryStatus()
        {
            return _dbHelper.ExecuteQuery("sp_Report_GetInventoryStatus");
        }

        public DataTable GetStaffPerformance(DateTime startDate, DateTime endDate)
        {
            return _dbHelper.ExecuteQuery("sp_Report_GetStaffPerformance", DateRange(startDate, endDate));
        }

        // One place that turns the two picked dates into an inclusive range, so the
        // end date's orders (up to 23:59:59) are included in every report.
        private static MySqlParameter[] DateRange(DateTime startDate, DateTime endDate)
        {
            return new[]
            {
                new MySqlParameter("@p_StartDate", startDate.Date),
                new MySqlParameter("@p_EndDate", endDate.Date.AddDays(1).AddSeconds(-1))
            };
        }
    }
}
