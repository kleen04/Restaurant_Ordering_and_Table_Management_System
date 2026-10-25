using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Restaurant_Ordering_and_Management_System.Helper;

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
        using (GraphicsPath path = GraphicsHelper.RoundedRectangle(this.ClientRectangle, cornerRadius))
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
}
