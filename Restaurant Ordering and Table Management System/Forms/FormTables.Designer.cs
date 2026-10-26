namespace Restaurant_Ordering_and_Management_System.Forms
{
    partial class FormTables
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.flpTables = new Restaurant_Ordering_and_Management_System.Forms.TableFloorPanel();
            this.flpLegend = new Restaurant_Ordering_and_Management_System.Forms.TableStatusLegend();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnAddTable = new System.Windows.Forms.Button();
            this.btnEditTable = new System.Windows.Forms.Button();
            this.btnUpdateStatus = new System.Windows.Forms.Button();
            this.btnDeleteTable = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.flpLegend.SuspendLayout();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlHeader
            //
            this.pnlHeader.BackColor = global::Restaurant_Ordering_and_Management_System.Helper.AppTheme.Header;
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.pnlHeader.Size = new System.Drawing.Size(960, 64);
            this.pnlHeader.TabIndex = 0;
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(220, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🍽️ Table Management";
            //
            // lblSubtitle
            //
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSubtitle.ForeColor = global::Restaurant_Ordering_and_Management_System.Helper.AppTheme.OnPrimaryMuted;
            this.lblSubtitle.Location = new System.Drawing.Point(22, 38);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(240, 17);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Restaurant floor plan - click a table to manage it";
            //
            // pnlContent
            //
            this.pnlContent.BackColor = System.Drawing.Color.White;
            this.pnlContent.Controls.Add(this.flpTables);
            this.pnlContent.Controls.Add(this.flpLegend);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 64);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(16);
            this.pnlContent.Size = new System.Drawing.Size(960, 436);
            this.pnlContent.TabIndex = 1;
            //
            // flpLegend
            //
            this.flpLegend.BackColor = System.Drawing.Color.White;
            this.flpLegend.Dock = System.Windows.Forms.DockStyle.Top;
            this.flpLegend.Hint = "Click a table to select it  •  Double-click to update its status";
            this.flpLegend.Location = new System.Drawing.Point(16, 16);
            this.flpLegend.Name = "flpLegend";
            this.flpLegend.Padding = new System.Windows.Forms.Padding(4, 6, 4, 0);
            this.flpLegend.Size = new System.Drawing.Size(928, 32);
            this.flpLegend.TabIndex = 1;
            //
            // flpTables
            //
            this.flpTables.AutoScroll = true;
            this.flpTables.BackColor = global::Restaurant_Ordering_and_Management_System.Helper.AppTheme.Floor;
            this.flpTables.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpTables.Location = new System.Drawing.Point(16, 48);
            this.flpTables.Name = "flpTables";
            this.flpTables.Padding = new System.Windows.Forms.Padding(12);
            this.flpTables.Size = new System.Drawing.Size(928, 372);
            this.flpTables.TabIndex = 0;
            this.flpTables.TableDoubleClicked += new System.EventHandler<Restaurant_Ordering_and_Management_System.Forms.TableEventArgs>(this.FlpTables_TableDoubleClicked);
            //
            // pnlButtons
            //
            this.pnlButtons.BackColor = global::Restaurant_Ordering_and_Management_System.Helper.AppTheme.Bar;
            this.pnlButtons.Controls.Add(this.btnAddTable);
            this.pnlButtons.Controls.Add(this.btnEditTable);
            this.pnlButtons.Controls.Add(this.btnUpdateStatus);
            this.pnlButtons.Controls.Add(this.btnDeleteTable);
            this.pnlButtons.Controls.Add(this.btnRefresh);
            this.pnlButtons.Controls.Add(this.btnReturn);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 500);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(960, 60);
            this.pnlButtons.TabIndex = 2;
            //
            // btnAddTable
            //
            this.btnAddTable.BackColor = global::Restaurant_Ordering_and_Management_System.Helper.AppTheme.Accent;
            this.btnAddTable.FlatAppearance.BorderSize = 0;
            this.btnAddTable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddTable.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnAddTable.ForeColor = System.Drawing.Color.White;
            this.btnAddTable.Location = new System.Drawing.Point(16, 12);
            this.btnAddTable.Name = "btnAddTable";
            this.btnAddTable.Size = new System.Drawing.Size(110, 36);
            this.btnAddTable.TabIndex = 0;
            this.btnAddTable.Text = "➕ Add Table";
            this.btnAddTable.UseVisualStyleBackColor = false;
            this.btnAddTable.Click += new System.EventHandler(this.BtnAddTable_Click);
            //
            // btnEditTable
            //
            this.btnEditTable.BackColor = global::Restaurant_Ordering_and_Management_System.Helper.AppTheme.Primary;
            this.btnEditTable.FlatAppearance.BorderSize = 0;
            this.btnEditTable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditTable.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnEditTable.ForeColor = System.Drawing.Color.White;
            this.btnEditTable.Location = new System.Drawing.Point(136, 12);
            this.btnEditTable.Name = "btnEditTable";
            this.btnEditTable.Size = new System.Drawing.Size(100, 36);
            this.btnEditTable.TabIndex = 1;
            this.btnEditTable.Text = "✏️ Edit";
            this.btnEditTable.UseVisualStyleBackColor = false;
            this.btnEditTable.Click += new System.EventHandler(this.BtnEditTable_Click);
            //
            // btnUpdateStatus
            //
            this.btnUpdateStatus.BackColor = global::Restaurant_Ordering_and_Management_System.Helper.AppTheme.Primary;
            this.btnUpdateStatus.FlatAppearance.BorderSize = 0;
            this.btnUpdateStatus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnUpdateStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnUpdateStatus.ForeColor = System.Drawing.Color.White;
            this.btnUpdateStatus.Location = new System.Drawing.Point(246, 12);
            this.btnUpdateStatus.Name = "btnUpdateStatus";
            this.btnUpdateStatus.Size = new System.Drawing.Size(140, 36);
            this.btnUpdateStatus.TabIndex = 2;
            this.btnUpdateStatus.Text = "🔄 Update Status";
            this.btnUpdateStatus.UseVisualStyleBackColor = false;
            this.btnUpdateStatus.Click += new System.EventHandler(this.BtnUpdateStatus_Click);
            //
            // btnDeleteTable
            //
            this.btnDeleteTable.BackColor = global::Restaurant_Ordering_and_Management_System.Helper.AppTheme.Danger;
            this.btnDeleteTable.FlatAppearance.BorderSize = 0;
            this.btnDeleteTable.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeleteTable.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnDeleteTable.ForeColor = System.Drawing.Color.White;
            this.btnDeleteTable.Location = new System.Drawing.Point(396, 12);
            this.btnDeleteTable.Name = "btnDeleteTable";
            this.btnDeleteTable.Size = new System.Drawing.Size(100, 36);
            this.btnDeleteTable.TabIndex = 3;
            this.btnDeleteTable.Text = "🗑️ Delete";
            this.btnDeleteTable.UseVisualStyleBackColor = false;
            this.btnDeleteTable.Click += new System.EventHandler(this.BtnDeleteTable_Click);
            //
            // btnRefresh
            //
            this.btnRefresh.BackColor = global::Restaurant_Ordering_and_Management_System.Helper.AppTheme.Neutral;
            this.btnRefresh.FlatAppearance.BorderSize = 0;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnRefresh.ForeColor = System.Drawing.Color.White;
            this.btnRefresh.Location = new System.Drawing.Point(506, 12);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(100, 36);
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "⟳ Refresh";
            this.btnRefresh.UseVisualStyleBackColor = false;
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            //
            // btnReturn
            //
            this.btnReturn.BackColor = global::Restaurant_Ordering_and_Management_System.Helper.AppTheme.Neutral;
            this.btnReturn.FlatAppearance.BorderSize = 0;
            this.btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturn.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.btnReturn.ForeColor = System.Drawing.Color.White;
            this.btnReturn.Location = new System.Drawing.Point(844, 12);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(100, 36);
            this.btnReturn.TabIndex = 5;
            this.btnReturn.Text = "↩ Return";
            this.btnReturn.UseVisualStyleBackColor = false;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            //
            // FormTables
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(960, 560);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(760, 480);
            this.Name = "FormTables";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Table Management";
            this.Load += new System.EventHandler(this.FormTables_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.flpLegend.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlContent;
        private Restaurant_Ordering_and_Management_System.Forms.TableFloorPanel flpTables;
        private Restaurant_Ordering_and_Management_System.Forms.TableStatusLegend flpLegend;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnAddTable;
        private System.Windows.Forms.Button btnEditTable;
        private System.Windows.Forms.Button btnUpdateStatus;
        private System.Windows.Forms.Button btnDeleteTable;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnReturn;
    }
}
    #endregion