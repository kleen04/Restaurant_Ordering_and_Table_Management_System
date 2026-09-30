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
    // Stored procedures: sp_Table_GetAll, sp_Table_Insert, sp_Table_Update,
    // sp_Table_UpdateStatus, sp_Table_Delete (see Database/stored_procedures.sql).
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

        public void AddTable(RestaurantTable table)
        {
            _dbHelper.ExecuteNonQuery("sp_Table_Insert",
                new MySqlParameter("@p_Capacity", table.Capacity),
                new MySqlParameter("@p_Status", table.Status.ToString()),
                new MySqlParameter("@p_CurrentGuests", table.CurrentGuests));
        }

        public void UpdateTable(RestaurantTable table)
        {
            _dbHelper.ExecuteNonQuery("sp_Table_Update",
                new MySqlParameter("@p_TableId", table.TableId),
                new MySqlParameter("@p_Capacity", table.Capacity),
                new MySqlParameter("@p_Status", table.Status.ToString()),
                new MySqlParameter("@p_CurrentGuests", table.CurrentGuests));
        }

        public void DeleteTable(int tableId)
        {
            _dbHelper.ExecuteNonQuery("sp_Table_Delete",
                new MySqlParameter("@p_TableId", tableId));
        }

        public void UpdateTableStatus(int tableId, TableStatus status, int currentGuests)
        {
            _dbHelper.ExecuteNonQuery("sp_Table_UpdateStatus",
                new MySqlParameter("@p_TableId", tableId),
                new MySqlParameter("@p_Status", status.ToString()),
                new MySqlParameter("@p_CurrentGuests", currentGuests));
        }
    }
}