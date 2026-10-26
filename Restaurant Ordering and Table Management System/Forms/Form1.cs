using Restaurant_Ordering_and_Management_System.Helper;
using Restaurant_Ordering_and_Management_System.Interfaces;
using Restaurant_Ordering_and_Management_System.Models;
using Restaurant_Ordering_and_Management_System.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Restaurant_Ordering_and_Management_System.Forms
{
    public partial class Form1 : Form
    {
        private readonly AppServices _services;
        private readonly ITableService _tableService;
        private readonly IOrderService _orderService;
        private readonly IReportService _reportService;

        // Newest open order per table, rebuilt on every refresh; feeds the caption under each tile.
        private Dictionary<int, Order> _openOrderByTable = new Dictionary<int, Order>();

        // The Recent Orders list shows this many rows, open orders first.
        private const int RecentOrderRows = 6;

        // The dashboard refreshes itself this often so it can be left open on a spare screen.
        private const int AutoRefreshSeconds = 30;

        private System.Windows.Forms.Timer _clockTimer;
        private System.Windows.Forms.Timer _refreshTimer;
        private ContextMenuStrip _orderMenu;

        private ToolStripMenuItem _markInProgressItem;
        private ToolStripMenuItem _markCompletedItem;
        private ToolStripMenuItem _cancelOrderItem;

        public Form1(AppServices services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
            _tableService = services.Tables;
            _orderService = services.Orders;
            _reportService = services.Reports;

            InitializeComponent();
            flpDashTables.CaptionProvider = GetTableCaption;
            ApplyStatCardColors();
            BuildOrderStatusMenu();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateDateTime();
            _clockTimer = StartTimer(1000, (s, ev) => UpdateDateTime());
            _refreshTimer = StartTimer(AutoRefreshSeconds * 1000, RefreshTimer_Tick);

            toolStripStatusLabel.Text = "Tip: right-click an order under Recent Orders to update its status.";
            LoadDashboard();
        }

        private static System.Windows.Forms.Timer StartTimer(int intervalMilliseconds, EventHandler onTick)
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer { Interval = intervalMilliseconds };
            timer.Tick += onTick;
            timer.Start();
            return timer;
        }

        // Skips a beat instead of interrupting: while another screen is open on top
        // (this form is hidden or disabled by a dialog) or while the right-click menu is showing.
        private void RefreshTimer_Tick(object sender, EventArgs e)
        {
            if (!Visible || !Enabled || _orderMenu.Visible)
            {
                return;
            }

            LoadDashboard(false);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            if (_clockTimer != null) _clockTimer.Dispose();
            if (_refreshTimer != null) _refreshTimer.Dispose();
            base.OnFormClosed(e);
        }

        // ---------------------------------------------------------------
        // Dashboard data (live from the database)
        // ---------------------------------------------------------------
        // showErrors is false for the background refresh, so a dropped connection updates the
        // status bar quietly instead of popping up a message box every 30 seconds.
        private void LoadDashboard(bool showErrors = true)
        {
            try
            {
                List<RestaurantTable> tables = _tableService.GetAllTables();
                List<Order> orders = _orderService.GetRecentOrders(50);

                _openOrderByTable = BuildOpenOrderLookup(orders);
                flpDashTables.SetTables(tables);
                flpTableLegend.SetTables(tables);

                dgvRecentOrders.SetOrders(PickOrdersToShow(orders, RecentOrderRows));

                SalesSummary salesToday = _reportService.GetSalesSummary(DateTime.Today, DateTime.Today);
                ShowSummary(DashboardSummary.Build(tables, orders, salesToday));

                toolStripLastUpdated.Text = "Updated " + DateTime.Now.ToString("h:mm:ss tt");
            }
            catch (Exception ex)
            {
                toolStripLastUpdated.Text = "Update failed at " + DateTime.Now.ToString("h:mm:ss tt");
                if (showErrors)
                {
                    MessageHelper.ShowError("Error loading dashboard data: " + ex.Message, "Database Error");
                }
            }
        }

        // The cards use the same colours as the tiles and chips they summarise.
        private void ApplyStatCardColors()
        {
            cardCompletedToday.AccentColor = OrderStatusStyle.GetColor(OrderStatus.Completed);
            cardTablesOccupied.AccentColor = TableStatusStyle.GetColor(TableStatus.Occupied);
            cardOpenOrders.AccentColor = OrderStatusStyle.GetColor(OrderStatus.Pending);
        }

        private void ShowSummary(DashboardSummary summary)
        {
            cardCompletedToday.Value = summary.CompletedToday.ToString("N0");
            cardRevenueToday.Value = "₱" + summary.RevenueToday.ToString("N2");
            cardTablesOccupied.Value = summary.OccupiedTables + " / " + summary.TotalTables;
            cardOpenOrders.Value = summary.OpenOrders.ToString("N0");
        }

        // Open orders first (they need attention), then the newest finished ones.
        private static IEnumerable<Order> PickOrdersToShow(IEnumerable<Order> orders, int count)
        {
            return orders.OrderBy(o => o.IsOpen ? 0 : 1)
                         .ThenByDescending(o => o.OrderTime)
                         .ThenByDescending(o => o.OrderId)
                         .Take(count);
        }

        // Orders arrive newest first, so the first open order seen for a table is its current one.
        private static Dictionary<int, Order> BuildOpenOrderLookup(IEnumerable<Order> orders)
        {
            Dictionary<int, Order> lookup = new Dictionary<int, Order>();
            foreach (Order order in orders)
            {
                if (order.IsOpen && !lookup.ContainsKey(order.TableId))
                {
                    lookup.Add(order.TableId, order);
                }
            }
            return lookup;
        }

        private string GetTableCaption(RestaurantTable table)
        {
            Order openOrder;
            if (table.Status == TableStatus.Occupied && _openOrderByTable.TryGetValue(table.TableId, out openOrder))
            {
                return "Order #" + openOrder.OrderId;
            }
            return null;
        }

        // A free table starts a new order for that table; any other table just reports its state.
        private void FlpDashTables_TableClicked(object sender, TableEventArgs e)
        {
            if (e.Table.Status == TableStatus.Available)
            {
                OpenNewOrder(e.Table.TableId);
                return;
            }

            string caption = GetTableCaption(e.Table);
            toolStripStatusLabel.Text = "Table " + e.Table.TableId + " is " + e.Table.Status.ToString().ToLower()
                + " (" + e.Table.CurrentGuests + " of " + e.Table.Capacity + " seated)"
                + (caption == null ? "" : " - " + caption);
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
            _orderMenu = menu;

            _markInProgressItem = new ToolStripMenuItem("Mark as In Progress");
            _markCompletedItem = new ToolStripMenuItem("Mark as Completed");
            _cancelOrderItem = new ToolStripMenuItem("Cancel Order");

            _markInProgressItem.Click += (s, e) => ChangeSelectedOrderStatus(OrderStatus.InProgress);
            _markCompletedItem.Click += (s, e) => ChangeSelectedOrderStatus(OrderStatus.Completed);
            _cancelOrderItem.Click += (s, e) => ChangeSelectedOrderStatus(OrderStatus.Cancelled);

            menu.Items.AddRange(new ToolStripItem[] { _markInProgressItem, _markCompletedItem, _cancelOrderItem });
            menu.Opening += OrderMenu_Opening;

            dgvRecentOrders.ContextMenuStrip = menu;
        }

        private void OrderMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Order order = dgvRecentOrders.SelectedOrder;
            if (order == null || !order.IsOpen)
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
            Order order = dgvRecentOrders.SelectedOrder;
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
            OpenNewOrder(null);
        }

        // Shared by the sidebar button and clicking a free table on the dashboard.
        private void OpenNewOrder(int? preselectedTableId)
        {
            using (FormAddOrder form = new FormAddOrder(_services.Menu, _services.Tables, _services.Staff,
                                                        _services.Orders, preselectedTableId))
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
