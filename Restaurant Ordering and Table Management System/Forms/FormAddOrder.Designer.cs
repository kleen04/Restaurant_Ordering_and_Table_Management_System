namespace Restaurant_Ordering_and_Management_System.Forms
{
    partial class FormAddOrder
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
            this.pnlTableSelect = new System.Windows.Forms.Panel();
            this.cmbTable = new System.Windows.Forms.ComboBox();
            this.lblTable = new System.Windows.Forms.Label();
            this.pnlOrder = new System.Windows.Forms.Panel();
            this.dgvOrderItems = new System.Windows.Forms.DataGridView();
            this.lblOrderItemsHeader = new System.Windows.Forms.Label();
            this.pnlTotal = new System.Windows.Forms.Panel();
            this.btnRemoveItem = new System.Windows.Forms.Button();
            this.lblTotalValue = new System.Windows.Forms.Label();
            this.lblTotalCaption = new System.Windows.Forms.Label();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.dgvMenu = new System.Windows.Forms.DataGridView();
            this.lblMenuHeader = new System.Windows.Forms.Label();
            this.pnlAddToOrder = new System.Windows.Forms.Panel();
            this.btnAddToOrder = new System.Windows.Forms.Button();
            this.nudQuantity = new System.Windows.Forms.NumericUpDown();
            this.lblQuantity = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnPlaceOrder = new System.Windows.Forms.Button();
            this.btnReturn = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlTableSelect.SuspendLayout();
            this.pnlOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderItems)).BeginInit();
            this.pnlTotal.SuspendLayout();
            this.pnlMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMenu)).BeginInit();
            this.pnlAddToOrder.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).BeginInit();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            //
            // pnlHeader
            //
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(107)))), ((int)(((byte)(53)))));
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.pnlHeader.Size = new System.Drawing.Size(1040, 64);
            this.pnlHeader.TabIndex = 0;
            //
            // lblTitle
            //
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(150, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🧾 New Order";
            //
            // lblSubtitle
            //
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(227)))));
            this.lblSubtitle.Location = new System.Drawing.Point(22, 38);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(220, 17);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Build an order for a dining table";
            //
            // pnlTableSelect
            //
            this.pnlTableSelect.BackColor = System.Drawing.Color.White;
            this.pnlTableSelect.Controls.Add(this.cmbTable);
            this.pnlTableSelect.Controls.Add(this.lblTable);
            this.pnlTableSelect.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTableSelect.Location = new System.Drawing.Point(0, 64);
            this.pnlTableSelect.Name = "pnlTableSelect";
            this.pnlTableSelect.Padding = new System.Windows.Forms.Padding(20, 12, 20, 12);
            this.pnlTableSelect.Size = new System.Drawing.Size(1040, 52);
            this.pnlTableSelect.TabIndex = 1;
            //
            // lblTable
            //
            this.lblTable.AutoSize = true;
            this.lblTable.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTable.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblTable.Location = new System.Drawing.Point(20, 15);
            this.lblTable.Name = "lblTable";
            this.lblTable.Size = new System.Drawing.Size(48, 19);
            this.lblTable.TabIndex = 0;
            this.lblTable.Text = "Table:";
            //
            // cmbTable
            //
            this.cmbTable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTable.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cmbTable.FormattingEnabled = true;
            this.cmbTable.Items.AddRange(new object[] {
            "Table 1 (2 seats)",
            "Table 2 (4 seats)",
            "Table 3 (4 seats)",
            "Table 4 (6 seats)",
            "Table 5 (2 seats)",
            "Table 6 (8 seats)"});
            this.cmbTable.Location = new System.Drawing.Point(76, 12);
            this.cmbTable.Name = "cmbTable";
            this.cmbTable.Size = new System.Drawing.Size(200, 25);
            this.cmbTable.TabIndex = 1;
            //
            // pnlOrder
            //
            this.pnlOrder.BackColor = System.Drawing.Color.White;
            this.pnlOrder.Controls.Add(this.dgvOrderItems);
            this.pnlOrder.Controls.Add(this.pnlTotal);
            this.pnlOrder.Controls.Add(this.lblOrderItemsHeader);
            this.pnlOrder.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlOrder.Location = new System.Drawing.Point(460, 116);
            this.pnlOrder.Name = "pnlOrder";
            this.pnlOrder.Padding = new System.Windows.Forms.Padding(16, 12, 16, 16);
            this.pnlOrder.Size = new System.Drawing.Size(580, 364);
            this.pnlOrder.TabIndex = 3;
            //
            // lblOrderItemsHeader
            //
            this.lblOrderItemsHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblOrderItemsHeader.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblOrderItemsHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblOrderItemsHeader.Location = new System.Drawing.Point(16, 12);
            this.lblOrderItemsHeader.Name = "lblOrderItemsHeader";
            this.lblOrderItemsHeader.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.lblOrderItemsHeader.Size = new System.Drawing.Size(548, 30);
            this.lblOrderItemsHeader.TabIndex = 0;
            this.lblOrderItemsHeader.Text = "Current Order";
            //
            // pnlTotal
            //
            this.pnlTotal.Controls.Add(this.btnRemoveItem);
            this.pnlTotal.Controls.Add(this.lblTotalValue);
            this.pnlTotal.Controls.Add(this.lblTotalCaption);
            this.pnlTotal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTotal.Location = new System.Drawing.Point(16, 306);
            this.pnlTotal.Name = "pnlTotal";
            this.pnlTotal.Size = new System.Drawing.Size(548, 62);
            this.pnlTotal.TabIndex = 2;
            //
            // btnRemoveItem
            //
            this.btnRemoveItem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnRemoveItem.FlatAppearance.BorderSize = 0;
            this.btnRemoveItem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemoveItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnRemoveItem.ForeColor = System.Drawing.Color.White;
            this.btnRemoveItem.Location = new System.Drawing.Point(438, 14);
            this.btnRemoveItem.Name = "btnRemoveItem";
            this.btnRemoveItem.Size = new System.Drawing.Size(110, 34);
            this.btnRemoveItem.TabIndex = 2;
            this.btnRemoveItem.Text = "🗑️ Remove";
            this.btnRemoveItem.UseVisualStyleBackColor = false;
            this.btnRemoveItem.Click += new System.EventHandler(this.BtnRemoveItem_Click);
            //
            // lblTotalValue
            //
            this.lblTotalValue.AutoSize = true;
            this.lblTotalValue.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTotalValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.lblTotalValue.Location = new System.Drawing.Point(90, 12);
            this.lblTotalValue.Name = "lblTotalValue";
            this.lblTotalValue.Size = new System.Drawing.Size(85, 37);
            this.lblTotalValue.TabIndex = 1;
            this.lblTotalValue.Text = "₱0.00";
            //
            // lblTotalCaption
            //
            this.lblTotalCaption.AutoSize = true;
            this.lblTotalCaption.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotalCaption.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblTotalCaption.Location = new System.Drawing.Point(0, 22);
            this.lblTotalCaption.Name = "lblTotalCaption";
            this.lblTotalCaption.Size = new System.Drawing.Size(48, 20);
            this.lblTotalCaption.TabIndex = 0;
            this.lblTotalCaption.Text = "Total:";
            //
            // dgvOrderItems
            //
            this.dgvOrderItems.AllowUserToAddRows = false;
            this.dgvOrderItems.AllowUserToDeleteRows = false;
            this.dgvOrderItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvOrderItems.BackgroundColor = System.Drawing.Color.White;
            this.dgvOrderItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvOrderItems.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(227)))));
            this.dgvOrderItems.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvOrderItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrderItems.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.dgvOrderItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOrderItems.EnableHeadersVisualStyles = false;
            this.dgvOrderItems.Location = new System.Drawing.Point(16, 42);
            this.dgvOrderItems.Name = "dgvOrderItems";
            this.dgvOrderItems.ReadOnly = true;
            this.dgvOrderItems.RowHeadersVisible = false;
            this.dgvOrderItems.RowTemplate.Height = 30;
            this.dgvOrderItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvOrderItems.Size = new System.Drawing.Size(548, 264);
            this.dgvOrderItems.TabIndex = 1;
            //
            // pnlMenu
            //
            this.pnlMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.pnlMenu.Controls.Add(this.dgvMenu);
            this.pnlMenu.Controls.Add(this.pnlAddToOrder);
            this.pnlMenu.Controls.Add(this.lblMenuHeader);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenu.Location = new System.Drawing.Point(0, 116);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Padding = new System.Windows.Forms.Padding(16, 12, 8, 16);
            this.pnlMenu.Size = new System.Drawing.Size(460, 364);
            this.pnlMenu.TabIndex = 2;
            //
            // lblMenuHeader
            //
            this.lblMenuHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMenuHeader.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblMenuHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblMenuHeader.Location = new System.Drawing.Point(16, 12);
            this.lblMenuHeader.Name = "lblMenuHeader";
            this.lblMenuHeader.Padding = new System.Windows.Forms.Padding(0, 0, 0, 8);
            this.lblMenuHeader.Size = new System.Drawing.Size(436, 30);
            this.lblMenuHeader.TabIndex = 0;
            this.lblMenuHeader.Text = "Menu Items";
            //
            // pnlAddToOrder
            //
            this.pnlAddToOrder.Controls.Add(this.btnAddToOrder);
            this.pnlAddToOrder.Controls.Add(this.nudQuantity);
            this.pnlAddToOrder.Controls.Add(this.lblQuantity);
            this.pnlAddToOrder.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlAddToOrder.Location = new System.Drawing.Point(16, 306);
            this.pnlAddToOrder.Name = "pnlAddToOrder";
            this.pnlAddToOrder.Size = new System.Drawing.Size(436, 62);
            this.pnlAddToOrder.TabIndex = 2;
            //
            // lblQuantity
            //
            this.lblQuantity.AutoSize = true;
            this.lblQuantity.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblQuantity.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80)))));
            this.lblQuantity.Location = new System.Drawing.Point(0, 20);
            this.lblQuantity.Name = "lblQuantity";
            this.lblQuantity.Size = new System.Drawing.Size(52, 19);
            this.lblQuantity.TabIndex = 0;
            this.lblQuantity.Text = "Qty:";
            //
            // nudQuantity
            //
            this.nudQuantity.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.nudQuantity.Location = new System.Drawing.Point(56, 16);
            this.nudQuantity.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudQuantity.Maximum = new decimal(new int[] { 99, 0, 0, 0 });
            this.nudQuantity.Value = new decimal(new int[] { 1, 0, 0, 0 });
            this.nudQuantity.Name = "nudQuantity";
            this.nudQuantity.Size = new System.Drawing.Size(60, 25);
            this.nudQuantity.TabIndex = 1;
            //
            // btnAddToOrder
            //
            this.btnAddToOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnAddToOrder.FlatAppearance.BorderSize = 0;
            this.btnAddToOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddToOrder.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnAddToOrder.ForeColor = System.Drawing.Color.White;
            this.btnAddToOrder.Location = new System.Drawing.Point(146, 12);
            this.btnAddToOrder.Name = "btnAddToOrder";
            this.btnAddToOrder.Size = new System.Drawing.Size(180, 34);
            this.btnAddToOrder.TabIndex = 2;
            this.btnAddToOrder.Text = "➕ Add to Order";
            this.btnAddToOrder.UseVisualStyleBackColor = false;
            this.btnAddToOrder.Click += new System.EventHandler(this.BtnAddToOrder_Click);
            //
            // dgvMenu
            //
            this.dgvMenu.AllowUserToAddRows = false;
            this.dgvMenu.AllowUserToDeleteRows = false;
            this.dgvMenu.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvMenu.BackgroundColor = System.Drawing.Color.White;
            this.dgvMenu.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvMenu.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(237)))), ((int)(((byte)(227)))));
            this.dgvMenu.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.dgvMenu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvMenu.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.dgvMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMenu.EnableHeadersVisualStyles = false;
            this.dgvMenu.Location = new System.Drawing.Point(16, 42);
            this.dgvMenu.MultiSelect = false;
            this.dgvMenu.Name = "dgvMenu";
            this.dgvMenu.ReadOnly = true;
            this.dgvMenu.RowHeadersVisible = false;
            this.dgvMenu.RowTemplate.Height = 30;
            this.dgvMenu.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvMenu.Size = new System.Drawing.Size(436, 264);
            this.dgvMenu.TabIndex = 1;
            //
            // pnlButtons
            //
            this.pnlButtons.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlButtons.Controls.Add(this.btnPlaceOrder);
            this.pnlButtons.Controls.Add(this.btnReturn);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 480);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(1040, 64);
            this.pnlButtons.TabIndex = 4;
            //
            // btnPlaceOrder
            //
            this.btnPlaceOrder.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnPlaceOrder.FlatAppearance.BorderSize = 0;
            this.btnPlaceOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPlaceOrder.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnPlaceOrder.ForeColor = System.Drawing.Color.White;
            this.btnPlaceOrder.Location = new System.Drawing.Point(16, 12);
            this.btnPlaceOrder.Name = "btnPlaceOrder";
            this.btnPlaceOrder.Size = new System.Drawing.Size(200, 40);
            this.btnPlaceOrder.TabIndex = 0;
            this.btnPlaceOrder.Text = "✔ Place Order";
            this.btnPlaceOrder.UseVisualStyleBackColor = false;
            this.btnPlaceOrder.Click += new System.EventHandler(this.BtnPlaceOrder_Click);
            //
            // btnReturn
            //
            this.btnReturn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(195)))), ((int)(((byte)(199)))));
            this.btnReturn.FlatAppearance.BorderSize = 0;
            this.btnReturn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturn.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnReturn.ForeColor = System.Drawing.Color.White;
            this.btnReturn.Location = new System.Drawing.Point(924, 12);
            this.btnReturn.Name = "btnReturn";
            this.btnReturn.Size = new System.Drawing.Size(100, 40);
            this.btnReturn.TabIndex = 1;
            this.btnReturn.Text = "✕ Cancel";
            this.btnReturn.UseVisualStyleBackColor = false;
            this.btnReturn.Click += new System.EventHandler(this.btnReturn_Click);
            //
            // FormAddOrder
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(245)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(1040, 544);
            this.Controls.Add(this.pnlOrder);
            this.Controls.Add(this.pnlMenu);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.pnlTableSelect);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.MinimumSize = new System.Drawing.Size(880, 500);
            this.Name = "FormAddOrder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "New Order";
            this.Load += new System.EventHandler(this.FormAddOrder_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlTableSelect.ResumeLayout(false);
            this.pnlTableSelect.PerformLayout();
            this.pnlOrder.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderItems)).EndInit();
            this.pnlTotal.ResumeLayout(false);
            this.pnlTotal.PerformLayout();
            this.pnlMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMenu)).EndInit();
            this.pnlAddToOrder.ResumeLayout(false);
            this.pnlAddToOrder.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudQuantity)).EndInit();
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlTableSelect;
        private System.Windows.Forms.Label lblTable;
        private System.Windows.Forms.ComboBox cmbTable;
        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Label lblMenuHeader;
        private System.Windows.Forms.DataGridView dgvMenu;
        private System.Windows.Forms.Panel pnlAddToOrder;
        private System.Windows.Forms.Label lblQuantity;
        private System.Windows.Forms.NumericUpDown nudQuantity;
        private System.Windows.Forms.Button btnAddToOrder;
        private System.Windows.Forms.Panel pnlOrder;
        private System.Windows.Forms.Label lblOrderItemsHeader;
        private System.Windows.Forms.DataGridView dgvOrderItems;
        private System.Windows.Forms.Panel pnlTotal;
        private System.Windows.Forms.Label lblTotalCaption;
        private System.Windows.Forms.Label lblTotalValue;
        private System.Windows.Forms.Button btnRemoveItem;
        private System.Windows.Forms.Panel pnlButtons;
        private System.Windows.Forms.Button btnPlaceOrder;
        private System.Windows.Forms.Button btnReturn;
    }
}
    #endregion