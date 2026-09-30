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
                items.Add(MapRowToItem(row));
            }

            return items;
        }

        public void AddItem(InventoryItem item)
        {
            _dbHelper.ExecuteNonQuery("sp_Inventory_Insert",
                new MySqlParameter("@p_ItemName", item.ItemName),
                new MySqlParameter("@p_Category", item.Category),
                new MySqlParameter("@p_Quantity", item.Quantity),
                new MySqlParameter("@p_Unit", item.Unit),
                new MySqlParameter("@p_ReorderLevel", item.ReorderLevel),
                new MySqlParameter("@p_UnitCost", item.UnitCost));
        }

        public void UpdateItem(InventoryItem item)
        {
            _dbHelper.ExecuteNonQuery("sp_Inventory_Update",
                new MySqlParameter("@p_ItemId", item.ItemId),
                new MySqlParameter("@p_ItemName", item.ItemName),
                new MySqlParameter("@p_Category", item.Category),
                new MySqlParameter("@p_Quantity", item.Quantity),
                new MySqlParameter("@p_Unit", item.Unit),
                new MySqlParameter("@p_ReorderLevel", item.ReorderLevel),
                new MySqlParameter("@p_UnitCost", item.UnitCost));
        }

        public void DeleteItem(int itemId)
        {
            _dbHelper.ExecuteNonQuery("sp_Inventory_Delete",
                new MySqlParameter("@p_ItemId", itemId));
        }

        private static InventoryItem MapRowToItem(DataRow row)
        {
            return new InventoryItem
            {
                ItemId = Convert.ToInt32(row["ItemID"]),
                ItemName = row["ItemName"].ToString(),
                Category = row["Category"].ToString(),
                Quantity = Convert.ToDecimal(row["Quantity"]),
                Unit = row["Unit"].ToString(),
                ReorderLevel = Convert.ToDecimal(row["ReorderLevel"]),
                UnitCost = Convert.ToDecimal(row["UnitCost"])
            };
        }
    }
}