using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Restaurant_Ordering_and_Management_System.Forms
{
    public partial class FormStaff : Form
    {
        public FormStaff()
        {
            InitializeComponent();
        }

        private void FormStaff_Load(object sender, EventArgs e)
        {
            InitializeStaffData();
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

            dgvStaff.Rows.Add("1", "Maria Santos", "Head Chef", "0917-123-4567", "Active", "2022-03-14");
            dgvStaff.Rows.Add("2", "Juan Dela Cruz", "Waiter", "0918-234-5678", "Active", "2023-06-01");
            dgvStaff.Rows.Add("3", "Ana Reyes", "Cashier", "0919-345-6789", "Active", "2021-11-20");
            dgvStaff.Rows.Add("4", "Pedro Bautista", "Waiter", "0920-456-7890", "Inactive", "2020-05-09");
            dgvStaff.Rows.Add("5", "Liza Cruz", "Manager", "0921-567-8901", "Active", "2019-01-15");

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
            dgvStaff.Rows.Clear();
            InitializeStaffData();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}