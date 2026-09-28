using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class FilletedPanel : Panel
{
    // The radius size of the rounded corners (higher number = more rounded)
    private int cornerRadius = 20;

    public int CornerRadius
    {
        get => cornerRadius;
        set
        {
            cornerRadius = Math.Max(1, value); // Prevent 0 or negative values
            this.Invalidate(); // Redraws the panel when the radius changes
        }
    }

    public FilletedPanel()
    {
        // Double buffering prevents flickering when resizing or rendering controls inside
        this.DoubleBuffered = true;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        // Create a rounded rectangle path matching the panel's current size
        using (GraphicsPath path = GetRoundedRectanglePath(this.ClientRectangle, cornerRadius))
        {
            // Clip the region so background colors and images don't leak past the rounded border
            this.Region = new Region(path);

            // Optional: Draw the panel's background color inside the rounded area
            using (SolidBrush brush = new SolidBrush(this.BackColor))
            {
                e.Graphics.FillPath(brush, path);
            }
        }
    }

    // Helper method to draw smooth arcs around the 4 rectangle corners
    private GraphicsPath GetRoundedRectanglePath(Rectangle rect, int radius)
    {
        GraphicsPath path = new GraphicsPath();
        int diameter = radius * 2;

        // Top-left arc
        path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
        // Top-right arc
        path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
        // Bottom-right arc
        path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
        // Bottom-left arc
        path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);

        path.CloseAllFigures();
        return path;
    }
}
