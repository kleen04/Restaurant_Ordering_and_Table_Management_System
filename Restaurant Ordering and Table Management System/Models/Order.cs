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

        public decimal Total
        {
            get
            {
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
