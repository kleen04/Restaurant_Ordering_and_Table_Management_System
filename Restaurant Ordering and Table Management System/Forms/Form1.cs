using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Restaurant_Ordering_and_Management_System.DBContext;
using Restaurant_Ordering_and_Management_System.Helper;
using Restaurant_Ordering_and_Management_System.Models;
using Restaurant_Ordering_and_Management_System.Service;

namespace Restaurant_Ordering_and_Management_System.Forms
{
    public partial class Form1 : Form
    {
        private TableService _tableService;
        private OrderService _orderService;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateDateTime();
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += (s, ev) => UpdateDateTime();
            timer.Start();

            try
            {
                // Initialize services
                DatabaseConnection dbConnection = new DatabaseConnection();
                DbHelper dbHelper = new DbHelper(dbConnection);
                _tableService = new TableService(dbHelper);
                _orderService = new OrderService(dbHelper);

                InitializeSampleData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading dashboard data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeSampleData()
        {
            dgvTableStatus.Columns.Add("TableID", "Table ID");
            dgvTableStatus.Columns.Add("Status", "Status");
            dgvTableStatus.Columns.Add("Guests", "Guests");
            dgvTableStatus.Columns.Add("Order", "Current Order");

            dgvRecentOrders.Columns.Add("OrderID", "Order ID");
            dgvRecentOrders.Columns.Add("Table", "Table");
            dgvRecentOrders.Columns.Add("OrderTime", "Order Time");
            dgvRecentOrders.Columns.Add("Status", "Status");
            dgvRecentOrders.Columns.Add("Total", "Total");

            try
            {
                // Load real table data
                List<RestaurantTable> tables = _tableService.GetAllTables();
                foreach (RestaurantTable table in tables)
                {
                    dgvTableStatus.Rows.Add(
                        table.TableId,
                        table.Status.ToString(),
                        table.CurrentGuests,
                        table.Status == TableStatus.Occupied ? $"Order #{table.TableId * 100}" : "None"
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading table status: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                // Load real recent order data (last 5 orders)
                List<Order> orders = _orderService.GetRecentOrders(5);
                foreach (Order order in orders)
                {
                    dgvRecentOrders.Rows.Add(
                        order.OrderId,
                        order.TableId,
                        order.OrderTime.ToString("h:mm tt"),
                        order.Status.ToString(),
                        $"₱0.00" // TODO: Calculate total from OrderItems
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading recent orders: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            dgvTableStatus.AutoResizeColumns();
            dgvRecentOrders.AutoResizeColumns();
        }

        private void UpdateDateTime()
        {
            lblCurrentTime.Text = $"📅 Date and Time: {DateTime.Now:MM/dd/yyyy HH:mm:ss}";
        }

        private void BtnNewOrder_Click(object sender, EventArgs e)
        {
            FormAddOrder formAddOrder = new FormAddOrder();
            formAddOrder.ShowDialog();

            this.Show();
        }

        private void BtnInventory_Click(object sender, EventArgs e)
        {
            this.Hide();

            InventoryForm formInventory = new InventoryForm();
            formInventory.ShowDialog();

            this.Show();
        }

        private void BtnStaff_Click(object sender, EventArgs e)
        {
            this.Hide();

            FormStaff formStaff = new FormStaff();
            formStaff.ShowDialog();

            this.Show();
        }

        private void BtnReports_Click(object sender, EventArgs e)
        {
            this.Hide();

            FormReports formReports = new FormReports();
            formReports.ShowDialog();

            this.Show();
        }

        private void BtnTableManagement_Click(object sender, EventArgs e)
        {
            this.Hide();

            FormTables formTables = new FormTables();
            formTables.ShowDialog();

            this.Show();
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Opening Settings Form...", "Settings", MessageBoxButtons.OK, MessageBoxIcon.Information);
            toolStripStatusLabel.Text = "Settings Form opened";
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
    }
}
