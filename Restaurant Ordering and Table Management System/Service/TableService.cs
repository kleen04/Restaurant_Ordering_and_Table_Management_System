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
    // OWNER: Table module member — backs FormTables and the
    // dashboard's dgvTableStatus grid on Form1.
    // Stored procedures: sp_Table_GetAll, sp_Table_UpdateStatus
    // (see Database/stored_procedures.sql).
    // GetAllTables() below is a finished example.
    // ============================================================
    public class TableService : ITableService
    {
        private readonly DbHelper _dbHelper;

        public TableService(DbHelper dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
        }

        public List<RestaurantTable> GetAllTables()
        {
            List<RestaurantTable> tables = new List<RestaurantTable>();
            DataTable table = _dbHelper.ExecuteQuery("sp_Table_GetAll");

            foreach (DataRow row in table.Rows)
            {
                tables.Add(new RestaurantTable
                {
                    TableId = Convert.ToInt32(row["TableID"]),
                    Capacity = Convert.ToInt32(row["Capacity"]),
                    Status = (TableStatus)Enum.Parse(typeof(TableStatus), row["Status"].ToString()),
                    CurrentGuests = Convert.ToInt32(row["CurrentGuests"])
                });
            }

            return tables;
        }

        public void UpdateTableStatus(int tableId, TableStatus status, int currentGuests)
        {
            // TODO(Table owner): call sp_Table_UpdateStatus with
            // @TableId, @Status, @CurrentGuests via _dbHelper.ExecuteNonQuery(...).
            // Call this whenever FormAddOrder seats a table or FormTables
            // marks one free again, so the dashboard grid stays accurate.
            throw new NotImplementedException();
        }
    }
}
