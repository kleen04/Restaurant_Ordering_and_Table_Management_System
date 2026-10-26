using System.Drawing;
using Restaurant_Ordering_and_Management_System.Models;

namespace Restaurant_Ordering_and_Management_System.Helper
{
    /// <summary>
    /// The single place that decides how each order status is named and coloured,
    /// the counterpart of TableStatusStyle for orders.
    /// </summary>
    public static class OrderStatusStyle
    {
        public static Color GetColor(OrderStatus status)
        {
            switch (status)
            {
                case OrderStatus.Pending: return Color.FromArgb(211, 141, 12);
                case OrderStatus.InProgress: return Color.FromArgb(52, 152, 219);
                case OrderStatus.Completed: return Color.FromArgb(30, 132, 73);
                case OrderStatus.Cancelled: return Color.FromArgb(127, 140, 141);
                default: return Color.Gray;
            }
        }

        public static string GetText(OrderStatus status)
        {
            return status == OrderStatus.InProgress ? "In Progress" : status.ToString();
        }
    }
}
