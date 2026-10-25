using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace Restaurant_Ordering_and_Management_System.Helper
{
    /// <summary>Shared drawing routines so custom controls don't each re-implement them.</summary>
    public static class GraphicsHelper
    {
        /// <summary>Builds a rounded-rectangle path. The caller disposes the returned path.</summary>
        public static GraphicsPath RoundedRectangle(RectangleF rect, float radius)
        {
            float diameter = Math.Min(radius * 2, Math.Min(rect.Width, rect.Height));

            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseAllFigures();
            return path;
        }
    }
}
