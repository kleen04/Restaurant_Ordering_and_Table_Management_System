using System;
using System.Data;
using System.Windows.Forms;
using Restaurant_Ordering_and_Management_System.Helper;
using Restaurant_Ordering_and_Management_System.Interfaces;
using Restaurant_Ordering_and_Management_System.Models;

namespace Restaurant_Ordering_and_Management_System.Forms
{
    public partial class FormReports : Form
    {
        private readonly IReportService _reportService;

        // Remembered so Export / Print can name the report that is currently shown.
        private string _currentReportTitle = "Report";

        public FormReports(IReportService reportService)
        {
            _reportService = reportService ?? throw new ArgumentNullException(nameof(reportService));
            InitializeComponent();
        }

        private void FormReports_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Today.AddDays(-7);
            dtpTo.Value = DateTime.Today;
            cmbReportType.SelectedIndex = 0;

            // The headline figures count Completed orders only.
            lblTotalOrdersLabel.Text = "Completed Orders";

            GenerateReport();
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            GenerateReport();
        }

        private void GenerateReport()
        {
            DateTime from = dtpFrom.Value.Date;
            DateTime to = dtpTo.Value.Date;

            if (from > to)
            {
                MessageHelper.ShowWarning("The start date must be on or before the end date.", "Reports");
                return;
            }

            string reportType = cmbReportType.SelectedItem == null ? "Sales Summary" : cmbReportType.SelectedItem.ToString();
            string range = from.ToString("yyyy-MM-dd") + " to " + to.ToString("yyyy-MM-dd");

            try
            {
                ShowSummary(_reportService.GetSalesSummary(from, to));

                switch (reportType)
                {
                    case "Order History":
                        ShowReport(_reportService.GetOrderHistory(from, to),
                            new[] { "Order #", "Table", "Served By", "Order Time", "Status", "Total" },
                            "OrderTotal");
                        break;

                    case "Inventory Status":
                        ShowReport(_reportService.GetInventoryStatus(),
                            new[] { "Item Name", "Category", "Quantity", "Unit", "Reorder Level", "Status" });
                        range = "as of " + DateTime.Today.ToString("yyyy-MM-dd");
                        break;

                    case "Staff Performance":
                        ShowReport(_reportService.GetStaffPerformance(from, to),
                            new[] { "Staff Name", "Position", "Orders Handled", "Completed Sales" },
                            "CompletedSales");
                        break;

                    default:
                        reportType = "Sales Summary";
                        ShowReport(_reportService.GetDailySales(from, to),
                            new[] { "Date", "Orders", "Revenue" },
                            "Revenue");
                        break;
                }

                _currentReportTitle = reportType + " (" + range + ")";
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Could not generate the report: " + ex.Message, "Reports");
            }
        }

        private void ShowSummary(SalesSummary summary)
        {
            lblTotalOrdersValue.Text = summary.TotalOrders.ToString("N0");
            lblTotalRevenueValue.Text = "₱" + summary.TotalRevenue.ToString("N2");
            lblAvgOrderValue.Text = "₱" + summary.AverageOrderValue.ToString("N2");
        }

        // Binds a report table to the grid, renames the headers and formats money/dates.
        private void ShowReport(DataTable data, string[] headers, params string[] currencyColumns)
        {
            dgvReport.DataSource = null;
            dgvReport.Columns.Clear();
            dgvReport.AutoGenerateColumns = true;
            dgvReport.DataSource = data;

            for (int i = 0; i < headers.Length && i < dgvReport.Columns.Count; i++)
            {
                dgvReport.Columns[i].HeaderText = headers[i];
            }

            foreach (string name in currencyColumns)
            {
                if (dgvReport.Columns.Contains(name))
                {
                    dgvReport.Columns[name].DefaultCellStyle.Format = "₱#,##0.00";
                }
            }

            if (dgvReport.Columns.Contains("SaleDate"))
            {
                dgvReport.Columns["SaleDate"].DefaultCellStyle.Format = "yyyy-MM-dd";
            }
            if (dgvReport.Columns.Contains("OrderTime"))
            {
                dgvReport.Columns["OrderTime"].DefaultCellStyle.Format = "yyyy-MM-dd h:mm tt";
            }

            dgvReport.AutoResizeColumns();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            string fileName = _currentReportTitle.Replace(" ", "_").Replace("(", "").Replace(")", "").Replace(":", "") + ".csv";
            ReportExporter.ExportToCsv(dgvReport, fileName);
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            ReportExporter.Print(dgvReport, _currentReportTitle);
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
