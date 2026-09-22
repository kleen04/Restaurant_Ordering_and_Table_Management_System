using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Restaurant_Ordering_and_Management_System.Helper;
using Restaurant_Ordering_and_Management_System.Interfaces;
using Restaurant_Ordering_and_Management_System.Models;

namespace Restaurant_Ordering_and_Management_System.Service
{
    // ============================================================
    // OWNER: Inventory & Reports module member — backs InventoryForm
    // and FormReports.
    // Stored procedures: sp_Inventory_GetAll, sp_Inventory_Insert,
    // sp_Inventory_Update, sp_Inventory_Delete
    // (see Database/stored_procedures.sql).
    // GetAllItems() below is a finished example.
    // ============================================================
    public class InventoryService : IInventoryService
    {
        private readonly DbHelper _dbHelper;

        public InventoryService(DbHelper dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
        }

        public List<InventoryItem> GetAllItems()
        {
            List<InventoryItem> items = new List<InventoryItem>();
            DataTable table = _dbHelper.ExecuteQuery("sp_Inventory_GetAll");

            foreach (DataRow row in table.Rows)
            {
                items.Add(new InventoryItem
                {
                    ItemId = Convert.ToInt32(row["ItemID"]),
                    ItemName = row["ItemName"].ToString(),
                    Category = row["Category"].ToString(),
                    Quantity = Convert.ToDecimal(row["Quantity"]),
                    Unit = row["Unit"].ToString(),
                    ReorderLevel = Convert.ToDecimal(row["ReorderLevel"]),
                    UnitCost = Convert.ToDecimal(row["UnitCost"])
                });
            }

            return items;
        }

        public void AddItem(InventoryItem item)
        {
            // TODO(Inventory owner): call sp_Inventory_Insert with
            // @ItemName, @Category, @Quantity, @Unit, @ReorderLevel, @UnitCost.
            // This replaces InventoryForm's current placeholder MessageBox
            // in BtnAddItem_Click.
            throw new NotImplementedException();
        }

        public void UpdateItem(InventoryItem item)
        {
            // TODO(Inventory owner): call sp_Inventory_Update with @ItemId plus
            // the same fields as AddItem. Replaces BtnEditItem_Click's placeholder.
            throw new NotImplementedException();
        }

        public void DeleteItem(int itemId)
        {
            // TODO(Inventory owner): call sp_Inventory_Delete with @ItemId.
            // Replaces the direct dgvInventory.Rows.RemoveAt(...) call in
            // BtnDeleteItem_Click — the grid should refresh from the database
            // afterward, not just drop the row from the UI.
            throw new NotImplementedException();
        }
    }
}
