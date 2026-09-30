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
    // OWNER: Staff module member — backs FormStaff.
    // Stored procedures live in Database/stored_procedures.sql
    // (sp_Staff_GetAll, sp_Staff_GetById, sp_Staff_Insert,
    //  sp_Staff_Update, sp_Staff_SetActive, sp_Staff_Delete).
    // ============================================================
    public class StaffService : IStaffService
    {
        private readonly DbHelper _dbHelper;

        public StaffService(DbHelper dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
        }

        public List<Staff> GetAllStaff()
        {
            List<Staff> staffList = new List<Staff>();
            DataTable table = _dbHelper.ExecuteQuery("sp_Staff_GetAll");

            foreach (DataRow row in table.Rows)
            {
                staffList.Add(MapRowToStaff(row));
            }

            return staffList;
        }

        public Staff GetStaffById(int staffId)
        {
            DataTable table = _dbHelper.ExecuteQuery("sp_Staff_GetById",
                new MySqlParameter("@p_StaffId", staffId));

            return table.Rows.Count > 0 ? MapRowToStaff(table.Rows[0]) : null;
        }

        public void AddStaff(Staff staff)
        {
            _dbHelper.ExecuteNonQuery("sp_Staff_Insert",
                new MySqlParameter("@p_FullName", staff.FullName),
                new MySqlParameter("@p_Position", staff.Position),
                new MySqlParameter("@p_ContactNumber", staff.ContactNumber),
                new MySqlParameter("@p_DateHired", staff.DateHired));
        }

        public void UpdateStaff(Staff staff)
        {
            _dbHelper.ExecuteNonQuery("sp_Staff_Update",
                new MySqlParameter("@p_StaffId", staff.StaffId),
                new MySqlParameter("@p_FullName", staff.FullName),
                new MySqlParameter("@p_Position", staff.Position),
                new MySqlParameter("@p_ContactNumber", staff.ContactNumber),
                new MySqlParameter("@p_DateHired", staff.DateHired));
        }

        public void SetStaffActive(int staffId, bool isActive)
        {
            // Deactivating keeps past orders pointing at a valid staff record.
            _dbHelper.ExecuteNonQuery("sp_Staff_SetActive",
                new MySqlParameter("@p_StaffId", staffId),
                new MySqlParameter("@p_IsActive", isActive ? 1 : 0));
        }

        public void DeleteStaff(int staffId)
        {
            // Hard delete. MySQL refuses (FK error 1451) when the staff member has
            // served orders; callers should then offer to deactivate instead.
            _dbHelper.ExecuteNonQuery("sp_Staff_Delete",
                new MySqlParameter("@p_StaffId", staffId));
        }

        private static Staff MapRowToStaff(DataRow row)
        {
            return new Staff
            {
                StaffId = Convert.ToInt32(row["StaffID"]),
                FullName = row["FullName"].ToString(),
                Position = row["Position"].ToString(),
                ContactNumber = row["ContactNumber"].ToString(),
                IsActive = Convert.ToBoolean(row["IsActive"]),
                DateHired = Convert.ToDateTime(row["DateHired"])
            };
        }
    }
}