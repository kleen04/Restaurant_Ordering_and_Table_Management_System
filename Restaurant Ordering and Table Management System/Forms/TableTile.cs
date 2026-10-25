using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using Restaurant_Ordering_and_Management_System.Helper;
using Restaurant_Ordering_and_Management_System.Models;

namespace Restaurant_Ordering_and_Management_System.Forms
{
    /// <summary>
    /// Draws one dining table as seen from above: a round or rectangular table top,
    /// one chair per seat, and a filled chair for every seated guest.
    /// Display only - choosing a table and acting on it is the host form's job.
    /// </summary>
    public class TableTile : Control
    {
        private const int TileSize = 150;
        private const int TableCenterY = 64;

        private const int ChairSize = 14;
        private const int ChairGap = 3;
        private const int MaxChairsDrawn = 12;

        private const int RoundTableMaxSeats = 4;
        private const int RoundDiameter = 76;

        private const int RectHeight = 60;
        private const int RectMinWidth = 84;
        private const int RectMaxWidth = 104;
        private const int RectWidthPerSeat = 18;
        private const int RectBaseWidth = 40;
        private const int SeatsBeforeEndChairs = 6;

        private static readonly Color EmptyChairColor = Color.FromArgb(189, 195, 199);
        private static readonly Color HoverBackColor = Color.FromArgb(240, 248, 250);
        private static readonly Color SelectedBackColor = Color.FromArgb(209, 240, 245);
        private static readonly Color SelectedBorderColor = Color.FromArgb(23, 162, 184);
        private static readonly Color PrimaryTextColor = Color.FromArgb(44, 62, 80);
        private static readonly Color SecondaryTextColor = Color.FromArgb(108, 117, 125);

        private readonly Font _idFont = new Font("Segoe UI", 11F, FontStyle.Bold);
        private readonly Font _infoFont = new Font("Segoe UI", 9F);
        private readonly Font _statusFont = new Font("Segoe UI", 8.5F, FontStyle.Bold);
        private readonly StringFormat _centered = new StringFormat
        {
            Alignment = StringAlignment.Center,
            LineAlignment = StringAlignment.Center
        };

        private bool _selected;
        private bool _hovered;

        public RestaurantTable Table { get; }

        public TableTile(RestaurantTable table)
        {
            Table = table ?? throw new ArgumentNullException(nameof(table));

            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);

            Size = new Size(TileSize, TileSize);
            Margin = new Padding(8);
            Cursor = Cursors.Hand;
            TabStop = false;
            AccessibleName = string.Format("Table {0}, {1}, {2} of {3} seated",
                table.TableId, table.Status, table.CurrentGuests, table.Capacity);
        }

        public bool Selected
        {
            get { return _selected; }
            set
            {
                if (_selected == value)
                {
                    return;
                }
                _selected = value;
                Invalidate();
            }
        }

        private bool IsRound
        {
            get { return Table.Capacity <= RoundTableMaxSeats; }
        }

        private int SeatsDrawn
        {
            get { return Math.Min(Table.Capacity, MaxChairsDrawn); }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _hovered = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _hovered = false;
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.Clear(Parent != null ? Parent.BackColor : BackColor);

            Color statusColor = TableStatusStyle.GetColor(Table.Status);
            RectangleF tableBounds = GetTableBounds();

            PaintBackdrop(g);
            PaintChairs(g, tableBounds, statusColor);
            PaintTableTop(g, tableBounds, statusColor);
            PaintLabels(g, tableBounds, statusColor);
        }

        // ---- painting ------------------------------------------------------

        private void PaintBackdrop(Graphics g)
        {
            if (!_selected && !_hovered)
            {
                return;
            }

            RectangleF area = new RectangleF(1, 1, Width - 3, Height - 3);
            using (GraphicsPath path = GraphicsHelper.RoundedRectangle(area, 14))
            using (SolidBrush fill = new SolidBrush(_selected ? SelectedBackColor : HoverBackColor))
            {
                g.FillPath(fill, path);
                if (_selected)
                {
                    using (Pen border = new Pen(SelectedBorderColor, 2f))
                    {
                        g.DrawPath(border, path);
                    }
                }
            }
        }

        private void PaintChairs(Graphics g, RectangleF tableBounds, Color statusColor)
        {
            List<PointF> centers = GetChairCenters(tableBounds);
            int seated = Math.Min(Table.CurrentGuests, centers.Count);

            using (SolidBrush taken = new SolidBrush(statusColor))
            using (SolidBrush empty = new SolidBrush(EmptyChairColor))
            {
                for (int i = 0; i < centers.Count; i++)
                {
                    RectangleF chair = new RectangleF(
                        centers[i].X - ChairSize / 2f, centers[i].Y - ChairSize / 2f, ChairSize, ChairSize);

                    using (GraphicsPath path = GraphicsHelper.RoundedRectangle(chair, 4))
                    {
                        g.FillPath(i < seated ? taken : empty, path);
                    }
                }
            }
        }

        private void PaintTableTop(Graphics g, RectangleF tableBounds, Color statusColor)
        {
            using (GraphicsPath shape = CreateTableShape(tableBounds))
            using (SolidBrush fill = new SolidBrush(TableStatusStyle.GetTint(Table.Status)))
            using (Pen border = new Pen(statusColor, 2.5f))
            {
                g.FillPath(fill, shape);
                g.DrawPath(border, shape);
            }
        }

        private void PaintLabels(Graphics g, RectangleF tableBounds, Color statusColor)
        {
            float centerY = tableBounds.Top + tableBounds.Height / 2f;

            DrawCentered(g, "T" + Table.TableId, _idFont, PrimaryTextColor,
                new RectangleF(tableBounds.X, centerY - 20, tableBounds.Width, 20));

            DrawCentered(g, Table.CurrentGuests + "/" + Table.Capacity, _infoFont, SecondaryTextColor,
                new RectangleF(tableBounds.X, centerY, tableBounds.Width, 18));

            DrawCentered(g, Table.Status.ToString(), _statusFont, statusColor,
                new RectangleF(0, Height - 24, Width, 18));
        }

        private void DrawCentered(Graphics g, string text, Font font, Color color, RectangleF bounds)
        {
            using (SolidBrush brush = new SolidBrush(color))
            {
                g.DrawString(text, font, brush, bounds, _centered);
            }
        }

        // ---- geometry ------------------------------------------------------

        private GraphicsPath CreateTableShape(RectangleF bounds)
        {
            if (IsRound)
            {
                GraphicsPath ellipse = new GraphicsPath();
                ellipse.AddEllipse(bounds);
                return ellipse;
            }
            return GraphicsHelper.RoundedRectangle(bounds, 12);
        }

        private RectangleF GetTableBounds()
        {
            float centerX = Width / 2f;

            if (IsRound)
            {
                return new RectangleF(centerX - RoundDiameter / 2f, TableCenterY - RoundDiameter / 2f,
                    RoundDiameter, RoundDiameter);
            }

            int width = Math.Max(RectMinWidth, Math.Min(RectMaxWidth,
                RectBaseWidth + RectWidthPerSeat * GetTopRowSeats()));
            return new RectangleF(centerX - width / 2f, TableCenterY - RectHeight / 2f, width, RectHeight);
        }

        private List<PointF> GetChairCenters(RectangleF table)
        {
            return IsRound ? GetRoundChairCenters(table) : GetRectangularChairCenters(table);
        }

        private List<PointF> GetRoundChairCenters(RectangleF table)
        {
            List<PointF> centers = new List<PointF>();
            float orbit = table.Width / 2f + ChairGap + ChairSize / 2f;
            float cx = table.Left + table.Width / 2f;
            float cy = table.Top + table.Height / 2f;

            for (int i = 0; i < SeatsDrawn; i++)
            {
                double angle = -Math.PI / 2 + 2 * Math.PI * i / SeatsDrawn;
                centers.Add(new PointF(cx + (float)(orbit * Math.Cos(angle)), cy + (float)(orbit * Math.Sin(angle))));
            }
            return centers;
        }

        private List<PointF> GetRectangularChairCenters(RectangleF table)
        {
            List<PointF> centers = new List<PointF>();
            float offset = ChairGap + ChairSize / 2f;
            int topSeats = GetTopRowSeats();
            int bottomSeats = GetSideSeats() - topSeats;

            AddRow(centers, table.Left, table.Width, table.Top - offset, topSeats);
            AddRow(centers, table.Left, table.Width, table.Bottom + offset, bottomSeats);

            if (GetEndSeats() > 0)
            {
                float middle = table.Top + table.Height / 2f;
                centers.Add(new PointF(table.Left - offset, middle));
                centers.Add(new PointF(table.Right + offset, middle));
            }
            return centers;
        }

        private static void AddRow(List<PointF> centers, float left, float width, float y, int count)
        {
            for (int i = 0; i < count; i++)
            {
                centers.Add(new PointF(left + width * (i + 1) / (count + 1), y));
            }
        }

        // Long tables seat one guest at each end; everyone else sits along the two long sides.
        private int GetEndSeats()
        {
            return SeatsDrawn >= SeatsBeforeEndChairs ? 2 : 0;
        }

        private int GetSideSeats()
        {
            return SeatsDrawn - GetEndSeats();
        }

        private int GetTopRowSeats()
        {
            return (GetSideSeats() + 1) / 2;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _idFont.Dispose();
                _infoFont.Dispose();
                _statusFont.Dispose();
                _centered.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
