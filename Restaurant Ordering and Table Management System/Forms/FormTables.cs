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
    public partial class FormTables : Form
    {
        private TableService _tableService;

        public FormTables()
        {
            InitializeComponent();
        }

        private void FormTables_Load(object sender, EventArgs e)
        {
            try
            {
                // Initialize services
                DatabaseConnection dbConnection = new DatabaseConnection();
                DbHelper dbHelper = new DbHelper(dbConnection);
                _tableService = new TableService(dbHelper);

                InitializeTableData();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading table data: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void InitializeTableData()
        {
            dgvTables.Columns.Clear();
            dgvTables.Columns.Add("TableId", "Table #");
            dgvTables.Columns.Add("Capacity", "Capacity");
            dgvTables.Columns.Add("Status", "Status");
            dgvTables.Columns.Add("CurrentGuests", "Current Guests");

            try
            {
                // Load real data from database
                List<RestaurantTable> tables = _tableService.GetAllTables();

                foreach (RestaurantTable table in tables)
                {
                    dgvTables.Rows.Add(
                        table.TableId,
                        table.Capacity,
                        table.Status.ToString(),
                        table.CurrentGuests
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error populating tables grid: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            dgvTables.AutoResizeColumns();
            dgvTables.CellFormatting += DgvTables_CellFormatting;
        }

        private void DgvTables_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvTables.Columns[e.ColumnIndex].Name != "Status" || e.Value == null)
            {
                return;
            }

            switch (e.Value.ToString())
            {
                case "Available":
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(30, 132, 73);
                    e.CellStyle.Font = new System.Drawing.Font(dgvTables.Font, System.Drawing.FontStyle.Bold);
                    break;
                case "Occupied":
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(192, 57, 43);
                    e.CellStyle.Font = new System.Drawing.Font(dgvTables.Font, System.Drawing.FontStyle.Bold);
                    break;
                case "Reserved":
                    e.CellStyle.ForeColor = System.Drawing.Color.FromArgb(211, 141, 12);
                    e.CellStyle.Font = new System.Drawing.Font(dgvTables.Font, System.Drawing.FontStyle.Bold);
                    break;
            }
        }

        private void BtnAddTable_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Add Table functionality to be implemented", "Add Table", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnEditTable_Click(object sender, EventArgs e)
        {
            if (dgvTables.SelectedRows.Count > 0)
            {
                MessageBox.Show("Edit Table functionality to be implemented", "Edit Table", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select a table to edit", "Edit Table", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnUpdateStatus_Click(object sender, EventArgs e)
        {
            if (dgvTables.SelectedRows.Count > 0)
            {
                MessageBox.Show("Update Status functionality to be implemented", "Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select a table to update", "Update Status", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnDeleteTable_Click(object sender, EventArgs e)
        {
            if (dgvTables.SelectedRows.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure you want to delete this table?", "Delete Table", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    dgvTables.Rows.RemoveAt(dgvTables.SelectedRows[0].Index);
                }
            }
            else
            {
                MessageBox.Show("Please select a table to delete", "Delete Table", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                dgvTables.Rows.Clear();
                InitializeTableData();
                MessageBox.Show("Table data refreshed successfully.", "Refresh", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing table data: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
