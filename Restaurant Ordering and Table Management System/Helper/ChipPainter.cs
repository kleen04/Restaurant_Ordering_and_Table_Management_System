using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Restaurant_Ordering_and_Management_System.Helper
{
    /// <summary>Paints a small coloured "pill" with white text, e.g. a status badge inside a grid cell.</summary>
    public static class ChipPainter
    {
        private const int HorizontalPadding = 9;
        private const int VerticalMargin = 4;

        public static void Draw(Graphics g, Rectangle cellBounds, string text, Color fill, Font font)
        {
            Size textSize = TextRenderer.MeasureText(g, text, font, Size.Empty, TextFormatFlags.NoPadding);

            int height = Math.Min(cellBounds.Height - 2 * VerticalMargin, textSize.Height + 4);
            int width = Math.Min(cellBounds.Width - 4, textSize.Width + 2 * HorizontalPadding);
            Rectangle chip = new Rectangle(
                cellBounds.X + (cellBounds.Width - width) / 2,
                cellBounds.Y + (cellBounds.Height - height) / 2,
                width, height);

            SmoothingMode previous = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            using (GraphicsPath pill = GraphicsHelper.RoundedRectangle(chip, chip.Height / 2f))
            using (SolidBrush brush = new SolidBrush(fill))
            {
                g.FillPath(brush, pill);
            }
            g.SmoothingMode = previous;

            TextRenderer.DrawText(g, text, font, chip, Color.White,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter |
                TextFormatFlags.NoPadding | TextFormatFlags.SingleLine);
        }
    }
}
