using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Restaurant_Ordering_and_Management_System.DBContext;
using Restaurant_Ordering_and_Management_System.Helper;
using Restaurant_Ordering_and_Management_System.Models;
using Restaurant_Ordering_and_Management_System.Service;

namespace Restaurant_Ordering_and_Management_System.Forms
{
    public partial class FormStaff : Form
    {
        private StaffService _staffService;

        public FormStaff()
        {
            InitializeComponent();
        }

        private void FormStaff_Load(object sender, EventArgs e)
        {
            try
            {
                // Initialize services
                DatabaseConnection dbConnection = new DatabaseConnection();
                DbHelper dbHelper = new DbHelper(dbConnection);
                _staffService = new StaffService(dbHelper);

                InitializeStaffData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading staff data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeStaffData()
        {
            dgvStaff.Columns.Clear();
            dgvStaff.Columns.Add("StaffId", "Staff ID");
            dgvStaff.Columns.Add("FullName", "Full Name");
            dgvStaff.Columns.Add("Position", "Position");
            dgvStaff.Columns.Add("ContactNumber", "Contact Number");
            dgvStaff.Columns.Add("Status", "Status");
            dgvStaff.Columns.Add("DateHired", "Date Hired");

            try
            {
                // Load real data from database
                List<Staff> staffList = _staffService.GetAllStaff();

                foreach (Staff staff in staffList)
                {
                    string status = staff.IsActive ? "Active" : "Inactive";
                    dgvStaff.Rows.Add(
                        staff.StaffId,
                        staff.FullName,
                        staff.Position,
                        staff.ContactNumber ?? "N/A",
                        status,
                        staff.DateHired.ToString("yyyy-MM-dd")
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error populating staff grid: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            dgvStaff.AutoResizeColumns();
        }

        private void BtnAddStaff_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Add Staff functionality to be implemented", "Add Staff", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnEditStaff_Click(object sender, EventArgs e)
        {
            if (dgvStaff.SelectedRows.Count > 0)
            {
                MessageBox.Show("Edit Staff functionality to be implemented", "Edit Staff", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select a staff member to edit", "Edit Staff", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnToggleActive_Click(object sender, EventArgs e)
        {
            if (dgvStaff.SelectedRows.Count > 0)
            {
                MessageBox.Show("Activate/Deactivate functionality to be implemented", "Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select a staff member to update", "Update Status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnDeleteStaff_Click(object sender, EventArgs e)
        {
            if (dgvStaff.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to remove this staff member?", "Delete Staff", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    dgvStaff.Rows.RemoveAt(dgvStaff.SelectedRows[0].Index);
                }
            }
            else
            {
                MessageBox.Show("Please select a staff member to delete", "Delete Staff", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                dgvStaff.Rows.Clear();
                InitializeStaffData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing staff data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}