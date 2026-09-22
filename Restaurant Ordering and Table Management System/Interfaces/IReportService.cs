using System;
using System.Data;

namespace Restaurant_Ordering_and_Management_System.Interfaces
{
    public interface IReportService
    {
        DataTable GetSalesReport(DateTime startDate, DateTime endDate);
        decimal GetTotalRevenue(DateTime startDate, DateTime endDate);
    }
}
