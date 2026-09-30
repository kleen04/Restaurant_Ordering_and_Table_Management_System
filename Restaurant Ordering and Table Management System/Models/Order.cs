using System;
using System.Collections.Generic;

namespace Restaurant_Ordering_and_Management_System.Models
{
    public enum OrderStatus
    {
        Pending,
        InProgress,
        Completed,
        Cancelled
    }

    public class Order
    {
        public int OrderId { get; set; }
        public int TableId { get; set; }
        public int StaffId { get; set; }
        public DateTime OrderTime { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        /// <summary>Number of guests being seated with this order. Not stored on the
        /// Orders table; it is written to RestaurantTables.CurrentGuests when the
        /// order is created.</summary>
        public int GuestCount { get; set; }

        /// <summary>Total supplied by the database for list views (sp_Order_GetRecent),
        /// where the line items are not loaded.</summary>
        public decimal SummaryTotal { get; set; }

        /// <summary>Sum of the line items when they are loaded; otherwise the
        /// database-supplied summary total.</summary>
        public decimal Total
        {
            get
            {
                if (Items.Count == 0)
                {
                    return SummaryTotal;
                }

                decimal total = 0m;
                foreach (OrderItem item in Items)
                {
                    total += item.Subtotal;
                }
                return total;
            }
        }
    }
}
