using System.Collections.Generic;
using Restaurant_Ordering_and_Management_System.Models;

namespace Restaurant_Ordering_and_Management_System.Interfaces
{
    public interface IOrderService
    {
        List<Order> GetRecentOrders(int count);
        Order GetOrderById(int orderId);
        int CreateOrder(Order order);
        void UpdateOrderStatus(int orderId, OrderStatus status);
    }
}
