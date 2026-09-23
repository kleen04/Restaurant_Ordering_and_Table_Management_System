using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_Ordering_and_Management_System.Forms
{
    public partial class FormReports : Form
    {
        public FormReports()
        {
            InitializeComponent();
        }

        private void FormReports_Load(object sender, EventArgs e)
        {
            dtpFrom.Value = DateTime.Today.AddDays(-7);
            dtpTo.Value = DateTime.Today;
            cmbReportType.SelectedIndex = 0;
            LoadSalesSummary();
        }

        private void BtnGenerate_Click(object sender, EventArgs e)
        {
            switch (cmbReportType.SelectedItem?.ToString())
            {
                case "Order History":
                    LoadOrderHistory();
                    break;
                case "Inventory Status":
                    LoadInventoryStatus();
                    break;
                case "Staff Performance":
                    LoadStaffPerformance();
                    break;
                default:
                    LoadSalesSummary();
                    break;
            }
        }

        private void LoadSalesSummary()
        {
            dgvReport.Columns.Clear();
            dgvReport.Rows.Clear();
            dgvReport.Columns.Add("Date", "Date");
            dgvReport.Columns.Add("Orders", "Orders");
            dgvReport.Columns.Add("Revenue", "Revenue");
            dgvReport.Rows.Add("2026-09-17", "24", "₱18,450.00");
            dgvReport.Rows.Add("2026-09-18", "31", "₱22,900.00");
            dgvReport.Rows.Add("2026-09-19", "28", "₱20,150.00");
            dgvReport.Rows.Add("2026-09-20", "35", "₱26,700.00");
            dgvReport.Rows.Add("2026-09-21", "19", "₱14,300.00");
            dgvReport.AutoResizeColumns();

            lblTotalOrdersValue.Text = "137";
            lblTotalRevenueValue.Text = "₱102,500.00";
            lblAvgOrderValue.Text = "₱748.18";
        }

        private void LoadOrderHistory()
        {
            dgvReport.Columns.Clear();
            dgvReport.Rows.Clear();
            dgvReport.Columns.Add("OrderId", "Order #");
            dgvReport.Columns.Add("Table", "Table");
            dgvReport.Columns.Add("Time", "Time");
            dgvReport.Columns.Add("Status", "Status");
            dgvReport.Columns.Add("Total", "Total");
            dgvReport.Rows.Add("1042", "5", "12:15 PM", "Completed", "₱845.00");
            dgvReport.Rows.Add("1043", "2", "12:40 PM", "Completed", "₱620.00");
            dgvReport.Rows.Add("1044", "4", "1:05 PM", "In Progress", "₱1,120.00");
            dgvReport.AutoResizeColumns();
        }

        private void LoadInventoryStatus()
        {
            dgvReport.Columns.Clear();
            dgvReport.Rows.Clear();
            dgvReport.Columns.Add("ItemName", "Item Name");
            dgvReport.Columns.Add("Quantity", "Quantity");
            dgvReport.Columns.Add("Status", "Status");
            dgvReport.Rows.Add("Soy Sauce", "8 bottles", "Low Stock");
            dgvReport.Rows.Add("Salt", "3 kg", "Low Stock");
            dgvReport.Rows.Add("Rice", "100 kg", "In Stock");
            dgvReport.AutoResizeColumns();
        }

        private void LoadStaffPerformance()
        {
            dgvReport.Columns.Clear();
            dgvReport.Rows.Clear();
            dgvReport.Columns.Add("StaffName", "Staff Name");
            dgvReport.Columns.Add("OrdersHandled", "Orders Handled");
            dgvReport.Columns.Add("HoursWorked", "Hours Worked");
            dgvReport.Rows.Add("Juan Dela Cruz", "58", "40");
            dgvReport.Rows.Add("Pedro Bautista", "44", "32");
            dgvReport.AutoResizeColumns();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Export to CSV functionality to be implemented", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Print functionality to be implemented", "Print", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
