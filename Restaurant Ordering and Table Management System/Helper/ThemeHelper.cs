using System.Drawing;
using System.Windows.Forms;

namespace Restaurant_Ordering_and_Management_System.Helper
{
    /// <summary>Applies AppTheme to controls that are styled in code, so every grid and dialog button looks the same.</summary>
    public static class ThemeHelper
    {
        /// <summary>Maroon header row, light peach selection and faint row banding - the same on every screen.</summary>
        public static void StyleGrid(DataGridView grid)
        {
            grid.BackgroundColor = AppTheme.Surface;
            grid.BorderStyle = BorderStyle.None;
            grid.GridColor = AppTheme.GridLine;
            grid.EnableHeadersVisualStyles = false;

            DataGridViewCellStyle header = grid.ColumnHeadersDefaultCellStyle;
            header.BackColor = AppTheme.Primary;
            header.ForeColor = AppTheme.OnPrimary;
            header.SelectionBackColor = AppTheme.Primary;
            header.SelectionForeColor = AppTheme.OnPrimary;
            header.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);

            grid.DefaultCellStyle.BackColor = AppTheme.Surface;
            grid.DefaultCellStyle.ForeColor = AppTheme.TextDark;
            grid.DefaultCellStyle.SelectionBackColor = AppTheme.AccentTint;
            grid.DefaultCellStyle.SelectionForeColor = AppTheme.TextDark;
            grid.AlternatingRowsDefaultCellStyle.BackColor = AppTheme.RowAlternate;
            grid.AlternatingRowsDefaultCellStyle.SelectionBackColor = AppTheme.AccentTint;
            grid.AlternatingRowsDefaultCellStyle.SelectionForeColor = AppTheme.TextDark;
        }

        /// <summary>Flat, borderless, white-text button in the given colour (AppTheme.Accent, Primary, Neutral or Danger).</summary>
        public static void StyleButton(Button button, Color back)
        {
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = back;
            button.ForeColor = AppTheme.OnPrimary;
            button.UseVisualStyleBackColor = false;
            button.Cursor = Cursors.Hand;
        }
    }
}
