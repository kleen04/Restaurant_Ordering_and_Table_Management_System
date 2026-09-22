using System;
using System.Windows.Forms;

namespace Restaurant_Ordering_and_Management_System.Forms
{
    public partial class InventoryForm : Form
    {
        public InventoryForm()
        {
            InitializeComponent();
        }

        private void InventoryForm_Load(object sender, EventArgs e)
        {
            InitializeInventoryData();
        }

        private void InitializeInventoryData()
        {
            // Initialize DataGridView columns
            dgvInventory.Columns.Add("ItemID", "Item ID");
            dgvInventory.Columns.Add("ItemName", "Item Name");
            dgvInventory.Columns.Add("Category", "Category");
            dgvInventory.Columns.Add("Quantity", "Quantity");
            dgvInventory.Columns.Add("Unit", "Unit");
            dgvInventory.Columns.Add("ReorderLevel", "Reorder Level");
            dgvInventory.Columns.Add("UnitCost", "Unit Cost");
            dgvInventory.Columns.Add("Status", "Status");

            // Add sample data
            dgvInventory.Rows.Add("1", "Chicken Breast", "Meat", "50", "kg", "20", "₱250.00", "In Stock");
            dgvInventory.Rows.Add("2", "Rice", "Grains", "100", "kg", "30", "₱45.00", "In Stock");
            dgvInventory.Rows.Add("3", "Garlic", "Vegetables", "15", "kg", "10", "₱80.00", "In Stock");
            dgvInventory.Rows.Add("4", "Soy Sauce", "Condiments", "8", "bottles", "5", "₱120.00", "Low Stock");
            dgvInventory.Rows.Add("5", "Oil", "Cooking Supplies", "6", "liters", "4", "₱200.00", "In Stock");
            dgvInventory.Rows.Add("6", "Tomato Sauce", "Condiments", "12", "cans", "8", "₱60.00", "In Stock");
            dgvInventory.Rows.Add("7", "Onion", "Vegetables", "20", "kg", "15", "₱50.00", "In Stock");
            dgvInventory.Rows.Add("8", "Salt", "Seasonings", "3", "kg", "2", "₱30.00", "Low Stock");

            dgvInventory.AutoResizeColumns();
        }

        private void BtnAddItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Add Item functionality to be implemented", "Add Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnEditItem_Click(object sender, EventArgs e)
        {
            if (dgvInventory.SelectedRows.Count > 0)
            {
                MessageBox.Show("Edit Item functionality to be implemented", "Edit Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select an item to edit", "Edit Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnDeleteItem_Click(object sender, EventArgs e)
        {
            if (dgvInventory.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this item?", "Delete Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    dgvInventory.Rows.RemoveAt(dgvInventory.SelectedRows[0].Index);
                }
            }
            else
            {
                MessageBox.Show("Please select an item to delete", "Delete Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            dgvInventory.Rows.Clear();
            InitializeInventoryData();
            MessageBox.Show("Inventory refreshed", "Refresh", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
