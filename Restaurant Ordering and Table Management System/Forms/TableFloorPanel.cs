using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Restaurant_Ordering_and_Management_System.Models;

namespace Restaurant_Ordering_and_Management_System.Forms
{
    public class TableEventArgs : EventArgs
    {
        public RestaurantTable Table { get; }

        public TableEventArgs(RestaurantTable table)
        {
            Table = table ?? throw new ArgumentNullException(nameof(table));
        }
    }

    /// <summary>
    /// A wrapping floor of TableTile controls. It owns everything about showing tiles
    /// (building, scaling, tooltips, selection, click events) so the Tables page and the
    /// dashboard don't each re-implement it. Callers just hand it the tables to show.
    /// </summary>
    public class TableFloorPanel : FlowLayoutPanel
    {
        private readonly ToolTip _toolTip = new ToolTip();
        private TableTile _selectedTile;
        private bool _centeringRows;
        private bool _centerRows;

        public TableFloorPanel()
        {
            AutoScroll = true;
            WrapContents = true;
            FlowDirection = FlowDirection.LeftToRight;
            DoubleBuffered = true;
        }

        public event EventHandler<TableEventArgs> TableClicked;
        public event EventHandler<TableEventArgs> TableDoubleClicked;

        /// <summary>Centers each row of tiles within the available floor area.</summary>
        [DefaultValue(false)]
        public bool CenterRows
        {
            get { return _centerRows; }
            set
            {
                if (_centerRows == value)
                {
                    return;
                }

                _centerRows = value;
                PerformLayout();
            }
        }

        /// <summary>1 = full size (Tables page). Smaller values give compact tiles for the dashboard.</summary>
        [DefaultValue(1f)]
        public float TileScale { get; set; } = 1f;

        /// <summary>Gap, in pixels, around each tile.</summary>
        [DefaultValue(8)]
        public int TileSpacing { get; set; } = 8;

        /// <summary>When false, clicking raises events but nothing is highlighted as selected.</summary>
        [DefaultValue(true)]
        public bool SelectionEnabled { get; set; } = true;

        /// <summary>Optional: text to show under each table instead of its status name.</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Func<RestaurantTable, string> CaptionProvider { get; set; }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public RestaurantTable SelectedTable
        {
            get { return _selectedTile == null ? null : _selectedTile.Table; }
        }

        /// <summary>Replaces the tiles. A selected table stays selected if it still exists.</summary>
        public void SetTables(IEnumerable<RestaurantTable> tables)
        {
            if (tables == null)
            {
                throw new ArgumentNullException(nameof(tables));
            }

            int? keepSelectedId = _selectedTile == null ? (int?)null : _selectedTile.Table.TableId;
            _selectedTile = null;

            SuspendLayout();
            try
            {
                ClearTiles();
                foreach (RestaurantTable table in tables)
                {
                    TableTile tile = CreateTile(table);
                    Controls.Add(tile);

                    if (SelectionEnabled && table.TableId == keepSelectedId)
                    {
                        SelectTile(tile);
                    }
                }
            }
            finally
            {
                ResumeLayout();
            }
        }

        public void ClearSelection()
        {
            SelectTile(null);
        }

        private TableTile CreateTile(RestaurantTable table)
        {
            TableTile tile = new TableTile(table, TileScale);
            tile.Margin = new Padding(TileSpacing);

            string caption = CaptionProvider == null ? null : CaptionProvider(table);
            if (!string.IsNullOrEmpty(caption))
            {
                tile.BottomText = caption;
            }

            tile.Click += Tile_Click;
            tile.DoubleClick += Tile_DoubleClick;
            _toolTip.SetToolTip(tile, string.IsNullOrEmpty(caption) ? tile.Description : tile.Description + "\n" + caption);
            return tile;
        }

        private void ClearTiles()
        {
            while (Controls.Count > 0)
            {
                Control tile = Controls[0];
                Controls.Remove(tile);
                _toolTip.SetToolTip(tile, null);
                tile.Dispose();
            }
        }

        private void SelectTile(TableTile tile)
        {
            if (_selectedTile != null)
            {
                _selectedTile.Selected = false;
            }

            _selectedTile = tile;

            if (_selectedTile != null)
            {
                _selectedTile.Selected = true;
            }
        }

        private void Tile_Click(object sender, EventArgs e)
        {
            TableTile tile = (TableTile)sender;
            if (SelectionEnabled)
            {
                SelectTile(tile);
            }

            EventHandler<TableEventArgs> handler = TableClicked;
            if (handler != null)
            {
                handler(this, new TableEventArgs(tile.Table));
            }
        }

        private void Tile_DoubleClick(object sender, EventArgs e)
        {
            TableTile tile = (TableTile)sender;

            EventHandler<TableEventArgs> handler = TableDoubleClicked;
            if (handler != null)
            {
                handler(this, new TableEventArgs(tile.Table));
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            if (!_centerRows || _centeringRows || Controls.Count == 0)
            {
                return;
            }

            Dictionary<int, List<Control>> rows = new Dictionary<int, List<Control>>();
            foreach (Control tile in Controls)
            {
                List<Control> row;
                if (!rows.TryGetValue(tile.Top, out row))
                {
                    row = new List<Control>();
                    rows.Add(tile.Top, row);
                }
                row.Add(tile);
            }

            _centeringRows = true;
            try
            {
                foreach (List<Control> row in rows.Values)
                {
                    int left = int.MaxValue;
                    int right = int.MinValue;
                    foreach (Control tile in row)
                    {
                        left = Math.Min(left, tile.Left - tile.Margin.Left);
                        right = Math.Max(right, tile.Right + tile.Margin.Right);
                    }

                    int offset = (ClientSize.Width - (right - left)) / 2 - left;
                    foreach (Control tile in row)
                    {
                        tile.Left += offset;
                    }
                }
            }
            finally
            {
                _centeringRows = false;
            }
        }

        // Clicking the empty floor clears the selection.
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (SelectionEnabled)
            {
                SelectTile(null);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _toolTip.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
