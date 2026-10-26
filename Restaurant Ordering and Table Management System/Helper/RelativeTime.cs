using System;

namespace Restaurant_Ordering_and_Management_System.Helper
{
    /// <summary>Turns a timestamp into the short "12 min ago" wording used on the dashboard.</summary>
    public static class RelativeTime
    {
        public static string Format(DateTime time, DateTime now)
        {
            TimeSpan age = now - time;
            if (age < TimeSpan.Zero)
            {
                age = TimeSpan.Zero; // a slightly-ahead clock should not show negative minutes
            }

            if (age.TotalMinutes < 1)
            {
                return "Just now";
            }
            if (age.TotalMinutes < 60)
            {
                return (int)age.TotalMinutes + " min ago";
            }
            if (age.TotalHours < 24)
            {
                int hours = (int)age.TotalHours;
                return hours + (hours == 1 ? " hr ago" : " hrs ago");
            }

            return time.Date == now.Date.AddDays(-1) ? "Yesterday" : time.ToString("MMM d");
        }
    }
}
