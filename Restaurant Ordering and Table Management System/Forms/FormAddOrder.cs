using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_Ordering_and_Management_System.Forms
{
    public partial class FormAddOrder : Form
    {
        private decimal _orderTotal = 0m;

        public FormAddOrder()
        {
            InitializeComponent();
        }

        private void FormAddOrder_Load(object sender, EventArgs e)
        {
            if (cmbTable.Items.Count > 0)
            {
                cmbTable.SelectedIndex = 0;
            }
            InitializeMenuData();
            InitializeOrderGrid();
        }

        private void InitializeMenuData()
        {
            dgvMenu.Columns.Clear();
            dgvMenu.Columns.Add("Name", "Item");
            dgvMenu.Columns.Add("Category", "Category");
            dgvMenu.Columns.Add("Price", "Price");

            dgvMenu.Rows.Add("Adobo Rice Bowl", "Main", "185.00");
            dgvMenu.Rows.Add("Sinigang na Baboy", "Main", "220.00");
            dgvMenu.Rows.Add("Grilled Bangus", "Main", "210.00");
            dgvMenu.Rows.Add("Lumpiang Shanghai", "Appetizer", "120.00");
            dgvMenu.Rows.Add("Halo-Halo", "Dessert", "95.00");
            dgvMenu.Rows.Add("Iced Tea", "Beverage", "45.00");
            dgvMenu.Rows.Add("Bottled Water", "Beverage", "25.00");

            dgvMenu.AutoResizeColumns();
        }

        private void InitializeOrderGrid()
        {
            dgvOrderItems.Columns.Clear();
            dgvOrderItems.Columns.Add("Item", "Item");
            dgvOrderItems.Columns.Add("Quantity", "Qty");
            dgvOrderItems.Columns.Add("UnitPrice", "Unit Price");
            dgvOrderItems.Columns.Add("Subtotal", "Subtotal");
            UpdateTotal();
        }

        private void BtnAddToOrder_Click(object sender, EventArgs e)
        {
            if (dgvMenu.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a menu item first", "Add to Order", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var row = dgvMenu.SelectedRows[0];
            string name = row.Cells["Name"].Value.ToString();
            decimal price = decimal.Parse(row.Cells["Price"].Value.ToString());
            int qty = (int)nudQuantity.Value;
            decimal subtotal = price * qty;

            dgvOrderItems.Rows.Add(name, qty, price.ToString("N2"), subtotal.ToString("N2"));
            _orderTotal += subtotal;
            UpdateTotal();
        }

        private void BtnRemoveItem_Click(object sender, EventArgs e)
        {
            if (dgvOrderItems.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an order line to remove", "Remove Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var row = dgvOrderItems.SelectedRows[0];
            decimal subtotal = decimal.Parse(row.Cells["Subtotal"].Value.ToString());
            _orderTotal -= subtotal;
            dgvOrderItems.Rows.RemoveAt(row.Index);
            UpdateTotal();
        }

        private void UpdateTotal()
        {
            lblTotalValue.Text = "₱" + _orderTotal.ToString("N2");
        }

        private void BtnPlaceOrder_Click(object sender, EventArgs e)
        {
            if (dgvOrderItems.Rows.Count == 0)
            {
                MessageBox.Show("Add at least one item before placing the order", "Place Order", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            MessageBox.Show("Place Order functionality to be implemented (save via IOrderService.CreateOrder)", "Place Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
