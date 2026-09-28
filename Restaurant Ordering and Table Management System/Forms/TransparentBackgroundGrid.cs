using System;
using System.Drawing;
using System.Windows.Forms;

public class TransparentBackgroundGrid : DataGridView
{
    private int backgroundOpacity = 130; // 0 to 255 (approx 50% opacity)
    private Color backgroundTint = Color.Black;

    public int BackgroundOpacity
    {
        get => backgroundOpacity;
        set { backgroundOpacity = value; Invalidate(); }
    }

    public Color BackgroundTint
    {
        get => backgroundTint;
        set { backgroundTint = value; Invalidate(); }
    }

    public TransparentBackgroundGrid()
    {
        // 1. We remove ControlStyles.UserPaint here to stop the crash.
        // Instead, we just enable double-buffering to prevent flickering.
        this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

        // 2. Set BackgroundColor to a SOLID dummy color so it doesn't crash.
        // Our PaintBackground override will completely ignore this color anyway.
        base.BackgroundColor = Color.DarkGray;
    }

    protected override void PaintBackground(Graphics graphics, Rectangle clipBounds, Rectangle gridBounds)
    {
        // 1. Ask the parent container (the Form or Panel) to draw itself where the grid is.
        // This brings the background form/image into our grid area.
        if (this.Parent != null)
        {
            using (var brush = new SolidBrush(this.Parent.BackColor))
            {
                graphics.FillRectangle(brush, gridBounds);
            }

            // If your parent has a background image, draw it here instead
            if (this.Parent.BackgroundImage != null)
            {
                graphics.DrawImage(this.Parent.BackgroundImage, gridBounds, this.Bounds, GraphicsUnit.Pixel);
            }
        }

        // 2. Lay down our custom semi-transparent color tint overlay over the area.
        using (SolidBrush brush = new SolidBrush(Color.FromArgb(backgroundOpacity, backgroundTint)))
        {
            graphics.FillRectangle(brush, gridBounds);
        }
    }
}
