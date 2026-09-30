using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Restaurant_Ordering_and_Management_System.Helper;
using Restaurant_Ordering_and_Management_System.Interfaces;
using Restaurant_Ordering_and_Management_System.Models;
using MenuItem = Restaurant_Ordering_and_Management_System.Models.MenuItem;

namespace Restaurant_Ordering_and_Management_System.Forms
{
    public partial class FormAddOrder : Form
    {
        private readonly IMenuService _menuService;
        private readonly ITableService _tableService;
        private readonly IStaffService _staffService;
        private readonly IOrderService _orderService;

        // The order being built. The grid is always re-drawn from this list.
        private readonly List<OrderItem> _lines = new List<OrderItem>();

        private ComboBox _cmbStaff;
        private NumericUpDown _nudGuests;

        public FormAddOrder(IMenuService menuService, ITableService tableService,
                            IStaffService staffService, IOrderService orderService)
        {
            _menuService = menuService ?? throw new ArgumentNullException(nameof(menuService));
            _tableService = tableService ?? throw new ArgumentNullException(nameof(tableService));
            _staffService = staffService ?? throw new ArgumentNullException(nameof(staffService));
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));

            InitializeComponent();
            AddStaffAndGuestControls();
            cmbTable.SelectedIndexChanged += CmbTable_SelectedIndexChanged;
        }

        // Items shown in the combo boxes: real records, readable text.
        private sealed class TableChoice
        {
            public RestaurantTable Table { get; set; }
            public override string ToString()
            {
                return "Table " + Table.TableId + " (seats " + Table.Capacity + ", " + Table.Status + ")";
            }
        }

        private sealed class StaffChoice
        {
            public Staff Staff { get; set; }
            public override string ToString()
            {
                return Staff.FullName + " (" + Staff.Position + ")";
            }
        }

        // Staff and guest-count selectors sit on the same strip as the table selector.
        private void AddStaffAndGuestControls()
        {
            Font labelFont = lblTable.Font;
            Color labelColor = lblTable.ForeColor;

            Label lblStaff = new Label { Text = "Staff:", AutoSize = true, Font = labelFont, ForeColor = labelColor, Location = new Point(300, 15) };
            _cmbStaff = new ComboBox { DropDownStyle = ComboBoxStyle.DropDownList, Font = cmbTable.Font, Location = new Point(356, 12), Size = new Size(240, 25) };
            Label lblGuests = new Label { Text = "Guests:", AutoSize = true, Font = labelFont, ForeColor = labelColor, Location = new Point(620, 15) };
            _nudGuests = new NumericUpDown { Font = cmbTable.Font, Location = new Point(690, 12), Size = new Size(70, 25), Minimum = 1, Maximum = 1, Value = 1 };

            pnlTableSelect.Controls.Add(lblStaff);
            pnlTableSelect.Controls.Add(_cmbStaff);
            pnlTableSelect.Controls.Add(lblGuests);
            pnlTableSelect.Controls.Add(_nudGuests);
        }

        private void FormAddOrder_Load(object sender, EventArgs e)
        {
            try
            {
                LoadTables();
                LoadStaff();
                LoadMenu();
                InitializeOrderGrid();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Error loading order data: " + ex.Message, "Database Error");
                btnPlaceOrder.Enabled = false;
            }
        }

        private void LoadTables()
        {
            cmbTable.Items.Clear();

            // An occupied table already has a running order, so it is not offered.
            foreach (RestaurantTable table in _tableService.GetAllTables())
            {
                if (table.Status != TableStatus.Occupied)
                {
                    cmbTable.Items.Add(new TableChoice { Table = table });
                }
            }

            if (cmbTable.Items.Count > 0)
            {
                cmbTable.SelectedIndex = 0;
            }
            else
            {
                btnPlaceOrder.Enabled = false;
                MessageHelper.ShowWarning("There are no free tables right now. Free a table first, then create the order.", "No Free Tables");
            }
        }

        private void LoadStaff()
        {
            _cmbStaff.Items.Clear();

            foreach (Staff staff in _staffService.GetAllStaff())
            {
                if (staff.IsActive)
                {
                    _cmbStaff.Items.Add(new StaffChoice { Staff = staff });
                }
            }

            if (_cmbStaff.Items.Count > 0)
            {
                _cmbStaff.SelectedIndex = 0;
            }
            else
            {
                btnPlaceOrder.Enabled = false;
                MessageHelper.ShowWarning("There is no active staff member to take the order. Add or activate one in Staff first.", "No Active Staff");
            }
        }

        private void CmbTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            TableChoice choice = cmbTable.SelectedItem as TableChoice;
            if (choice == null)
            {
                return;
            }

            _nudGuests.Maximum = Math.Max(1, choice.Table.Capacity);
            _nudGuests.Value = 1;
        }

        private void LoadMenu()
        {
            dgvMenu.Columns.Clear();
            dgvMenu.Columns.Add("MenuItemId", "Id");
            dgvMenu.Columns["MenuItemId"].Visible = false;
            dgvMenu.Columns.Add("Name", "Item");
            dgvMenu.Columns.Add("Category", "Category");
            dgvMenu.Columns.Add("Price", "Price");
            dgvMenu.Columns["Price"].DefaultCellStyle.Format = "N2";

            foreach (MenuItem item in _menuService.GetAllMenuItems())
            {
                if (item.IsAvailable)
                {
                    dgvMenu.Rows.Add(item.MenuItemId, item.Name, item.Category, item.Price);
                }
            }

            dgvMenu.AutoResizeColumns();
        }

        private void InitializeOrderGrid()
        {
            dgvOrderItems.Columns.Clear();
            dgvOrderItems.Columns.Add("Item", "Item");
            dgvOrderItems.Columns.Add("Quantity", "Qty");
            dgvOrderItems.Columns.Add("UnitPrice", "Unit Price");
            dgvOrderItems.Columns.Add("Subtotal", "Subtotal");
            RenderOrderLines();
        }

        private void RenderOrderLines()
        {
            dgvOrderItems.Rows.Clear();

            foreach (OrderItem line in _lines)
            {
                int rowIndex = dgvOrderItems.Rows.Add(line.MenuItemName, line.Quantity,
                    line.UnitPrice.ToString("N2"), line.Subtotal.ToString("N2"));
                dgvOrderItems.Rows[rowIndex].Tag = line;
            }

            decimal total = 0m;
            foreach (OrderItem line in _lines)
            {
                total += line.Subtotal;
            }
            lblTotalValue.Text = "₱" + total.ToString("N2");
        }

        private void BtnAddToOrder_Click(object sender, EventArgs e)
        {
            if (dgvMenu.SelectedRows.Count == 0)
            {
                MessageHelper.ShowWarning("Please select a menu item first", "Add to Order");
                return;
            }

            DataGridViewRow row = dgvMenu.SelectedRows[0];
            int menuItemId = Convert.ToInt32(row.Cells["MenuItemId"].Value);
            int quantity = (int)nudQuantity.Value;

            // Adding the same dish again just increases its quantity.
            OrderItem existing = _lines.Find(l => l.MenuItemId == menuItemId);
            if (existing != null)
            {
                existing.Quantity += quantity;
            }
            else
            {
                _lines.Add(new OrderItem
                {
                    MenuItemId = menuItemId,
                    MenuItemName = row.Cells["Name"].Value.ToString(),
                    UnitPrice = Convert.ToDecimal(row.Cells["Price"].Value),
                    Quantity = quantity
                });
            }

            RenderOrderLines();
        }

        private void BtnRemoveItem_Click(object sender, EventArgs e)
        {
            if (dgvOrderItems.SelectedRows.Count == 0)
            {
                MessageHelper.ShowWarning("Please select an order line to remove", "Remove Item");
                return;
            }

            OrderItem line = dgvOrderItems.SelectedRows[0].Tag as OrderItem;
            if (line != null)
            {
                _lines.Remove(line);
            }

            RenderOrderLines();
        }

        private void BtnPlaceOrder_Click(object sender, EventArgs e)
        {
            TableChoice tableChoice = cmbTable.SelectedItem as TableChoice;
            StaffChoice staffChoice = _cmbStaff.SelectedItem as StaffChoice;

            if (tableChoice == null)
            {
                MessageHelper.ShowWarning("Please select a table", "Place Order");
                return;
            }
            if (staffChoice == null)
            {
                MessageHelper.ShowWarning("Please select the staff member taking the order", "Place Order");
                return;
            }
            if (_lines.Count == 0)
            {
                MessageHelper.ShowWarning("Add at least one item before placing the order", "Place Order");
                return;
            }

            Order order = new Order
            {
                TableId = tableChoice.Table.TableId,
                StaffId = staffChoice.Staff.StaffId,
                GuestCount = (int)_nudGuests.Value
            };
            order.Items.AddRange(_lines);

            try
            {
                int orderId = _orderService.CreateOrder(order);

                MessageHelper.ShowInfo(
                    "Order #" + orderId + " placed for Table " + order.TableId + ".\nTotal: ₱" + order.Total.ToString("N2"),
                    "Order Placed");

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("The order could not be saved: " + ex.Message, "Place Order");
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
