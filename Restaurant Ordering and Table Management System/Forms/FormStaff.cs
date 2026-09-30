using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Restaurant_Ordering_and_Management_System.Helper;
using Restaurant_Ordering_and_Management_System.Interfaces;
using Restaurant_Ordering_and_Management_System.Models;

namespace Restaurant_Ordering_and_Management_System.Forms
{
    public partial class FormStaff : Form
    {
        private readonly IStaffService _staffService;

        public FormStaff(IStaffService staffService)
        {
            _staffService = staffService ?? throw new ArgumentNullException(nameof(staffService));
            InitializeComponent();
        }

        private void FormStaff_Load(object sender, EventArgs e)
        {
            LoadStaff();
        }

        private void LoadStaff()
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
                foreach (Staff staff in _staffService.GetAllStaff())
                {
                    dgvStaff.Rows.Add(
                        staff.StaffId,
                        staff.FullName,
                        staff.Position,
                        string.IsNullOrEmpty(staff.ContactNumber) ? "N/A" : staff.ContactNumber,
                        staff.IsActive ? "Active" : "Inactive",
                        staff.DateHired.ToString("yyyy-MM-dd"));
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Error populating staff grid: " + ex.Message, "Database Error");
            }

            dgvStaff.AutoResizeColumns();
        }

        private int? GetSelectedStaffId()
        {
            if (dgvStaff.SelectedRows.Count == 0)
            {
                return null;
            }
            return Convert.ToInt32(dgvStaff.SelectedRows[0].Cells["StaffId"].Value);
        }

        // Shared by Add and Edit: shows the dialog and returns the staff record entered (or null).
        private Staff PromptForStaff(string title, Staff existing)
        {
            List<DialogField> fields = new List<DialogField>
            {
                DialogField.Text("Full Name", existing == null ? "" : existing.FullName),
                DialogField.Text("Position", existing == null ? "" : existing.Position),
                DialogField.Text("Contact Number", existing == null ? "" : existing.ContactNumber),
                DialogField.Date("Date Hired", existing == null ? DateTime.Today : existing.DateHired)
            };

            string[] values = InputDialog.Prompt(this, title, fields, ValidateStaffInput);
            if (values == null)
            {
                return null;
            }

            return new Staff
            {
                StaffId = existing == null ? 0 : existing.StaffId,
                FullName = values[0],
                Position = values[1],
                ContactNumber = values[2],
                DateHired = DateTime.Parse(values[3]),
                IsActive = existing == null || existing.IsActive
            };
        }

        private static string ValidateStaffInput(string[] v)
        {
            if (!ValidationHelper.IsRequired(v[0]))
            {
                return "Full name is required.";
            }
            if (v[0].Length > 100)
            {
                return "Full name can be at most 100 characters.";
            }
            if (!ValidationHelper.IsRequired(v[1]))
            {
                return "Position is required.";
            }
            if (v[1].Length > 50)
            {
                return "Position can be at most 50 characters.";
            }
            if (v[2].Length > 20)
            {
                return "Contact number can be at most 20 characters.";
            }

            return null;
        }

        private void BtnAddStaff_Click(object sender, EventArgs e)
        {
            Staff staff = PromptForStaff("Add Staff", null);
            if (staff == null)
            {
                return;
            }

            try
            {
                _staffService.AddStaff(staff);
                LoadStaff();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Could not add the staff member: " + ex.Message, "Add Staff");
            }
        }

        private void BtnEditStaff_Click(object sender, EventArgs e)
        {
            try
            {
                int? staffId = GetSelectedStaffId();
                if (staffId == null)
                {
                    MessageHelper.ShowWarning("Please select a staff member to edit", "Edit Staff");
                    return;
                }

                Staff existing = _staffService.GetStaffById(staffId.Value);
                if (existing == null)
                {
                    MessageHelper.ShowWarning("That staff member no longer exists. The list will be refreshed.", "Edit Staff");
                    LoadStaff();
                    return;
                }

                Staff edited = PromptForStaff("Edit Staff", existing);
                if (edited == null)
                {
                    return;
                }

                _staffService.UpdateStaff(edited);
                LoadStaff();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Could not update the staff member: " + ex.Message, "Edit Staff");
            }
        }

        private void BtnToggleActive_Click(object sender, EventArgs e)
        {
            try
            {
                int? staffId = GetSelectedStaffId();
                if (staffId == null)
                {
                    MessageHelper.ShowWarning("Please select a staff member to update", "Update Status");
                    return;
                }

                Staff staff = _staffService.GetStaffById(staffId.Value);
                if (staff == null)
                {
                    LoadStaff();
                    return;
                }

                bool activate = !staff.IsActive;
                string action = activate ? "activate" : "deactivate";
                if (!MessageHelper.Confirm("Are you sure you want to " + action + " " + staff.FullName + "?", "Update Status"))
                {
                    return;
                }

                _staffService.SetStaffActive(staff.StaffId, activate);
                LoadStaff();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Could not update the staff status: " + ex.Message, "Update Status");
            }
        }

        private void BtnDeleteStaff_Click(object sender, EventArgs e)
        {
            try
            {
                int? staffId = GetSelectedStaffId();
                if (staffId == null)
                {
                    MessageHelper.ShowWarning("Please select a staff member to delete", "Delete Staff");
                    return;
                }

                if (!MessageHelper.Confirm("Are you sure you want to delete this staff member?", "Delete Staff"))
                {
                    return;
                }

                _staffService.DeleteStaff(staffId.Value);
                LoadStaff();
            }
            catch (Exception ex)
            {
                if (DbHelper.IsForeignKeyViolation(ex))
                {
                    MessageHelper.ShowWarning(
                        "This staff member has served orders and cannot be deleted.\nUse Activate/Deactivate instead to keep the order history intact.",
                        "Delete Staff");
                }
                else
                {
                    MessageHelper.ShowError("Could not delete the staff member: " + ex.Message, "Delete Staff");
                }
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadStaff();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}