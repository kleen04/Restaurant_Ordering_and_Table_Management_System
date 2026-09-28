using System.Drawing;
using System.Windows.Forms;

public class TransparentDataGridView : DataGridView
{
    private int backgroundOpacity = 130; // 0 (invisible) to 255 (opaque)
    private Color backgroundTint = Color.Black; // Tint color

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

    public TransparentDataGridView()
    {
        // Tells Windows Forms to support transparent background colors
        this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, true);
        this.BackgroundColor = Color.Transparent;
    }

    protected override void PaintBackground(Graphics graphics, Rectangle clipBounds, Rectangle gridBounds)
    {
        // Draw the parent control's background first so we are see-through to it
        base.PaintBackground(graphics, clipBounds, gridBounds);

        // Draw our semi-transparent color tint overlay over the grid background area
        using (SolidBrush brush = new SolidBrush(Color.FromArgb(backgroundOpacity, backgroundTint)))
        {
            graphics.FillRectangle(brush, gridBounds);
        }
    }
}