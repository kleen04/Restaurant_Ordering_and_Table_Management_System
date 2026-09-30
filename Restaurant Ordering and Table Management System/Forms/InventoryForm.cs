using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Restaurant_Ordering_and_Management_System.Helper;
using Restaurant_Ordering_and_Management_System.Interfaces;
using Restaurant_Ordering_and_Management_System.Models;

namespace Restaurant_Ordering_and_Management_System.Forms
{
    public partial class InventoryForm : Form
    {
        private readonly IInventoryService _inventoryService;

        public InventoryForm(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService ?? throw new ArgumentNullException(nameof(inventoryService));
            InitializeComponent();
        }

        private void InventoryForm_Load(object sender, EventArgs e)
        {
            // Attached once here (not on every refresh) so it runs once per cell.
            dgvInventory.CellFormatting += DgvInventory_CellFormatting;
            LoadInventory();
        }

        private void LoadInventory()
        {
            // Clear first, otherwise every refresh would add the columns again.
            dgvInventory.Columns.Clear();
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
                foreach (InventoryItem item in _inventoryService.GetAllItems())
                {
                    dgvInventory.Rows.Add(
                        item.ItemId,
                        item.ItemName,
                        item.Category,
                        item.Quantity.ToString("F2"),
                        item.Unit,
                        item.ReorderLevel.ToString("F2"),
                        "₱" + item.UnitCost.ToString("F2"),
                        item.IsLowStock ? "Low Stock" : "In Stock");
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Error populating inventory grid: " + ex.Message, "Database Error");
            }

            dgvInventory.AutoResizeColumns();
        }

        // Colour-codes the Status column: red for Low Stock, green for In Stock.
        private void DgvInventory_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvInventory.Columns[e.ColumnIndex].Name != "Status" || e.Value == null)
            {
                return;
            }

            if (e.Value.ToString() == "Low Stock")
            {
                e.CellStyle.ForeColor = Color.FromArgb(192, 57, 43);
                e.CellStyle.Font = new Font(dgvInventory.Font, FontStyle.Bold);
            }
            else
            {
                e.CellStyle.ForeColor = Color.FromArgb(30, 132, 73);
                e.CellStyle.Font = new Font(dgvInventory.Font, FontStyle.Bold);
            }
        }

        private InventoryItem GetSelectedItem()
        {
            if (dgvInventory.SelectedRows.Count == 0)
            {
                return null;
            }

            int itemId = Convert.ToInt32(dgvInventory.SelectedRows[0].Cells["ItemID"].Value);
            return _inventoryService.GetAllItems().Find(i => i.ItemId == itemId);
        }

        // Shared by Add and Edit: shows the dialog and returns the item entered (or null).
        private InventoryItem PromptForItem(string title, InventoryItem existing)
        {
            List<DialogField> fields = new List<DialogField>
            {
                DialogField.Text("Item Name", existing == null ? "" : existing.ItemName),
                DialogField.Text("Category", existing == null ? "" : existing.Category),
                DialogField.Text("Quantity", existing == null ? "0" : existing.Quantity.ToString("0.##")),
                DialogField.Text("Unit", existing == null ? "" : existing.Unit),
                DialogField.Text("Reorder Level", existing == null ? "0" : existing.ReorderLevel.ToString("0.##")),
                DialogField.Text("Unit Cost", existing == null ? "0" : existing.UnitCost.ToString("0.##"))
            };

            string[] values = InputDialog.Prompt(this, title, fields, ValidateItemInput);
            if (values == null)
            {
                return null;
            }

            return new InventoryItem
            {
                ItemId = existing == null ? 0 : existing.ItemId,
                ItemName = values[0],
                Category = values[1],
                Quantity = decimal.Parse(values[2]),
                Unit = values[3],
                ReorderLevel = decimal.Parse(values[4]),
                UnitCost = decimal.Parse(values[5])
            };
        }

        private static string ValidateItemInput(string[] v)
        {
            if (!ValidationHelper.IsRequired(v[0]))
            {
                return "Item name is required.";
            }
            if (!ValidationHelper.IsRequired(v[1]))
            {
                return "Category is required.";
            }
            if (!ValidationHelper.IsRequired(v[3]))
            {
                return "Unit is required (for example kg, liter, pcs).";
            }
            if (v[0].Length > 100 || v[1].Length > 50 || v[3].Length > 20)
            {
                return "One of the text fields is too long (name 100, category 50, unit 20 characters).";
            }

            decimal number;
            if (!ValidationHelper.IsNonNegativeDecimal(v[2], out number))
            {
                return "Quantity must be a number that is zero or more.";
            }
            if (!ValidationHelper.IsNonNegativeDecimal(v[4], out number))
            {
                return "Reorder level must be a number that is zero or more.";
            }
            if (!ValidationHelper.IsNonNegativeDecimal(v[5], out number))
            {
                return "Unit cost must be a number that is zero or more.";
            }

            return null;
        }

        private void BtnAddItem_Click(object sender, EventArgs e)
        {
            InventoryItem item = PromptForItem("Add Inventory Item", null);
            if (item == null)
            {
                return;
            }

            try
            {
                _inventoryService.AddItem(item);
                LoadInventory();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Could not add the item: " + ex.Message, "Add Item");
            }
        }

        private void BtnEditItem_Click(object sender, EventArgs e)
        {
            try
            {
                InventoryItem existing = GetSelectedItem();
                if (existing == null)
                {
                    MessageHelper.ShowWarning("Please select an item to edit", "Edit Item");
                    return;
                }

                InventoryItem edited = PromptForItem("Edit Inventory Item", existing);
                if (edited == null)
                {
                    return;
                }

                _inventoryService.UpdateItem(edited);
                LoadInventory();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Could not update the item: " + ex.Message, "Edit Item");
            }
        }

        private void BtnDeleteItem_Click(object sender, EventArgs e)
        {
            try
            {
                InventoryItem item = GetSelectedItem();
                if (item == null)
                {
                    MessageHelper.ShowWarning("Please select an item to delete", "Delete Item");
                    return;
                }

                if (!MessageHelper.Confirm("Are you sure you want to delete \"" + item.ItemName + "\"?", "Delete Item"))
                {
                    return;
                }

                _inventoryService.DeleteItem(item.ItemId);
                LoadInventory();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Could not delete the item: " + ex.Message, "Delete Item");
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadInventory();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
