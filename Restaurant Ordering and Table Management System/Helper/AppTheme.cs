using System.Drawing;

namespace Restaurant_Ordering_and_Management_System.Helper
{
    /// <summary>
    /// The one place that defines the app's colours. Maroon is the brand colour (it was already
    /// the sidebar, status bar, log in and sign up screens); orange is the accent for the main
    /// action on a screen. Forms use these names instead of their own colour numbers, so the
    /// whole look can be changed here.
    /// </summary>
    public static class AppTheme
    {
        // Brand
        public static readonly Color Primary = Color.Maroon;                      // frames, headers, secondary buttons
        public static readonly Color PrimaryDark = Color.FromArgb(64, 0, 0);      // cards on a maroon background
        public static readonly Color Accent = Color.FromArgb(255, 107, 53);       // main action on a screen (Add, Save, Export...)

        // Screen headers - one value so every form's title bar matches.
        public static readonly Color Header = Primary;
        public static readonly Color OnPrimary = Color.White;                     // text on Primary / Header
        public static readonly Color OnPrimaryMuted = Color.FromArgb(255, 224, 214); // subtitle text on Primary / Header

        // Highlights (selected rows and tiles, hover)
        public static readonly Color AccentTint = Color.FromArgb(255, 237, 227);
        public static readonly Color AccentTintLight = Color.FromArgb(255, 246, 241);

        // Buttons that are not Primary / Accent
        public static readonly Color Danger = Color.FromArgb(192, 0, 0);          // Delete, Remove, Logout
        public static readonly Color Neutral = Color.FromArgb(108, 117, 125);     // Refresh, Print, Return, Close

        // Surfaces and text
        public static readonly Color Surface = Color.White;
        public static readonly Color Page = Color.FromArgb(245, 245, 245);        // screen background
        public static readonly Color Bar = Color.FromArgb(240, 240, 240);         // bottom button bars
        public static readonly Color Floor = Color.FromArgb(250, 246, 238);       // restaurant floor behind the table tiles
        public static readonly Color GridLine = Color.FromArgb(230, 230, 230);
        public static readonly Color RowAlternate = Color.FromArgb(253, 248, 245);
        public static readonly Color TextDark = Color.FromArgb(51, 51, 51);
        public static readonly Color TextMuted = Color.FromArgb(108, 117, 125);
    }
}
