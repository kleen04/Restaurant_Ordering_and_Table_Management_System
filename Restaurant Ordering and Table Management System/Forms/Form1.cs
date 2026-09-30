using Restaurant_Ordering_and_Management_System.Helper;
using Restaurant_Ordering_and_Management_System.Interfaces;
using Restaurant_Ordering_and_Management_System.Models;
using Restaurant_Ordering_and_Management_System.Service;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Restaurant_Ordering_and_Management_System.Forms
{
    public partial class Form1 : Form
    {
        private readonly AppServices _services;
        private readonly ITableService _tableService;
        private readonly IOrderService _orderService;

        private ToolStripMenuItem _markInProgressItem;
        private ToolStripMenuItem _markCompletedItem;
        private ToolStripMenuItem _cancelOrderItem;

        public Form1(AppServices services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
            _tableService = services.Tables;
            _orderService = services.Orders;

            InitializeComponent();
            BuildOrderStatusMenu();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateDateTime();
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += (s, ev) => UpdateDateTime();
            timer.Start();

            toolStripStatusLabel.Text = "Tip: right-click an order under Recent Orders to update its status.";
            LoadDashboard();
        }

        // ---------------------------------------------------------------
        // Dashboard data (live from the database)
        // ---------------------------------------------------------------
        private void LoadDashboard()
        {
            dgvTableStatus.Columns.Clear();
            dgvTableStatus.Columns.Add("TableID", "Table ID");
            dgvTableStatus.Columns.Add("Status", "Status");
            dgvTableStatus.Columns.Add("Guests", "Guests");
            dgvTableStatus.Columns.Add("Order", "Current Order");

            dgvRecentOrders.Columns.Clear();
            dgvRecentOrders.Columns.Add("OrderID", "Order ID");
            dgvRecentOrders.Columns.Add("Table", "Table");
            dgvRecentOrders.Columns.Add("OrderTime", "Order Time");
            dgvRecentOrders.Columns.Add("Status", "Status");
            dgvRecentOrders.Columns.Add("Total", "Total");

            try
            {
                List<RestaurantTable> tables = _tableService.GetAllTables();
                List<Order> orders = _orderService.GetRecentOrders(50);

                foreach (RestaurantTable table in tables)
                {
                    Order openOrder = orders.Find(o => o.TableId == table.TableId && IsOpen(o.Status));
                    string currentOrder = table.Status == TableStatus.Occupied && openOrder != null
                        ? "Order #" + openOrder.OrderId
                        : "None";

                    dgvTableStatus.Rows.Add(table.TableId, table.Status.ToString(), table.CurrentGuests, currentOrder);
                }

                int shown = 0;
                foreach (Order order in orders)
                {
                    if (shown++ >= 5)
                    {
                        break;
                    }

                    int rowIndex = dgvRecentOrders.Rows.Add(
                        order.OrderId,
                        order.TableId,
                        order.OrderTime.ToString("h:mm tt"),
                        StatusText(order.Status),
                        "₱" + order.Total.ToString("N2"));
                    dgvRecentOrders.Rows[rowIndex].Tag = order;
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Error loading dashboard data: " + ex.Message, "Database Error");
            }

            dgvTableStatus.AutoResizeColumns();
            dgvRecentOrders.AutoResizeColumns();
        }

        private static bool IsOpen(OrderStatus status)
        {
            return status == OrderStatus.Pending || status == OrderStatus.InProgress;
        }

        private static string StatusText(OrderStatus status)
        {
            return status == OrderStatus.InProgress ? "In Progress" : status.ToString();
        }

        private void UpdateDateTime()
        {
            lblCurrentTime.Text = $"📅 Date and Time: {DateTime.Now:MM/dd/yyyy HH:mm:ss}";
        }

        // ---------------------------------------------------------------
        // Order status: Pending -> In Progress -> Completed / Cancelled
        // (right-click an order in the Recent Orders grid)
        // ---------------------------------------------------------------
        private void BuildOrderStatusMenu()
        {
            ContextMenuStrip menu = new ContextMenuStrip();

            _markInProgressItem = new ToolStripMenuItem("Mark as In Progress");
            _markCompletedItem = new ToolStripMenuItem("Mark as Completed");
            _cancelOrderItem = new ToolStripMenuItem("Cancel Order");

            _markInProgressItem.Click += (s, e) => ChangeSelectedOrderStatus(OrderStatus.InProgress);
            _markCompletedItem.Click += (s, e) => ChangeSelectedOrderStatus(OrderStatus.Completed);
            _cancelOrderItem.Click += (s, e) => ChangeSelectedOrderStatus(OrderStatus.Cancelled);

            menu.Items.AddRange(new ToolStripItem[] { _markInProgressItem, _markCompletedItem, _cancelOrderItem });
            menu.Opening += OrderMenu_Opening;

            dgvRecentOrders.ContextMenuStrip = menu;
            dgvRecentOrders.CellMouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
                {
                    dgvRecentOrders.ClearSelection();
                    dgvRecentOrders.Rows[e.RowIndex].Selected = true;
                }
            };
        }

        private Order GetSelectedOrder()
        {
            if (dgvRecentOrders.SelectedRows.Count == 0)
            {
                return null;
            }
            return dgvRecentOrders.SelectedRows[0].Tag as Order;
        }

        private void OrderMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Order order = GetSelectedOrder();
            if (order == null || !IsOpen(order.Status))
            {
                e.Cancel = true;
                return;
            }

            _markInProgressItem.Enabled = order.Status == OrderStatus.Pending;
            _markCompletedItem.Enabled = true;
            _cancelOrderItem.Enabled = true;
        }

        private void ChangeSelectedOrderStatus(OrderStatus newStatus)
        {
            Order order = GetSelectedOrder();
            if (order == null)
            {
                return;
            }

            if (newStatus == OrderStatus.Cancelled &&
                !MessageHelper.Confirm("Cancel order #" + order.OrderId + "? This cannot be undone.", "Cancel Order"))
            {
                return;
            }

            try
            {
                _orderService.UpdateOrderStatus(order.OrderId, newStatus);
                LoadDashboard();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Could not update the order: " + ex.Message, "Order Status");
            }
        }

        // ---------------------------------------------------------------
        // Navigation: each child form gets only the services it needs
        // ---------------------------------------------------------------
        private void BtnNewOrder_Click(object sender, EventArgs e)
        {
            using (FormAddOrder form = new FormAddOrder(_services.Menu, _services.Tables, _services.Staff, _services.Orders))
            {
                form.ShowDialog();
            }

            this.Show();
            LoadDashboard();
        }

        private void BtnInventory_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (InventoryForm form = new InventoryForm(_services.Inventory))
            {
                form.ShowDialog();
            }
            this.Show();
        }

        private void BtnStaff_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (FormStaff form = new FormStaff(_services.Staff))
            {
                form.ShowDialog();
            }
            this.Show();
            LoadDashboard();
        }

        private void BtnReports_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (FormReports form = new FormReports(_services.Reports))
            {
                form.ShowDialog();
            }
            this.Show();
        }

        private void BtnTableManagement_Click(object sender, EventArgs e)
        {
            this.Hide();
            using (FormTables form = new FormTables(_services.Tables))
            {
                form.ShowDialog();
            }
            this.Show();
            LoadDashboard();
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            MessageHelper.ShowInfo("Settings are not available yet.", "Settings");
            toolStripStatusLabel.Text = "Settings is planned for a later phase";
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            if (MessageHelper.Confirm("Are you sure you want to logout?", "Logout"))
            {
                Application.Exit();
            }
        }
    }
}
