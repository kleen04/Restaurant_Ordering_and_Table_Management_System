using System;
using System.Data;
using MySql.Data.MySqlClient;
using Restaurant_Ordering_and_Management_System.Helper;
using Restaurant_Ordering_and_Management_System.Interfaces;

namespace Restaurant_Ordering_and_Management_System.Service
{
    // ============================================================
    // OWNER: Inventory & Reports module member — backs FormReports.
    // Stored procedures: sp_Report_GetSalesByDateRange,
    // sp_Report_GetTotalRevenue (see Database/stored_procedures.sql).
    // Both methods are left as TODOs since FormReports has no controls
    // wired up yet — add date pickers there first, then call these.
    // ============================================================
    public class ReportService : IReportService
    {
        private readonly DbHelper _dbHelper;

        public ReportService(DbHelper dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
        }

        public DataTable GetSalesReport(DateTime startDate, DateTime endDate)
        {
            // TODO(Reports owner): call sp_Report_GetSalesByDateRange with
            // @StartDate, @EndDate and return the DataTable directly — it's
            // meant to be bound straight to a DataGridView.
            throw new NotImplementedException();
        }

        public decimal GetTotalRevenue(DateTime startDate, DateTime endDate)
        {
            // TODO(Reports owner): call sp_Report_GetTotalRevenue with
            // @StartDate, @EndDate via _dbHelper.ExecuteScalar(...) and
            // Convert.ToDecimal the result (handle DBNull -> 0m when there
            // are no orders in range).
            throw new NotImplementedException();
        }
    }
}
