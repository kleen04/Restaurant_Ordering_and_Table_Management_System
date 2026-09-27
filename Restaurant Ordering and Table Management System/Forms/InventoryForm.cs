using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Restaurant_Ordering_and_Management_System.DBContext;
using Restaurant_Ordering_and_Management_System.Helper;
using Restaurant_Ordering_and_Management_System.Models;
using Restaurant_Ordering_and_Management_System.Service;

namespace Restaurant_Ordering_and_Management_System.Forms
{
    public partial class InventoryForm : Form
    {
        private InventoryService _inventoryService;

        public InventoryForm()
        {
            InitializeComponent();
        }

        private void InventoryForm_Load(object sender, EventArgs e)
        {
            try
            {
                // Initialize services
                DatabaseConnection dbConnection = new DatabaseConnection();
                DbHelper dbHelper = new DbHelper(dbConnection);
                _inventoryService = new InventoryService(dbHelper);

                InitializeInventoryData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading inventory data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

            try
            {
                // Load real data from database
                List<InventoryItem> items = _inventoryService.GetAllItems();

                foreach (InventoryItem item in items)
                {
                    string status = item.IsLowStock ? "Low Stock" : "In Stock";
                    dgvInventory.Rows.Add(
                        item.ItemId,
                        item.ItemName,
                        item.Category,
                        item.Quantity.ToString("F2"),
                        item.Unit,
                        item.ReorderLevel.ToString("F2"),
                        $"₱{item.UnitCost:F2}",
                        status
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error populating inventory grid: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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
            try
            {
                dgvInventory.Rows.Clear();
                InitializeInventoryData();
                MessageBox.Show("Inventory refreshed successfully.", "Refresh", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing inventory data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
