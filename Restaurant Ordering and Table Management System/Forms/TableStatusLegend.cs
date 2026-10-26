using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Restaurant_Ordering_and_Management_System.Helper;
using Restaurant_Ordering_and_Management_System.Models;

namespace Restaurant_Ordering_and_Management_System.Forms
{
    /// <summary>
    /// Colour key for table statuses ("Available (4)  Occupied (3) ...") built from
    /// TableStatusStyle, so it always matches the tiles. Feed it the tables to show counts.
    /// </summary>
    public class TableStatusLegend : FlowLayoutPanel
    {
        private readonly Dictionary<TableStatus, Label> _labels = new Dictionary<TableStatus, Label>();
        private readonly Label _hint = new Label();

        public TableStatusLegend()
        {
            WrapContents = false;

            foreach (TableStatus status in TableStatusStyle.All)
            {
                Controls.Add(CreateItem(status));
            }

            _hint.AutoSize = true;
            _hint.ForeColor = AppTheme.TextMuted;
            _hint.Margin = new Padding(12, 2, 0, 0);
            _hint.Visible = false;
            Controls.Add(_hint);
        }

        /// <summary>Optional grey help text shown after the colour key.</summary>
        [DefaultValue("")]
        public string Hint
        {
            get { return _hint.Text; }
            set
            {
                _hint.Text = value ?? "";
                _hint.Visible = !string.IsNullOrEmpty(_hint.Text);
            }
        }

        public void SetTables(IEnumerable<RestaurantTable> tables)
        {
            Dictionary<TableStatus, int> counts = new Dictionary<TableStatus, int>();
            foreach (TableStatus status in TableStatusStyle.All)
            {
                counts[status] = 0;
            }

            foreach (RestaurantTable table in tables)
            {
                counts[table.Status]++;
            }

            foreach (TableStatus status in TableStatusStyle.All)
            {
                _labels[status].Text = status + " (" + counts[status] + ")";
            }
        }

        private Control CreateItem(TableStatus status)
        {
            FlowLayoutPanel item = new FlowLayoutPanel
            {
                AutoSize = true,
                WrapContents = false,
                Margin = new Padding(0, 0, 18, 0)
            };

            Label label = new Label
            {
                AutoSize = true,
                Text = status.ToString(),
                Margin = new Padding(0, 2, 0, 0)
            };
            _labels[status] = label;

            item.Controls.Add(new Panel
            {
                Size = new Size(14, 14),
                BackColor = TableStatusStyle.GetColor(status),
                Margin = new Padding(0, 3, 6, 0)
            });
            item.Controls.Add(label);
            return item;
        }
    }
}
