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
    //  sp_Staff_Update, sp_Staff_Delete).
    // GetAllStaff() below is a finished example — copy its shape
    // for the other four methods.
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
            // TODO(Staff owner): call sp_Staff_GetById with @StaffId,
            // then return MapRowToStaff(table.Rows[0]) (or null if no rows).
            throw new NotImplementedException();
        }

        public void AddStaff(Staff staff)
        {
            // TODO(Staff owner): call sp_Staff_Insert with
            // @FullName, @Position, @ContactNumber, @DateHired via
            // _dbHelper.ExecuteNonQuery(...).
            throw new NotImplementedException();
        }

        public void UpdateStaff(Staff staff)
        {
            // TODO(Staff owner): call sp_Staff_Update with @StaffId plus
            // the same fields as AddStaff.
            throw new NotImplementedException();
        }

        public void DeleteStaff(int staffId)
        {
            // TODO(Staff owner): call sp_Staff_Delete with @StaffId.
            // Prefer a soft delete (IsActive = 0) over a hard DELETE so
            // past orders still show who served them.
            throw new NotImplementedException();
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
