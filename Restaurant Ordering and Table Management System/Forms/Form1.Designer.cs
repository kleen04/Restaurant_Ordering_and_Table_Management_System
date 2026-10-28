namespace Restaurant_Ordering_and_Management_System.Forms
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblCurrentTime = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblRestaurantName = new System.Windows.Forms.Label();
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnTableManagement = new System.Windows.Forms.Button();
            this.btnReports = new System.Windows.Forms.Button();
            this.btnStaff = new System.Windows.Forms.Button();
            this.btnInventory = new System.Windows.Forms.Button();
            this.btnNewOrder = new System.Windows.Forms.Button();
            this.pnlMainContent = new System.Windows.Forms.Panel();
            this.pnlDashboard = new System.Windows.Forms.Panel();
            this.tlpStats = new System.Windows.Forms.TableLayoutPanel();
            this.cardCompletedToday = new Restaurant_Ordering_and_Management_System.Forms.StatCard();
            this.cardRevenueToday = new Restaurant_Ordering_and_Management_System.Forms.StatCard();
            this.cardTablesOccupied = new Restaurant_Ordering_and_Management_System.Forms.StatCard();
            this.cardOpenOrders = new Restaurant_Ordering_and_Management_System.Forms.StatCard();
            this.pnlTableCard = new FilletedPanel();
            this.flpDashTables = new Restaurant_Ordering_and_Management_System.Forms.TableFloorPanel();
            this.flpTableLegend = new Restaurant_Ordering_and_Management_System.Forms.TableStatusLegend();
            this.lblTableStatus = new System.Windows.Forms.Label();
            this.pnlOrdersCard = new FilletedPanel();
            this.dgvRecentOrders = new Restaurant_Ordering_and_Management_System.Forms.RecentOrdersGrid();
            this.lblRecentOrders = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripLastUpdated = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlHeader.SuspendLayout();
            this.pnlSidebar.SuspendLayout();
            this.pnlMainContent.SuspendLayout();
            this.pnlDashboard.SuspendLayout();
            this.tlpStats.SuspendLayout();
            this.pnlTableCard.SuspendLayout();
            this.pnlOrdersCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentOrders)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.Orange;
            this.pnlHeader.Controls.Add(this.lblCurrentTime);
            this.pnlHeader.Controls.Add(this.lblUser);
            this.pnlHeader.Controls.Add(this.lblRestaurantName);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(20);
            this.pnlHeader.Size = new System.Drawing.Size(1130, 80);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblCurrentTime
            // 
            this.lblCurrentTime.AutoSize = true;
            this.lblCurrentTime.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCurrentTime.ForeColor = System.Drawing.Color.White;
            this.lblCurrentTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCurrentTime.Location = new System.Drawing.Point(850, 45);
            this.lblCurrentTime.Name = "lblCurrentTime";
            this.lblCurrentTime.Size = new System.Drawing.Size(312, 23);
            this.lblCurrentTime.TabIndex = 2;
            this.lblCurrentTime.Text = "📅 Date and Time: 00/00/0000 12:00:00 AM";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblUser.ForeColor = System.Drawing.Color.White;
            this.lblUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUser.Location = new System.Drawing.Point(850, 20);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(194, 23);
            this.lblUser.TabIndex = 1;
            this.lblUser.Text = "👤 Logged in as: User";
            // 
            // lblRestaurantName
            // 
            this.lblRestaurantName.AutoSize = true;
            this.lblRestaurantName.Font = new System.Drawing.Font("Pristina", 24F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRestaurantName.ForeColor = System.Drawing.Color.White;
            this.lblRestaurantName.Location = new System.Drawing.Point(20, 15);
            this.lblRestaurantName.Name = "lblRestaurantName";
            this.lblRestaurantName.Size = new System.Drawing.Size(202, 52);
            this.lblRestaurantName.TabIndex = 0;
            this.lblRestaurantName.Text = "Tagum-Eats";
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.Maroon;
            this.pnlSidebar.Controls.Add(this.btnLogout);
            this.pnlSidebar.Controls.Add(this.btnSettings);
            this.pnlSidebar.Controls.Add(this.btnTableManagement);
            this.pnlSidebar.Controls.Add(this.btnReports);
            this.pnlSidebar.Controls.Add(this.btnStaff);
            this.pnlSidebar.Controls.Add(this.btnInventory);
            this.pnlSidebar.Controls.Add(this.btnNewOrder);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSidebar.Location = new System.Drawing.Point(0, 80);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.pnlSidebar.Size = new System.Drawing.Size(200, 590);
            this.pnlSidebar.TabIndex = 1;
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnLogout.Location = new System.Drawing.Point(10, 536);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(180, 45);
            this.btnLogout.TabIndex = 6;
            this.btnLogout.Text = "🚪 Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.BtnLogout_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.Color.Maroon;
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnSettings.ForeColor = System.Drawing.Color.White;
            this.btnSettings.Location = new System.Drawing.Point(10, 265);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(180, 45);
            this.btnSettings.TabIndex = 5;
            this.btnSettings.Text = "⚙️ Settings";
            this.btnSettings.UseVisualStyleBackColor = false;
            this.btnSettings.Click += new System.EventHandler(this.BtnSettings_Click);
            // 
            // btnTableManagement
            // 
            this.btnTableManagement.BackColor = System.Drawing.Color.Maroon;
            this.btnTableManagement.FlatAppearance.BorderSize = 0;
            this.btnTableManagement.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTableManagement.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnTableManagement.ForeColor = System.Drawing.Color.White;
            this.btnTableManagement.Location = new System.Drawing.Point(10, 215);
            this.btnTableManagement.Name = "btnTableManagement";
            this.btnTableManagement.Size = new System.Drawing.Size(180, 45);
            this.btnTableManagement.TabIndex = 4;
            this.btnTableManagement.Text = "🪑 Tables";
            this.btnTableManagement.UseVisualStyleBackColor = false;
            this.btnTableManagement.Click += new System.EventHandler(this.BtnTableManagement_Click);
            // 
            // btnReports
            // 
            this.btnReports.BackColor = System.Drawing.Color.Maroon;
            this.btnReports.FlatAppearance.BorderSize = 0;
            this.btnReports.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReports.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnReports.ForeColor = System.Drawing.Color.White;
            this.btnReports.Location = new System.Drawing.Point(10, 165);
            this.btnReports.Name = "btnReports";
            this.btnReports.Size = new System.Drawing.Size(180, 45);
            this.btnReports.TabIndex = 3;
            this.btnReports.Text = "📊 Reports";
            this.btnReports.UseVisualStyleBackColor = false;
            this.btnReports.Click += new System.EventHandler(this.BtnReports_Click);
            // 
            // btnStaff
            // 
            this.btnStaff.BackColor = System.Drawing.Color.Maroon;
            this.btnStaff.FlatAppearance.BorderSize = 0;
            this.btnStaff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStaff.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnStaff.ForeColor = System.Drawing.Color.White;
            this.btnStaff.Location = new System.Drawing.Point(10, 115);
            this.btnStaff.Name = "btnStaff";
            this.btnStaff.Size = new System.Drawing.Size(180, 45);
            this.btnStaff.TabIndex = 2;
            this.btnStaff.Text = "👥 Staff";
            this.btnStaff.UseVisualStyleBackColor = false;
            this.btnStaff.Click += new System.EventHandler(this.BtnStaff_Click);
            // 
            // btnInventory
            // 
            this.btnInventory.BackColor = System.Drawing.Color.Maroon;
            this.btnInventory.FlatAppearance.BorderSize = 0;
            this.btnInventory.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnInventory.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnInventory.ForeColor = System.Drawing.Color.White;
            this.btnInventory.Location = new System.Drawing.Point(10, 65);
            this.btnInventory.Name = "btnInventory";
            this.btnInventory.Size = new System.Drawing.Size(180, 45);
            this.btnInventory.TabIndex = 1;
            this.btnInventory.Text = "📦 Inventory";
            this.btnInventory.UseVisualStyleBackColor = false;
            this.btnInventory.Click += new System.EventHandler(this.BtnInventory_Click);
            // 
            // btnNewOrder
            // 
            this.btnNewOrder.BackColor = System.Drawing.Color.Maroon;
            this.btnNewOrder.FlatAppearance.BorderSize = 0;
            this.btnNewOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNewOrder.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnNewOrder.ForeColor = System.Drawing.Color.White;
            this.btnNewOrder.Location = new System.Drawing.Point(10, 15);
            this.btnNewOrder.Name = "btnNewOrder";
            this.btnNewOrder.Size = new System.Drawing.Size(180, 45);
            this.btnNewOrder.TabIndex = 0;
            this.btnNewOrder.Text = "📝 New Order";
            this.btnNewOrder.UseVisualStyleBackColor = false;
            this.btnNewOrder.Click += new System.EventHandler(this.BtnNewOrder_Click);
            // 
            // pnlMainContent
            // 
            this.pnlMainContent.AutoScroll = true;
            this.pnlMainContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.pnlMainContent.Controls.Add(this.pnlDashboard);
            this.pnlMainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainContent.Location = new System.Drawing.Point(200, 80);
            this.pnlMainContent.Name = "pnlMainContent";
            this.pnlMainContent.Size = new System.Drawing.Size(930, 590);
            this.pnlMainContent.TabIndex = 2;
            // 
            // pnlDashboard
            // 
            this.pnlDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.pnlDashboard.BackgroundImage = global::Restaurant_Ordering_and_Management_System.Properties.Resources.home;
            this.pnlDashboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pnlDashboard.Controls.Add(this.tlpStats);
            this.pnlDashboard.Controls.Add(this.pnlTableCard);
            this.pnlDashboard.Controls.Add(this.pnlOrdersCard);
            this.pnlDashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDashboard.Location = new System.Drawing.Point(0, 0);
            this.pnlDashboard.Name = "pnlDashboard";
            this.pnlDashboard.Padding = new System.Windows.Forms.Padding(15);
            this.pnlDashboard.Size = new System.Drawing.Size(930, 590);
            this.pnlDashboard.TabIndex = 1;
            // 
            // tlpStats
            // 
            this.tlpStats.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tlpStats.BackColor = System.Drawing.Color.Transparent;
            this.tlpStats.ColumnCount = 4;
            this.tlpStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpStats.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tlpStats.Controls.Add(this.cardCompletedToday, 0, 0);
            this.tlpStats.Controls.Add(this.cardRevenueToday, 1, 0);
            this.tlpStats.Controls.Add(this.cardTablesOccupied, 2, 0);
            this.tlpStats.Controls.Add(this.cardOpenOrders, 3, 0);
            this.tlpStats.Location = new System.Drawing.Point(10, 15);
            this.tlpStats.Name = "tlpStats";
            this.tlpStats.RowCount = 1;
            this.tlpStats.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpStats.Size = new System.Drawing.Size(910, 74);
            this.tlpStats.TabIndex = 0;
            // 
            // cardCompletedToday
            // 
            this.cardCompletedToday.AccentColor = System.Drawing.Color.Green;
            this.cardCompletedToday.BackColor = System.Drawing.Color.White;
            this.cardCompletedToday.Caption = "Completed Today";
            this.cardCompletedToday.CornerRadius = 14;
            this.cardCompletedToday.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardCompletedToday.Location = new System.Drawing.Point(5, 0);
            this.cardCompletedToday.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.cardCompletedToday.Name = "cardCompletedToday";
            this.cardCompletedToday.Size = new System.Drawing.Size(217, 74);
            this.cardCompletedToday.TabIndex = 0;
            // 
            // cardRevenueToday
            // 
            this.cardRevenueToday.BackColor = System.Drawing.Color.White;
            this.cardRevenueToday.Caption = "Revenue Today";
            this.cardRevenueToday.CornerRadius = 14;
            this.cardRevenueToday.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardRevenueToday.Location = new System.Drawing.Point(232, 0);
            this.cardRevenueToday.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.cardRevenueToday.Name = "cardRevenueToday";
            this.cardRevenueToday.Size = new System.Drawing.Size(217, 74);
            this.cardRevenueToday.TabIndex = 1;
            this.cardRevenueToday.Value = "₱0.00";
            // 
            // cardTablesOccupied
            // 
            this.cardTablesOccupied.AccentColor = System.Drawing.Color.Firebrick;
            this.cardTablesOccupied.BackColor = System.Drawing.Color.White;
            this.cardTablesOccupied.Caption = "Tables Occupied";
            this.cardTablesOccupied.CornerRadius = 14;
            this.cardTablesOccupied.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardTablesOccupied.Location = new System.Drawing.Point(459, 0);
            this.cardTablesOccupied.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.cardTablesOccupied.Name = "cardTablesOccupied";
            this.cardTablesOccupied.Size = new System.Drawing.Size(217, 74);
            this.cardTablesOccupied.TabIndex = 2;
            this.cardTablesOccupied.Value = "0 / 0";
            // 
            // cardOpenOrders
            // 
            this.cardOpenOrders.AccentColor = System.Drawing.Color.Goldenrod;
            this.cardOpenOrders.BackColor = System.Drawing.Color.White;
            this.cardOpenOrders.Caption = "Open Orders";
            this.cardOpenOrders.CornerRadius = 14;
            this.cardOpenOrders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.cardOpenOrders.Location = new System.Drawing.Point(686, 0);
            this.cardOpenOrders.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.cardOpenOrders.Name = "cardOpenOrders";
            this.cardOpenOrders.Size = new System.Drawing.Size(219, 74);
            this.cardOpenOrders.TabIndex = 3;
            // 
            // pnlTableCard
            // 
            this.pnlTableCard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTableCard.BackColor = System.Drawing.Color.White;
            this.pnlTableCard.Controls.Add(this.flpDashTables);
            this.pnlTableCard.Controls.Add(this.flpTableLegend);
            this.pnlTableCard.Controls.Add(this.lblTableStatus);
            this.pnlTableCard.CornerRadius = 14;
            this.pnlTableCard.Location = new System.Drawing.Point(15, 100);
            this.pnlTableCard.Name = "pnlTableCard";
            this.pnlTableCard.Size = new System.Drawing.Size(900, 252);
            this.pnlTableCard.TabIndex = 0;
            // 
            // flpDashTables
            // 
            this.flpDashTables.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpDashTables.AutoScroll = true;
            this.flpDashTables.BackColor = System.Drawing.Color.White;
            this.flpDashTables.Location = new System.Drawing.Point(10, 42);
            this.flpDashTables.Name = "flpDashTables";
            this.flpDashTables.SelectionEnabled = false;
            this.flpDashTables.Size = new System.Drawing.Size(880, 206);
            this.flpDashTables.TabIndex = 2;
            this.flpDashTables.TileScale = 0.65F;
            this.flpDashTables.TileSpacing = 2;
            this.flpDashTables.TableClicked += new System.EventHandler<Restaurant_Ordering_and_Management_System.Forms.TableEventArgs>(this.FlpDashTables_TableClicked);
            // 
            // flpTableLegend
            // 
            this.flpTableLegend.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flpTableLegend.BackColor = System.Drawing.Color.White;
            this.flpTableLegend.Hint = "Click a free table to start an order";
            this.flpTableLegend.Location = new System.Drawing.Point(170, 12);
            this.flpTableLegend.Name = "flpTableLegend";
            this.flpTableLegend.Size = new System.Drawing.Size(716, 26);
            this.flpTableLegend.TabIndex = 1;
            this.flpTableLegend.WrapContents = false;
            // 
            // lblTableStatus
            // 
            this.lblTableStatus.AutoSize = true;
            this.lblTableStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblTableStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTableStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.lblTableStatus.Location = new System.Drawing.Point(16, 10);
            this.lblTableStatus.Name = "lblTableStatus";
            this.lblTableStatus.Size = new System.Drawing.Size(161, 28);
            this.lblTableStatus.TabIndex = 0;
            this.lblTableStatus.Text = "🪑 Table Status";
            // 
            // pnlOrdersCard
            // 
            this.pnlOrdersCard.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlOrdersCard.BackColor = System.Drawing.Color.White;
            this.pnlOrdersCard.Controls.Add(this.dgvRecentOrders);
            this.pnlOrdersCard.Controls.Add(this.lblRecentOrders);
            this.pnlOrdersCard.CornerRadius = 14;
            this.pnlOrdersCard.Location = new System.Drawing.Point(15, 362);
            this.pnlOrdersCard.Name = "pnlOrdersCard";
            this.pnlOrdersCard.Size = new System.Drawing.Size(900, 222);
            this.pnlOrdersCard.TabIndex = 1;
            // 
            // dgvRecentOrders
            // 
            this.dgvRecentOrders.AllowUserToAddRows = false;
            this.dgvRecentOrders.AllowUserToDeleteRows = false;
            this.dgvRecentOrders.AllowUserToResizeRows = false;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(248)))), ((int)(((byte)(245)))));
            dataGridViewCellStyle9.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle9.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.dgvRecentOrders.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dgvRecentOrders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRecentOrders.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRecentOrders.BackgroundColor = System.Drawing.Color.White;
            this.dgvRecentOrders.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvRecentOrders.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvRecentOrders.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle10.BackColor = System.Drawing.Color.Maroon;
            dataGridViewCellStyle10.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle10.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.SelectionBackColor = System.Drawing.Color.Maroon;
            dataGridViewCellStyle10.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle10.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvRecentOrders.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle10;
            this.dgvRecentOrders.ColumnHeadersHeight = 26;
            this.dgvRecentOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            dataGridViewCellStyle16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(227)))));
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvRecentOrders.DefaultCellStyle = dataGridViewCellStyle16;
            this.dgvRecentOrders.EnableHeadersVisualStyles = false;
            this.dgvRecentOrders.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(230)))), ((int)(((byte)(230)))));
            this.dgvRecentOrders.Location = new System.Drawing.Point(10, 40);
            this.dgvRecentOrders.MultiSelect = false;
            this.dgvRecentOrders.Name = "dgvRecentOrders";
            this.dgvRecentOrders.ReadOnly = true;
            this.dgvRecentOrders.RowHeadersVisible = false;
            this.dgvRecentOrders.RowHeadersWidth = 51;
            this.dgvRecentOrders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRecentOrders.Size = new System.Drawing.Size(880, 178);
            this.dgvRecentOrders.TabIndex = 1;
            this.dgvRecentOrders.WaitWarning = System.TimeSpan.Parse("00:15:00");
            // 
            // lblRecentOrders
            // 
            this.lblRecentOrders.AutoSize = true;
            this.lblRecentOrders.BackColor = System.Drawing.Color.Transparent;
            this.lblRecentOrders.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblRecentOrders.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.lblRecentOrders.Location = new System.Drawing.Point(16, 10);
            this.lblRecentOrders.Name = "lblRecentOrders";
            this.lblRecentOrders.Size = new System.Drawing.Size(180, 28);
            this.lblRecentOrders.TabIndex = 0;
            this.lblRecentOrders.Text = "📝 Recent Orders";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.Maroon;
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.toolStripLastUpdated});
            this.statusStrip1.Location = new System.Drawing.Point(0, 670);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1130, 26);
            this.statusStrip1.TabIndex = 3;
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(997, 20);
            this.toolStripStatusLabel.Spring = true;
            this.toolStripStatusLabel.Text = "Ready";
            this.toolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripLastUpdated
            // 
            this.toolStripLastUpdated.ForeColor = System.Drawing.Color.White;
            this.toolStripLastUpdated.Name = "toolStripLastUpdated";
            this.toolStripLastUpdated.Size = new System.Drawing.Size(118, 20);
            this.toolStripLastUpdated.Text = "Not updated yet";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(1130, 696);
            this.MinimumSize = new System.Drawing.Size(1000, 650);
            this.Controls.Add(this.pnlMainContent);
            this.Controls.Add(this.pnlSidebar);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.statusStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Restaurant Ordering and Table Management System";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlSidebar.ResumeLayout(false);
            this.pnlMainContent.ResumeLayout(false);
            this.pnlDashboard.ResumeLayout(false);
            this.tlpStats.ResumeLayout(false);
            this.pnlTableCard.ResumeLayout(false);
            this.pnlTableCard.PerformLayout();
            this.pnlOrdersCard.ResumeLayout(false);
            this.pnlOrdersCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentOrders)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblRestaurantName;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblCurrentTime;
        private System.Windows.Forms.Panel pnlSidebar;
        private System.Windows.Forms.Button btnNewOrder;
        private System.Windows.Forms.Button btnInventory;
        private System.Windows.Forms.Button btnStaff;
        private System.Windows.Forms.Button btnReports;
        private System.Windows.Forms.Button btnTableManagement;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel pnlMainContent;
        private System.Windows.Forms.Panel pnlDashboard;
        private Restaurant_Ordering_and_Management_System.Forms.RecentOrdersGrid dgvRecentOrders;
        private FilletedPanel pnlOrdersCard;
        private System.Windows.Forms.TableLayoutPanel tlpStats;
        private Restaurant_Ordering_and_Management_System.Forms.StatCard cardCompletedToday;
        private Restaurant_Ordering_and_Management_System.Forms.StatCard cardRevenueToday;
        private Restaurant_Ordering_and_Management_System.Forms.StatCard cardTablesOccupied;
        private Restaurant_Ordering_and_Management_System.Forms.StatCard cardOpenOrders;
        private FilletedPanel pnlTableCard;
        private Restaurant_Ordering_and_Management_System.Forms.TableStatusLegend flpTableLegend;
        private Restaurant_Ordering_and_Management_System.Forms.TableFloorPanel flpDashTables;
        private System.Windows.Forms.Label lblRecentOrders;
        private System.Windows.Forms.Label lblTableStatus;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripLastUpdated;
    }
}
