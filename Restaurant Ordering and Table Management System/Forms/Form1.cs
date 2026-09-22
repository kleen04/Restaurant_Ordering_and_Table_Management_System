using System;
using System.Windows.Forms;

namespace Restaurant_Ordering_and_Management_System.Forms
{
    public partial class Form1 : Form
    {
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

            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            dgvTableStatus.Columns.Add("TableID", "Table ID");
            dgvTableStatus.Columns.Add("Status", "Status");
            dgvTableStatus.Columns.Add("Guests", "Guests");
            dgvTableStatus.Columns.Add("Order", "Current Order");

            dgvTableStatus.Rows.Add("1", "Available", "0", "None");
            dgvTableStatus.Rows.Add("2", "Occupied", "4", "Order #102");
            dgvTableStatus.Rows.Add("3", "Available", "0", "None");
            dgvTableStatus.Rows.Add("4", "Occupied", "2", "Order #105");

            dgvRecentOrders.Columns.Add("OrderID", "Order ID");
            dgvRecentOrders.Columns.Add("Table", "Table");
            dgvRecentOrders.Columns.Add("OrderTime", "Order Time");
            dgvRecentOrders.Columns.Add("Status", "Status");
            dgvRecentOrders.Columns.Add("Total", "Total");

            dgvRecentOrders.Rows.Add("101", "1", "2:45 PM", "Completed", "₱850.00");
            dgvRecentOrders.Rows.Add("102", "2", "3:15 PM", "In Progress", "₱1,200.00");
            dgvRecentOrders.Rows.Add("103", "3", "3:30 PM", "Pending", "₱650.00");
            dgvRecentOrders.Rows.Add("104", "4", "3:45 PM", "Completed", "₱920.00");
            dgvRecentOrders.Rows.Add("105", "4", "4:00 PM", "In Progress", "₱1,100.00");

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
