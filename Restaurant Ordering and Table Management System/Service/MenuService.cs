using System;
using System.Collections.Generic;
using System.Data;
using Restaurant_Ordering_and_Management_System.Helper;
using Restaurant_Ordering_and_Management_System.Interfaces;
using Restaurant_Ordering_and_Management_System.Models;

namespace Restaurant_Ordering_and_Management_System.Service
{
    // ============================================================
    // OWNER: Order module member — backs FormAddOrder's menu grid.
    // Stored procedure: sp_MenuItem_GetAll
    // (see Database/stored_procedures.sql).
    // ============================================================
    public class MenuService : IMenuService
    {
        private readonly DbHelper _dbHelper;

        public MenuService(DbHelper dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
        }

        public List<MenuItem> GetAllMenuItems()
        {
            List<MenuItem> items = new List<MenuItem>();
            DataTable table = _dbHelper.ExecuteQuery("sp_MenuItem_GetAll");

            foreach (DataRow row in table.Rows)
            {
                items.Add(new MenuItem
                {
                    MenuItemId = Convert.ToInt32(row["MenuItemID"]),
                    Name = row["Name"].ToString(),
                    Category = row["Category"].ToString(),
                    Price = Convert.ToDecimal(row["Price"]),
                    IsAvailable = Convert.ToBoolean(row["IsAvailable"])
                });
            }

            return items;
        }
    }
}