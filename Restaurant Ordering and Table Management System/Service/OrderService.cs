using System;
using System.Collections.Generic;
using System.Data;
using MySql.Data.MySqlClient;
using Restaurant_Ordering_and_Management_System.Helper;
using Restaurant_Ordering_and_Management_System.Interfaces;
using Restaurant_Ordering_and_Management_System.Models;

namespace Restaurant_Ordering_and_Management_System.Service
{
    // ============================================================
    // OWNER: Order module member — backs FormAddOrder and the
    // dashboard's dgvRecentOrders grid on Form1.
    // Stored procedures: sp_Order_GetRecent, sp_Order_GetById,
    // sp_Order_Create, sp_OrderItem_Insert, sp_Order_UpdateStatus
    // (see Database/stored_procedures.sql).
    // GetRecentOrders() below is a finished example for the header
    // row only — note it does NOT load each order's line items,
    // that's part of the TODO in GetOrderById.
    // ============================================================
    public class OrderService : IOrderService
    {
        private readonly DbHelper _dbHelper;

        public OrderService(DbHelper dbHelper)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
        }

        public List<Order> GetRecentOrders(int count)
        {
            List<Order> orders = new List<Order>();
            DataTable table = _dbHelper.ExecuteQuery("sp_Order_GetRecent",
                new MySqlParameter("@RowCount", count));

            foreach (DataRow row in table.Rows)
            {
                orders.Add(new Order
                {
                    OrderId = Convert.ToInt32(row["OrderID"]),
                    TableId = Convert.ToInt32(row["TableID"]),
                    StaffId = Convert.ToInt32(row["StaffID"]),
                    OrderTime = Convert.ToDateTime(row["OrderTime"]),
                    Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), row["Status"].ToString())
                    // Items intentionally left empty here — this call is for the
                    // dashboard's summary grid, which doesn't need line items.
                });
            }

            return orders;
        }

        public Order GetOrderById(int orderId)
        {
            // TODO(Order owner): call sp_Order_GetById for the header row,
            // then sp_OrderItem_GetByOrderId to fill Order.Items. Used by
            // FormAddOrder when reopening an existing order.
            throw new NotImplementedException();
        }

        public int CreateOrder(Order order)
        {
            // TODO(Order owner): call sp_Order_Create with @TableId, @StaffId,
            // @Status — it should return the new OrderID (see the stored
            // procedure's OUT parameter / SELECT LAST_INSERT_ID()). Then loop
            // over order.Items and call sp_OrderItem_Insert for each one.
            // Also call TableService.UpdateTableStatus(...) to mark the table
            // Occupied — don't duplicate that logic here (DRY).
            throw new NotImplementedException();
        }

        public void UpdateOrderStatus(int orderId, OrderStatus status)
        {
            // TODO(Order owner): call sp_Order_UpdateStatus with @OrderId, @Status.
            throw new NotImplementedException();
        }
    }
}
