using System;
using System.Drawing;
using Restaurant_Ordering_and_Management_System.Models;

namespace Restaurant_Ordering_and_Management_System.Helper
{
    /// <summary>
    /// The single place that decides which colour represents each table status,
    /// so the floor plan tiles and the legend can never drift apart.
    /// </summary>
    public static class TableStatusStyle
    {
        private const float TintStrength = 0.15f;

        public static readonly TableStatus[] All = (TableStatus[])Enum.GetValues(typeof(TableStatus));

        public static Color GetColor(TableStatus status)
        {
            switch (status)
            {
                case TableStatus.Available: return Color.FromArgb(30, 132, 73);
                case TableStatus.Occupied: return Color.FromArgb(192, 57, 43);
                case TableStatus.Reserved: return Color.FromArgb(211, 141, 12);
                default: return Color.Gray;
            }
        }

        /// <summary>A very light version of the status colour, used to fill the table top.</summary>
        public static Color GetTint(TableStatus status)
        {
            Color c = GetColor(status);
            return Color.FromArgb(Lighten(c.R), Lighten(c.G), Lighten(c.B));
        }

        private static int Lighten(int channel)
        {
            return (int)(255 - (255 - channel) * TintStrength);
        }
    }
}
