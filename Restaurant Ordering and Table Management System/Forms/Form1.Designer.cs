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
            this.lblTableStatus = new System.Windows.Forms.Label();
            this.dgvTableStatus = new System.Windows.Forms.DataGridView();
            this.lblRecentOrders = new System.Windows.Forms.Label();
            this.dgvRecentOrders = new System.Windows.Forms.DataGridView();
            this.pnlQuickStats = new System.Windows.Forms.Panel();
            this.lblPendingOrdersLabel = new System.Windows.Forms.Label();
            this.lblPendingOrdersValue = new System.Windows.Forms.Label();
            this.lblActiveTablesLabel = new System.Windows.Forms.Label();
            this.lblActiveTablesValue = new System.Windows.Forms.Label();
            this.lblTotalRevenueLabel = new System.Windows.Forms.Label();
            this.lblTotalRevenueValue = new System.Windows.Forms.Label();
            this.lblTotalOrdersLabel = new System.Windows.Forms.Label();
            this.lblTotalOrdersValue = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.pnlHeader.SuspendLayout();
            this.pnlSidebar.SuspendLayout();
            this.pnlMainContent.SuspendLayout();
            this.pnlDashboard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentOrders)).BeginInit();
            this.pnlQuickStats.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(35)))), ((int)(((byte)(126)))));
            this.pnlHeader.Controls.Add(this.lblCurrentTime);
            this.pnlHeader.Controls.Add(this.lblUser);
            this.pnlHeader.Controls.Add(this.lblRestaurantName);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(20);
            this.pnlHeader.Size = new System.Drawing.Size(1200, 80);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblCurrentTime
            // 
            this.lblCurrentTime.AutoSize = true;
            this.lblCurrentTime.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCurrentTime.ForeColor = System.Drawing.Color.White;
            this.lblCurrentTime.Location = new System.Drawing.Point(850, 45);
            this.lblCurrentTime.Name = "lblCurrentTime";
            this.lblCurrentTime.Size = new System.Drawing.Size(260, 19);
            this.lblCurrentTime.TabIndex = 2;
            this.lblCurrentTime.Text = "📅 Date and Time: 00/00/0000 00:00:00";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblUser.ForeColor = System.Drawing.Color.White;
            this.lblUser.Location = new System.Drawing.Point(850, 20);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(157, 19);
            this.lblUser.TabIndex = 1;
            this.lblUser.Text = "👤 Logged in as: Admin";
            // 
            // lblRestaurantName
            // 
            this.lblRestaurantName.AutoSize = true;
            this.lblRestaurantName.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold);
            this.lblRestaurantName.ForeColor = System.Drawing.Color.White;
            this.lblRestaurantName.Location = new System.Drawing.Point(20, 15);
            this.lblRestaurantName.Name = "lblRestaurantName";
            this.lblRestaurantName.Size = new System.Drawing.Size(441, 45);
            this.lblRestaurantName.TabIndex = 0;
            this.lblRestaurantName.Text = "🍽️ Restaurant Management";
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(107)))), ((int)(((byte)(53)))));
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
            this.pnlSidebar.Size = new System.Drawing.Size(200, 594);
            this.pnlSidebar.TabIndex = 1;
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLogout.ForeColor = System.Drawing.Color.White;
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
            this.btnSettings.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(107)))), ((int)(((byte)(53)))));
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
            this.btnTableManagement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(107)))), ((int)(((byte)(53)))));
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
            this.btnReports.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(107)))), ((int)(((byte)(53)))));
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
            this.btnStaff.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(107)))), ((int)(((byte)(53)))));
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
            this.btnInventory.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(107)))), ((int)(((byte)(53)))));
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
            this.btnNewOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(107)))), ((int)(((byte)(53)))));
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
            this.pnlMainContent.Controls.Add(this.pnlQuickStats);
            this.pnlMainContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMainContent.Location = new System.Drawing.Point(200, 80);
            this.pnlMainContent.Name = "pnlMainContent";
            this.pnlMainContent.Size = new System.Drawing.Size(1000, 594);
            this.pnlMainContent.TabIndex = 2;
            // 
            // pnlDashboard
            // 
            this.pnlDashboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.pnlDashboard.Controls.Add(this.lblTableStatus);
            this.pnlDashboard.Controls.Add(this.dgvTableStatus);
            this.pnlDashboard.Controls.Add(this.lblRecentOrders);
            this.pnlDashboard.Controls.Add(this.dgvRecentOrders);
            this.pnlDashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDashboard.Location = new System.Drawing.Point(0, 140);
            this.pnlDashboard.Name = "pnlDashboard";
            this.pnlDashboard.Padding = new System.Windows.Forms.Padding(15);
            this.pnlDashboard.Size = new System.Drawing.Size(1000, 454);
            this.pnlDashboard.TabIndex = 1;
            // 
            // lblTableStatus
            // 
            this.lblTableStatus.AutoSize = true;
            this.lblTableStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTableStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(35)))), ((int)(((byte)(126)))));
            this.lblTableStatus.Location = new System.Drawing.Point(15, 15);
            this.lblTableStatus.Name = "lblTableStatus";
            this.lblTableStatus.Size = new System.Drawing.Size(129, 21);
            this.lblTableStatus.TabIndex = 0;
            this.lblTableStatus.Text = "🪑 Table Status";
            // 
            // dgvTableStatus
            // 
            this.dgvTableStatus.AllowUserToAddRows = false;
            this.dgvTableStatus.AllowUserToDeleteRows = false;
            this.dgvTableStatus.BackgroundColor = System.Drawing.Color.White;
            this.dgvTableStatus.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTableStatus.Location = new System.Drawing.Point(15, 40);
            this.dgvTableStatus.Name = "dgvTableStatus";
            this.dgvTableStatus.ReadOnly = true;
            this.dgvTableStatus.Size = new System.Drawing.Size(970, 180);
            this.dgvTableStatus.TabIndex = 1;
            // 
            // lblRecentOrders
            // 
            this.lblRecentOrders.AutoSize = true;
            this.lblRecentOrders.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblRecentOrders.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(35)))), ((int)(((byte)(126)))));
            this.lblRecentOrders.Location = new System.Drawing.Point(15, 230);
            this.lblRecentOrders.Name = "lblRecentOrders";
            this.lblRecentOrders.Size = new System.Drawing.Size(143, 21);
            this.lblRecentOrders.TabIndex = 2;
            this.lblRecentOrders.Text = "📝 Recent Orders";
            // 
            // dgvRecentOrders
            // 
            this.dgvRecentOrders.AllowUserToAddRows = false;
            this.dgvRecentOrders.AllowUserToDeleteRows = false;
            this.dgvRecentOrders.BackgroundColor = System.Drawing.Color.White;
            this.dgvRecentOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecentOrders.Location = new System.Drawing.Point(15, 255);
            this.dgvRecentOrders.Name = "dgvRecentOrders";
            this.dgvRecentOrders.ReadOnly = true;
            this.dgvRecentOrders.Size = new System.Drawing.Size(970, 190);
            this.dgvRecentOrders.TabIndex = 3;
            // 
            // pnlQuickStats
            // 
            this.pnlQuickStats.BackColor = System.Drawing.Color.White;
            this.pnlQuickStats.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlQuickStats.Controls.Add(this.lblPendingOrdersLabel);
            this.pnlQuickStats.Controls.Add(this.lblPendingOrdersValue);
            this.pnlQuickStats.Controls.Add(this.lblActiveTablesLabel);
            this.pnlQuickStats.Controls.Add(this.lblActiveTablesValue);
            this.pnlQuickStats.Controls.Add(this.lblTotalRevenueLabel);
            this.pnlQuickStats.Controls.Add(this.lblTotalRevenueValue);
            this.pnlQuickStats.Controls.Add(this.lblTotalOrdersLabel);
            this.pnlQuickStats.Controls.Add(this.lblTotalOrdersValue);
            this.pnlQuickStats.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlQuickStats.Location = new System.Drawing.Point(0, 0);
            this.pnlQuickStats.Name = "pnlQuickStats";
            this.pnlQuickStats.Padding = new System.Windows.Forms.Padding(15);
            this.pnlQuickStats.Size = new System.Drawing.Size(1000, 140);
            this.pnlQuickStats.TabIndex = 0;
            // 
            // lblPendingOrdersLabel
            // 
            this.lblPendingOrdersLabel.AutoSize = true;
            this.lblPendingOrdersLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPendingOrdersLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblPendingOrdersLabel.Location = new System.Drawing.Point(780, 75);
            this.lblPendingOrdersLabel.Name = "lblPendingOrdersLabel";
            this.lblPendingOrdersLabel.Size = new System.Drawing.Size(104, 19);
            this.lblPendingOrdersLabel.TabIndex = 7;
            this.lblPendingOrdersLabel.Text = "Pending Orders";
            // 
            // lblPendingOrdersValue
            // 
            this.lblPendingOrdersValue.AutoSize = true;
            this.lblPendingOrdersValue.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblPendingOrdersValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.lblPendingOrdersValue.Location = new System.Drawing.Point(780, 20);
            this.lblPendingOrdersValue.Name = "lblPendingOrdersValue";
            this.lblPendingOrdersValue.Size = new System.Drawing.Size(44, 51);
            this.lblPendingOrdersValue.TabIndex = 6;
            this.lblPendingOrdersValue.Text = "0";
            // 
            // lblActiveTablesLabel
            // 
            this.lblActiveTablesLabel.AutoSize = true;
            this.lblActiveTablesLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblActiveTablesLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblActiveTablesLabel.Location = new System.Drawing.Point(530, 75);
            this.lblActiveTablesLabel.Name = "lblActiveTablesLabel";
            this.lblActiveTablesLabel.Size = new System.Drawing.Size(86, 19);
            this.lblActiveTablesLabel.TabIndex = 5;
            this.lblActiveTablesLabel.Text = "Active Tables";
            // 
            // lblActiveTablesValue
            // 
            this.lblActiveTablesValue.AutoSize = true;
            this.lblActiveTablesValue.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblActiveTablesValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(162)))), ((int)(((byte)(184)))));
            this.lblActiveTablesValue.Location = new System.Drawing.Point(530, 20);
            this.lblActiveTablesValue.Name = "lblActiveTablesValue";
            this.lblActiveTablesValue.Size = new System.Drawing.Size(44, 51);
            this.lblActiveTablesValue.TabIndex = 4;
            this.lblActiveTablesValue.Text = "0";
            // 
            // lblTotalRevenueLabel
            // 
            this.lblTotalRevenueLabel.AutoSize = true;
            this.lblTotalRevenueLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTotalRevenueLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblTotalRevenueLabel.Location = new System.Drawing.Point(280, 75);
            this.lblTotalRevenueLabel.Name = "lblTotalRevenueLabel";
            this.lblTotalRevenueLabel.Size = new System.Drawing.Size(94, 19);
            this.lblTotalRevenueLabel.TabIndex = 3;
            this.lblTotalRevenueLabel.Text = "Total Revenue";
            // 
            // lblTotalRevenueValue
            // 
            this.lblTotalRevenueValue.AutoSize = true;
            this.lblTotalRevenueValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTotalRevenueValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.lblTotalRevenueValue.Location = new System.Drawing.Point(280, 30);
            this.lblTotalRevenueValue.Name = "lblTotalRevenueValue";
            this.lblTotalRevenueValue.Size = new System.Drawing.Size(89, 37);
            this.lblTotalRevenueValue.TabIndex = 2;
            this.lblTotalRevenueValue.Text = "₱0.00";
            // 
            // lblTotalOrdersLabel
            // 
            this.lblTotalOrdersLabel.AutoSize = true;
            this.lblTotalOrdersLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTotalOrdersLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.lblTotalOrdersLabel.Location = new System.Drawing.Point(30, 75);
            this.lblTotalOrdersLabel.Name = "lblTotalOrdersLabel";
            this.lblTotalOrdersLabel.Size = new System.Drawing.Size(84, 19);
            this.lblTotalOrdersLabel.TabIndex = 1;
            this.lblTotalOrdersLabel.Text = "Total Orders";
            // 
            // lblTotalOrdersValue
            // 
            this.lblTotalOrdersValue.AutoSize = true;
            this.lblTotalOrdersValue.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblTotalOrdersValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(107)))), ((int)(((byte)(53)))));
            this.lblTotalOrdersValue.Location = new System.Drawing.Point(30, 20);
            this.lblTotalOrdersValue.Name = "lblTotalOrdersValue";
            this.lblTotalOrdersValue.Size = new System.Drawing.Size(44, 51);
            this.lblTotalOrdersValue.TabIndex = 0;
            this.lblTotalOrdersValue.Text = "0";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(35)))), ((int)(((byte)(126)))));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 674);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1200, 22);
            this.statusStrip1.TabIndex = 3;
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.ForeColor = System.Drawing.Color.White;
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel.Text = "Ready";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(1200, 696);
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
            this.pnlDashboard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentOrders)).EndInit();
            this.pnlQuickStats.ResumeLayout(false);
            this.pnlQuickStats.PerformLayout();
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
        private System.Windows.Forms.Panel pnlQuickStats;
        private System.Windows.Forms.Label lblTotalOrdersValue;
        private System.Windows.Forms.Label lblTotalOrdersLabel;
        private System.Windows.Forms.Label lblTotalRevenueValue;
        private System.Windows.Forms.Label lblTotalRevenueLabel;
        private System.Windows.Forms.Label lblActiveTablesValue;
        private System.Windows.Forms.Label lblActiveTablesLabel;
        private System.Windows.Forms.Label lblPendingOrdersValue;
        private System.Windows.Forms.Label lblPendingOrdersLabel;
        private System.Windows.Forms.Panel pnlDashboard;
        private System.Windows.Forms.DataGridView dgvRecentOrders;
        private System.Windows.Forms.DataGridView dgvTableStatus;
        private System.Windows.Forms.Label lblRecentOrders;
        private System.Windows.Forms.Label lblTableStatus;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
    }
}
