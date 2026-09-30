using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Restaurant_Ordering_and_Management_System.Helper;
using Restaurant_Ordering_and_Management_System.Interfaces;
using Restaurant_Ordering_and_Management_System.Models;

namespace Restaurant_Ordering_and_Management_System.Forms
{
    public partial class FormTables : Form
    {
        private static readonly string[] StatusChoices = { "Available", "Occupied", "Reserved" };

        private readonly ITableService _tableService;

        public FormTables(ITableService tableService)
        {
            _tableService = tableService ?? throw new ArgumentNullException(nameof(tableService));
            InitializeComponent();
        }

        private void FormTables_Load(object sender, EventArgs e)
        {
            // Attached once here (not on every refresh) so it runs once per cell.
            dgvTables.CellFormatting += DgvTables_CellFormatting;
            LoadTables();
        }

        private void LoadTables()
        {
            dgvTables.Columns.Clear();
            dgvTables.Columns.Add("TableId", "Table #");
            dgvTables.Columns.Add("Capacity", "Capacity");
            dgvTables.Columns.Add("Status", "Status");
            dgvTables.Columns.Add("CurrentGuests", "Current Guests");

            try
            {
                foreach (RestaurantTable table in _tableService.GetAllTables())
                {
                    dgvTables.Rows.Add(table.TableId, table.Capacity, table.Status.ToString(), table.CurrentGuests);
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Error populating tables grid: " + ex.Message, "Database Error");
            }

            dgvTables.AutoResizeColumns();
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
                    e.CellStyle.ForeColor = Color.FromArgb(30, 132, 73);
                    e.CellStyle.Font = new Font(dgvTables.Font, FontStyle.Bold);
                    break;
                case "Occupied":
                    e.CellStyle.ForeColor = Color.FromArgb(192, 57, 43);
                    e.CellStyle.Font = new Font(dgvTables.Font, FontStyle.Bold);
                    break;
                case "Reserved":
                    e.CellStyle.ForeColor = Color.FromArgb(211, 141, 12);
                    e.CellStyle.Font = new Font(dgvTables.Font, FontStyle.Bold);
                    break;
            }
        }

        private RestaurantTable GetSelectedTable()
        {
            if (dgvTables.SelectedRows.Count == 0)
            {
                return null;
            }

            int tableId = Convert.ToInt32(dgvTables.SelectedRows[0].Cells["TableId"].Value);
            return _tableService.GetAllTables().Find(t => t.TableId == tableId);
        }

        // Shared by Add and Edit: shows the dialog and returns the table the user entered (or null).
        private RestaurantTable PromptForTable(string title, RestaurantTable existing)
        {
            List<DialogField> fields = new List<DialogField>
            {
                DialogField.Text("Capacity", existing == null ? "" : existing.Capacity.ToString()),
                DialogField.Choice("Status", existing == null ? "Available" : existing.Status.ToString(), StatusChoices),
                DialogField.Text("Current Guests", existing == null ? "0" : existing.CurrentGuests.ToString())
            };

            string[] values = InputDialog.Prompt(this, title, fields, ValidateTableInput);
            if (values == null)
            {
                return null;
            }

            return new RestaurantTable
            {
                TableId = existing == null ? 0 : existing.TableId,
                Capacity = int.Parse(values[0]),
                Status = (TableStatus)Enum.Parse(typeof(TableStatus), values[1]),
                CurrentGuests = int.Parse(values[2])
            };
        }

        private static string ValidateTableInput(string[] v)
        {
            int capacity;
            if (!ValidationHelper.IsPositiveInteger(v[0], out capacity))
            {
                return "Capacity must be a whole number greater than zero.";
            }

            int guests;
            if (!ValidationHelper.IsNonNegativeInteger(v[2], out guests))
            {
                return "Current guests must be zero or a positive whole number.";
            }
            if (guests > capacity)
            {
                return "Current guests cannot be more than the table's capacity.";
            }
            if (v[1] == "Available" && guests > 0)
            {
                return "An Available table cannot have guests seated. Set guests to 0 or change the status.";
            }

            return null;
        }

        private void BtnAddTable_Click(object sender, EventArgs e)
        {
            RestaurantTable table = PromptForTable("Add Table", null);
            if (table == null)
            {
                return;
            }

            try
            {
                _tableService.AddTable(table);
                LoadTables();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Could not add the table: " + ex.Message, "Add Table");
            }
        }

        private void BtnEditTable_Click(object sender, EventArgs e)
        {
            try
            {
                RestaurantTable existing = GetSelectedTable();
                if (existing == null)
                {
                    MessageHelper.ShowWarning("Please select a table to edit", "Edit Table");
                    return;
                }

                RestaurantTable edited = PromptForTable("Edit Table #" + existing.TableId, existing);
                if (edited == null)
                {
                    return;
                }

                _tableService.UpdateTable(edited);
                LoadTables();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Could not update the table: " + ex.Message, "Edit Table");
            }
        }

        private void BtnUpdateStatus_Click(object sender, EventArgs e)
        {
            try
            {
                RestaurantTable table = GetSelectedTable();
                if (table == null)
                {
                    MessageHelper.ShowWarning("Please select a table to update", "Update Status");
                    return;
                }

                List<DialogField> fields = new List<DialogField>
                {
                    DialogField.Choice("Status", table.Status.ToString(), StatusChoices),
                    DialogField.Text("Current Guests", table.CurrentGuests.ToString())
                };

                string[] values = InputDialog.Prompt(this, "Update Status - Table #" + table.TableId, fields, v =>
                    ValidateTableInput(new[] { table.Capacity.ToString(), v[0], v[1] }));
                if (values == null)
                {
                    return;
                }

                _tableService.UpdateTableStatus(table.TableId,
                    (TableStatus)Enum.Parse(typeof(TableStatus), values[0]), int.Parse(values[1]));
                LoadTables();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Could not update the status: " + ex.Message, "Update Status");
            }
        }

        private void BtnDeleteTable_Click(object sender, EventArgs e)
        {
            try
            {
                RestaurantTable table = GetSelectedTable();
                if (table == null)
                {
                    MessageHelper.ShowWarning("Please select a table to delete", "Delete Table");
                    return;
                }

                if (!MessageHelper.Confirm("Are you sure you want to delete Table #" + table.TableId + "?", "Delete Table"))
                {
                    return;
                }

                _tableService.DeleteTable(table.TableId);
                LoadTables();
            }
            catch (Exception ex)
            {
                if (DbHelper.IsForeignKeyViolation(ex))
                {
                    MessageHelper.ShowWarning("This table has order history and cannot be deleted.", "Delete Table");
                }
                else
                {
                    MessageHelper.ShowError("Could not delete the table: " + ex.Message, "Delete Table");
                }
            }
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            LoadTables();
            MessageHelper.ShowInfo("Table data refreshed successfully.", "Refresh");
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
