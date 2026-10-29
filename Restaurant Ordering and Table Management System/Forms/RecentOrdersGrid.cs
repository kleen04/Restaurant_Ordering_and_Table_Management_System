using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Restaurant_Ordering_and_Management_System.Helper;
using Restaurant_Ordering_and_Management_System.Models;

namespace Restaurant_Ordering_and_Management_System.Forms
{
    /// <summary>
    /// A read-only orders list made for glancing at: status shown as a coloured chip,
    /// "12 min ago" instead of a clock time, finished orders dimmed, and open orders that
    /// have waited too long flagged in red. Callers just give it orders to show.
    /// </summary>
    public class RecentOrdersGrid : DataGridView
    {
        private const string OrderColumn = "OrderId";
        private const string TableColumn = "Table";
        private const string StatusColumn = "Status";
        private const string PlacedColumn = "Placed";
        private const string TotalColumn = "Total";

        private static readonly Color OpenTextColor = AppTheme.TextDark;
        private static readonly Color ClosedTextColor = Color.FromArgb(150, 158, 166);
        private static readonly Color OverdueTextColor = AppTheme.Danger;

        private readonly Font _chipFont = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        private readonly Font _overdueFont = new Font("Segoe UI", 9.75F, FontStyle.Bold);

        public RecentOrdersGrid()
        {
            AllowUserToAddRows = false;
            AllowUserToDeleteRows = false;
            AllowUserToResizeRows = false;
            ReadOnly = true;
            MultiSelect = false;
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            RowHeadersVisible = false;
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            ThemeHelper.StyleGrid(this);
            ThemeHelper.StyleGridSelection(this);
            CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            RowTemplate.Height = 25;
            DefaultCellStyle.Font = new Font("Segoe UI", 9.75F);

            ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            ColumnHeadersHeight = 26;

            AddColumn(OrderColumn, "Order", 14, DataGridViewContentAlignment.MiddleLeft);
            AddColumn(TableColumn, "Table", 14, DataGridViewContentAlignment.MiddleLeft);
            AddColumn(StatusColumn, "Status", 24, DataGridViewContentAlignment.MiddleCenter);
            AddColumn(PlacedColumn, "Placed", 24, DataGridViewContentAlignment.MiddleLeft);
            AddColumn(TotalColumn, "Total", 24, DataGridViewContentAlignment.MiddleRight);
        }

        /// <summary>An open order older than this is flagged. Default: 15 minutes.</summary>
        public TimeSpan WaitWarning { get; set; } = TimeSpan.FromMinutes(15);

        public Order SelectedOrder
        {
            get { return SelectedRows.Count == 0 ? null : SelectedRows[0].Tag as Order; }
        }

        /// <summary>Shows the given orders in the order supplied. A selected order stays selected if it is still listed.</summary>
        public void SetOrders(IEnumerable<Order> orders)
        {
            if (orders == null)
            {
                throw new ArgumentNullException(nameof(orders));
            }

            int? keepSelectedId = SelectedOrder == null ? (int?)null : SelectedOrder.OrderId;
            DateTime now = DateTime.Now;

            Rows.Clear();
            foreach (Order order in orders)
            {
                int index = Rows.Add(
                    "#" + order.OrderId,
                    "Table " + order.TableId,
                    OrderStatusStyle.GetText(order.Status),
                    RelativeTime.Format(order.OrderTime, now),
                    "₱" + order.Total.ToString("N2"));

                DataGridViewRow row = Rows[index];
                row.Tag = order;
                StyleRow(row, order, now);
            }

            ClearSelection();
            if (keepSelectedId.HasValue)
            {
                foreach (DataGridViewRow row in Rows)
                {
                    Order order = row.Tag as Order;
                    if (order != null && order.OrderId == keepSelectedId.Value)
                    {
                        row.Selected = true;
                        break;
                    }
                }
            }
        }

        private void StyleRow(DataGridViewRow row, Order order, DateTime now)
        {
            row.DefaultCellStyle.ForeColor = order.IsOpen ? OpenTextColor : ClosedTextColor;

            if (order.IsOpen && now - order.OrderTime >= WaitWarning)
            {
                DataGridViewCellStyle overdue = row.Cells[PlacedColumn].Style;
                overdue.ForeColor = OverdueTextColor;
                overdue.Font = _overdueFont;
            }
        }

        private void AddColumn(string name, string header, float weight, DataGridViewContentAlignment alignment)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn
            {
                Name = name,
                HeaderText = header,
                FillWeight = weight,
                SortMode = DataGridViewColumnSortMode.NotSortable
            };
            column.DefaultCellStyle.Alignment = alignment;
            column.HeaderCell.Style.Alignment = alignment;
            Columns.Add(column);
        }

        protected override void OnCellPainting(DataGridViewCellPaintingEventArgs e)
        {
            base.OnCellPainting(e);

            if (e.Handled || e.RowIndex < 0 || e.ColumnIndex < 0 || Columns[e.ColumnIndex].Name != StatusColumn)
            {
                return;
            }

            Order order = Rows[e.RowIndex].Tag as Order;
            if (order == null)
            {
                return;
            }

            e.PaintBackground(e.CellBounds, true);
            ChipPainter.Draw(e.Graphics, e.CellBounds, OrderStatusStyle.GetText(order.Status),
                OrderStatusStyle.GetColor(order.Status), _chipFont);
            e.Handled = true;
        }

        // Right-clicking a row selects it first, so the context menu acts on the row under the mouse.
        protected override void OnCellMouseDown(DataGridViewCellMouseEventArgs e)
        {
            base.OnCellMouseDown(e);

            if (e.Button == MouseButtons.Right && e.RowIndex >= 0)
            {
                ClearSelection();
                Rows[e.RowIndex].Selected = true;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _chipFont.Dispose();
                _overdueFont.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
