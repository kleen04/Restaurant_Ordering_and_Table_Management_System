using Restaurant_Ordering_and_Management_System.Helper;
using Restaurant_Ordering_and_Management_System.Interfaces;
using Restaurant_Ordering_and_Management_System.Models;
using Restaurant_Ordering_and_Management_System.Service;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Restaurant_Ordering_and_Management_System.Forms
{
    public partial class Form1 : Form
    {
        private readonly AppServices _services;
        private readonly string _loggedInUsername;
        private ProgramPreferences _preferences;
        private readonly ITableService _tableService;
        private readonly IOrderService _orderService;
        private readonly IReportService _reportService;

        // Newest open order per table, rebuilt on every refresh; feeds the caption under each tile.
        private Dictionary<int, Order> _openOrderByTable = new Dictionary<int, Order>();

        // The Recent Orders list shows this many rows, open orders first.
        private const int RecentOrderRows = 6;

        private System.Windows.Forms.Timer _clockTimer;
        private System.Windows.Forms.Timer _refreshTimer;
        private ContextMenuStrip _orderMenu;

        private ToolStripMenuItem _markInProgressItem;
        private ToolStripMenuItem _markCompletedItem;
        private ToolStripMenuItem _cancelOrderItem;

        public Form1(AppServices services, AuthenticatedUser account)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
            if (account == null) throw new ArgumentNullException(nameof(account));
            _loggedInUsername = account.Username;
            _tableService = services.Tables;
            _orderService = services.Orders;
            _reportService = services.Reports;

            InitializeComponent();
            _preferences = ProgramPreferencesStore.Load();
            WindowState = _preferences.StartMaximized ? FormWindowState.Maximized : FormWindowState.Normal;
            Text = _preferences.RestaurantName + " - Restaurant Ordering and Table Management System";
            lblUser.Text = "👤 Logged in as: " + _loggedInUsername;
            lblRestaurantName.Text = _preferences.RestaurantName;
            lblCurrentTime.Visible = _preferences.ShowDateTime;
            flpDashTables.CaptionProvider = GetTableCaption;
            pnlDashboard.Resize += PnlDashboard_Resize;
            pnlHeader.Resize += PnlHeader_Resize;
            ApplyStatCardColors();
            BuildOrderStatusMenu();
            LayoutDashboard();
            LayoutHeaderLabels();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateDateTime();
            _clockTimer = StartTimer(1000, (s, ev) => UpdateDateTime());
            _refreshTimer = StartTimer(_preferences.RefreshIntervalSeconds * 1000, RefreshTimer_Tick);

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

                toolStripLastUpdated.Text = "Updated " + DateTime.Now.ToString(_preferences.Use24HourClock ? "HH:mm:ss" : "h:mm:ss tt");
            }
            catch (Exception ex)
            {
                toolStripLastUpdated.Text = "Update failed at " + DateTime.Now.ToString(_preferences.Use24HourClock ? "HH:mm:ss" : "h:mm:ss tt");
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
            string timeFormat = _preferences.Use24HourClock ? "HH:mm:ss" : "h:mm:ss tt";
            lblCurrentTime.Text = "📅 Date and Time: " + DateTime.Now.ToString("MM/dd/yyyy " + timeFormat);
            LayoutHeaderLabels();
        }

        private void PnlDashboard_Resize(object sender, EventArgs e)
        {
            LayoutDashboard();
        }

        private void LayoutDashboard()
        {
            if (pnlDashboard == null || pnlDashboard.ClientSize.Width <= 0 || pnlDashboard.ClientSize.Height <= 0)
            {
                return;
            }

            int width = pnlDashboard.ClientSize.Width;
            int height = pnlDashboard.ClientSize.Height;
            int tableTop = tlpStats.Bottom + 11;
            int bottomPadding = 15;
            int gap = 10;
            int availableHeight = Math.Max(0, height - tableTop - bottomPadding - gap);
            int tableHeight = Math.Min(availableHeight, Math.Max(180, (int)(availableHeight * 0.48)));
            int ordersTop = tableTop + tableHeight + gap;
            int ordersHeight = Math.Max(0, height - ordersTop - bottomPadding);

            tlpStats.Location = new System.Drawing.Point(10, 15);
            tlpStats.Width = Math.Max(0, width - 20);
            pnlTableCard.Location = new System.Drawing.Point(15, tableTop);
            pnlTableCard.Size = new System.Drawing.Size(Math.Max(0, width - 30), tableHeight);
            pnlOrdersCard.Location = new System.Drawing.Point(15, ordersTop);
            pnlOrdersCard.Size = new System.Drawing.Size(Math.Max(0, width - 30), ordersHeight);
        }

        private void PnlHeader_Resize(object sender, EventArgs e)
        {
            LayoutHeaderLabels();
        }

        private void LayoutHeaderLabels()
        {
            if (pnlHeader == null || lblUser == null || lblCurrentTime == null)
            {
                return;
            }

            lblUser.Location = new System.Drawing.Point(
                Math.Max(lblRestaurantName.Right + 20, pnlHeader.ClientSize.Width - lblUser.Width - 20), 20);
            lblCurrentTime.Location = new System.Drawing.Point(
                Math.Max(lblRestaurantName.Right + 20, pnlHeader.ClientSize.Width - lblCurrentTime.Width - 20), 45);
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
            using (FormReports form = new FormReports(_services.Reports, _preferences))
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
            using (FormSettingsDialog dialog = new FormSettingsDialog(_preferences))
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    return;
                }

                try
                {
                    ProgramPreferencesStore.Save(dialog.Preferences);
                    _preferences = dialog.Preferences;
                    lblRestaurantName.Text = _preferences.RestaurantName;
                    Text = _preferences.RestaurantName + " - Restaurant Ordering and Table Management System";
                    lblCurrentTime.Visible = _preferences.ShowDateTime;
                    if (_refreshTimer != null)
                    {
                        _refreshTimer.Interval = _preferences.RefreshIntervalSeconds * 1000;
                    }
                    WindowState = _preferences.StartMaximized ? FormWindowState.Maximized : FormWindowState.Normal;
                    UpdateDateTime();
                    toolStripStatusLabel.Text = "Settings saved";
                }
                catch (Exception ex)
                {
                    MessageHelper.ShowError("Could not save settings: " + ex.Message, "Settings");
                }
            }
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            if (!_preferences.ConfirmBeforeLogout || MessageHelper.Confirm("Are you sure you want to logout?", "Logout"))
            {
                DialogResult = DialogResult.Retry;
                Close();
            }
        }
    }

    [XmlRoot("DashboardPreferences")]
    public sealed class ProgramPreferences
    {
        public string RestaurantName { get; set; } = "Tagum-Eats";
        public int RefreshIntervalSeconds { get; set; } = 30;
        public bool ShowDateTime { get; set; } = true;
        public bool Use24HourClock { get; set; }
        public bool StartMaximized { get; set; }
        public bool ConfirmBeforeLogout { get; set; } = true;
    }

    internal static class ProgramPreferencesStore
    {
        private static readonly string SettingsPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "RestaurantOrderingAndTableManagementSystem", "dashboard-settings.xml");

        public static ProgramPreferences Load()
        {
            if (!File.Exists(SettingsPath))
            {
                return new ProgramPreferences();
            }

            ProgramPreferences preferences;
            XmlSerializer serializer = new XmlSerializer(typeof(ProgramPreferences));
            using (FileStream stream = File.OpenRead(SettingsPath))
            {
                preferences = serializer.Deserialize(stream) as ProgramPreferences;
            }

            if (preferences == null)
            {
                throw new InvalidDataException("The saved program settings file is empty or invalid.");
            }

            Validate(preferences);
            return preferences;
        }

        public static void Save(ProgramPreferences preferences)
        {
            if (preferences == null) throw new ArgumentNullException(nameof(preferences));
            Validate(preferences);

            string directory = Path.GetDirectoryName(SettingsPath);
            Directory.CreateDirectory(directory);
            XmlSerializer serializer = new XmlSerializer(typeof(ProgramPreferences));
            using (FileStream stream = File.Create(SettingsPath))
            {
                serializer.Serialize(stream, preferences);
            }
        }

        private static void Validate(ProgramPreferences preferences)
        {
            if (string.IsNullOrWhiteSpace(preferences.RestaurantName) || preferences.RestaurantName.Trim().Length > 80)
            {
                throw new InvalidDataException("Restaurant name must contain 1 to 80 characters.");
            }
            if (preferences.RefreshIntervalSeconds < 10 || preferences.RefreshIntervalSeconds > 300)
            {
                throw new InvalidDataException("Dashboard refresh interval must be between 10 and 300 seconds.");
            }
        }
    }

    internal sealed class FormSettingsDialog : Form
    {
        private readonly TextBox _restaurantName;
        private readonly NumericUpDown _refreshInterval;
        private readonly CheckBox _showDateTime;
        private readonly CheckBox _use24HourClock;
        private readonly CheckBox _startMaximized;
        private readonly CheckBox _confirmBeforeLogout;

        public ProgramPreferences Preferences { get; private set; }

        public FormSettingsDialog(ProgramPreferences current)
        {
            if (current == null) throw new ArgumentNullException(nameof(current));

            Text = "Program Settings";
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MinimizeBox = false;
            MaximizeBox = false;
            ShowInTaskbar = false;
            ClientSize = new Size(560, 470);
            Font = new Font("Segoe UI", 9F);
            BackColor = AppTheme.Page;

            TableLayoutPanel layout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(24),
                ColumnCount = 2,
                RowCount = 10,
                BackColor = AppTheme.Page
            };
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 205));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 44));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 42));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 36));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            Label heading = new Label
            {
                Text = "Program Settings",
                Font = new Font("Segoe UI", 15F, FontStyle.Bold),
                ForeColor = AppTheme.TextDark,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };
            layout.Controls.Add(heading, 0, 0);
            layout.SetColumnSpan(heading, 2);

            Label generalHeading = CreateSectionLabel("General");
            layout.Controls.Add(generalHeading, 0, 1);
            layout.SetColumnSpan(generalHeading, 2);

            Label restaurantLabel = CreateFieldLabel("Restaurant name:");
            layout.Controls.Add(restaurantLabel, 0, 2);
            _restaurantName = new TextBox
            {
                Text = current.RestaurantName,
                MaxLength = 80,
                Dock = DockStyle.Fill,
                Anchor = AnchorStyles.Left | AnchorStyles.Right
            };
            layout.Controls.Add(_restaurantName, 1, 2);

            _startMaximized = CreateOptionCheckBox("Start dashboard maximized", current.StartMaximized);
            layout.Controls.Add(_startMaximized, 0, 3);
            layout.SetColumnSpan(_startMaximized, 2);
            _confirmBeforeLogout = CreateOptionCheckBox("Confirm before logging out", current.ConfirmBeforeLogout);
            layout.Controls.Add(_confirmBeforeLogout, 0, 4);
            layout.SetColumnSpan(_confirmBeforeLogout, 2);
            _use24HourClock = CreateOptionCheckBox("Use 24-hour time format", current.Use24HourClock);
            layout.Controls.Add(_use24HourClock, 0, 5);
            layout.SetColumnSpan(_use24HourClock, 2);

            Label dashboardHeading = CreateSectionLabel("Dashboard");
            layout.Controls.Add(dashboardHeading, 0, 6);
            layout.SetColumnSpan(dashboardHeading, 2);
            Label refreshLabel = CreateFieldLabel("Refresh interval (seconds):");
            layout.Controls.Add(refreshLabel, 0, 7);
            _refreshInterval = new NumericUpDown
            {
                Minimum = 10,
                Maximum = 300,
                Increment = 5,
                Value = current.RefreshIntervalSeconds,
                Width = 100,
                Anchor = AnchorStyles.Left
            };
            layout.Controls.Add(_refreshInterval, 1, 7);

            _showDateTime = new CheckBox
            {
                Text = "Show date and time in dashboard header",
                Checked = current.ShowDateTime,
                AutoSize = true,
                ForeColor = AppTheme.TextDark,
                Anchor = AnchorStyles.Left
            };
            layout.Controls.Add(_showDateTime, 0, 8);
            layout.SetColumnSpan(_showDateTime, 2);

            FlowLayoutPanel buttons = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.RightToLeft,
                WrapContents = false
            };
            Button saveButton = new Button
            {
                Text = "Save",
                DialogResult = DialogResult.None,
                BackColor = AppTheme.Accent,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Width = 100,
                Height = 34,
                Margin = new Padding(8, 4, 0, 0)
            };
            saveButton.FlatAppearance.BorderSize = 0;
            saveButton.Click += SaveButton_Click;
            Button cancelButton = new Button
            {
                Text = "Cancel",
                DialogResult = DialogResult.Cancel,
                BackColor = AppTheme.Neutral,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Width = 100,
                Height = 34,
                Margin = new Padding(8, 4, 0, 0)
            };
            cancelButton.FlatAppearance.BorderSize = 0;
            buttons.Controls.Add(saveButton);
            buttons.Controls.Add(cancelButton);
            layout.Controls.Add(buttons, 0, 9);
            layout.SetColumnSpan(buttons, 2);
            Controls.Add(layout);
            AcceptButton = saveButton;
            CancelButton = cancelButton;
        }

        private static Label CreateSectionLabel(string text)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = AppTheme.Primary,
                Anchor = AnchorStyles.Left,
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        private static CheckBox CreateOptionCheckBox(string text, bool isChecked)
        {
            return new CheckBox
            {
                Text = text,
                Checked = isChecked,
                AutoSize = true,
                ForeColor = AppTheme.TextDark,
                Anchor = AnchorStyles.Left
            };
        }

        private static Label CreateFieldLabel(string text)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                ForeColor = AppTheme.TextDark,
                Anchor = AnchorStyles.Left,
                TextAlign = ContentAlignment.MiddleLeft
            };
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            string name = _restaurantName.Text.Trim();
            if (name.Length == 0)
            {
                MessageHelper.ShowWarning("Enter a restaurant name.", "Settings");
                _restaurantName.Focus();
                return;
            }

            Preferences = new ProgramPreferences
            {
                RestaurantName = name,
                RefreshIntervalSeconds = (int)_refreshInterval.Value,
                ShowDateTime = _showDateTime.Checked,
                Use24HourClock = _use24HourClock.Checked,
                StartMaximized = _startMaximized.Checked,
                ConfirmBeforeLogout = _confirmBeforeLogout.Checked
            };
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
