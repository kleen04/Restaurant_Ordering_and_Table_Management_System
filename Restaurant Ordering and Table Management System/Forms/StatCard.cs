using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using Restaurant_Ordering_and_Management_System.Helper;

namespace Restaurant_Ordering_and_Management_System.Forms
{
    /// <summary>A rounded dashboard card: one big coloured number, a caption under it, and a colour stripe on the left.</summary>
    public class StatCard : FilletedPanel
    {
        private const int StripeWidth = 6;
        private const int TextLeft = 18;

        private static readonly Color CaptionColor = AppTheme.TextMuted;

        private string _value = "0";
        private string _caption = "";
        private Color _accentColor = AppTheme.Accent;

        public StatCard()
        {
            BackColor = AppTheme.Surface;
            CornerRadius = 14;
            ResizeRedraw = true;
        }

        [DefaultValue("0")]
        public string Value
        {
            get { return _value; }
            set
            {
                _value = value ?? "";
                Invalidate();
            }
        }

        [DefaultValue("")]
        public string Caption
        {
            get { return _caption; }
            set
            {
                _caption = value ?? "";
                Invalidate();
            }
        }

        public Color AccentColor
        {
            get { return _accentColor; }
            set
            {
                _accentColor = value;
                Invalidate();
            }
        }

        private bool ShouldSerializeAccentColor()
        {
            return _accentColor != AppTheme.Accent;
        }

        private void ResetAccentColor()
        {
            AccentColor = AppTheme.Accent;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;

            using (SolidBrush stripe = new SolidBrush(_accentColor))
            {
                g.FillRectangle(stripe, 0, 0, StripeWidth, Height);
            }

            const TextFormatFlags flags = TextFormatFlags.Left | TextFormatFlags.VerticalCenter |
                                          TextFormatFlags.NoPadding | TextFormatFlags.SingleLine |
                                          TextFormatFlags.EndEllipsis;
            int textWidth = Width - TextLeft - 8;
            int valueHeight = Height * 3 / 5;

            using (Font valueFont = FitValueFont(g, textWidth))
            {
                TextRenderer.DrawText(g, _value, valueFont, new Rectangle(TextLeft, 2, textWidth, valueHeight),
                    _accentColor, flags);
            }

            using (Font captionFont = new Font("Segoe UI", 9.5F))
            {
                TextRenderer.DrawText(g, _caption, captionFont,
                    new Rectangle(TextLeft, valueHeight, textWidth, Height - valueHeight - 4), CaptionColor, flags);
            }
        }

        // Large numbers (e.g. big peso totals) shrink a little rather than getting cut off.
        private Font FitValueFont(Graphics g, int maxWidth)
        {
            for (float size = 22F; size > 12F; size -= 1F)
            {
                Font font = new Font("Segoe UI", size, FontStyle.Bold);
                if (TextRenderer.MeasureText(g, _value, font, Size.Empty, TextFormatFlags.NoPadding).Width <= maxWidth)
                {
                    return font;
                }
                font.Dispose();
            }
            return new Font("Segoe UI", 12F, FontStyle.Bold);
        }
    }
}
