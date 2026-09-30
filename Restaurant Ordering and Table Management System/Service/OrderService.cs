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
    // sp_Order_Create, sp_OrderItem_Insert, sp_OrderItem_GetByOrderId,
    // sp_Order_UpdateStatus (see Database/stored_procedures.sql).
    // ============================================================
    public class OrderService : IOrderService
    {
        private readonly DbHelper _dbHelper;
        private readonly ITableService _tableService;

        public OrderService(DbHelper dbHelper, ITableService tableService)
        {
            _dbHelper = dbHelper ?? throw new ArgumentNullException(nameof(dbHelper));
            _tableService = tableService ?? throw new ArgumentNullException(nameof(tableService));
        }

        public List<Order> GetRecentOrders(int count)
        {
            List<Order> orders = new List<Order>();
            DataTable table = _dbHelper.ExecuteQuery("sp_Order_GetRecent",
                new MySqlParameter("@p_RowCount", count));

            foreach (DataRow row in table.Rows)
            {
                // Items are intentionally left empty here — the list only needs
                // the Total that the stored procedure already calculated.
                orders.Add(MapRowToOrder(row));
            }

            return orders;
        }

        public Order GetOrderById(int orderId)
        {
            DataTable header = _dbHelper.ExecuteQuery("sp_Order_GetById",
                new MySqlParameter("@p_OrderId", orderId));

            if (header.Rows.Count == 0)
            {
                return null;
            }

            Order order = MapRowToOrder(header.Rows[0]);

            DataTable items = _dbHelper.ExecuteQuery("sp_OrderItem_GetByOrderId",
                new MySqlParameter("@p_OrderId", orderId));

            foreach (DataRow row in items.Rows)
            {
                order.Items.Add(new OrderItem
                {
                    OrderItemId = Convert.ToInt32(row["OrderItemID"]),
                    OrderId = Convert.ToInt32(row["OrderID"]),
                    MenuItemId = Convert.ToInt32(row["MenuItemID"]),
                    MenuItemName = row["MenuItemName"].ToString(),
                    Quantity = Convert.ToInt32(row["Quantity"]),
                    UnitPrice = Convert.ToDecimal(row["UnitPrice"])
                });
            }

            return order;
        }

        public int CreateOrder(Order order)
        {
            if (order == null)
            {
                throw new ArgumentNullException(nameof(order));
            }
            if (order.Items.Count == 0)
            {
                throw new InvalidOperationException("An order needs at least one item.");
            }

            RestaurantTable table = _tableService.GetAllTables().Find(t => t.TableId == order.TableId);
            if (table == null)
            {
                throw new InvalidOperationException("The selected table does not exist.");
            }
            if (table.Status == TableStatus.Occupied)
            {
                throw new InvalidOperationException("Table " + table.TableId + " is already occupied.");
            }

            int guests = order.GuestCount > 0 ? order.GuestCount : 1;
            int newOrderId = 0;

            // The order header, every line item and the table's Occupied status are
            // saved together: if any step fails, none of them are kept.
            // (sp_Order_Create marks the table Occupied itself.)
            _dbHelper.ExecuteInTransaction(tx =>
            {
                MySqlParameter outId = new MySqlParameter("@p_NewOrderId", MySqlDbType.Int32)
                {
                    Direction = ParameterDirection.Output
                };

                tx.ExecuteNonQuery("sp_Order_Create",
                    new MySqlParameter("@p_TableId", order.TableId),
                    new MySqlParameter("@p_StaffId", order.StaffId),
                    new MySqlParameter("@p_Guests", guests),
                    outId);

                newOrderId = Convert.ToInt32(outId.Value);

                foreach (OrderItem item in order.Items)
                {
                    tx.ExecuteNonQuery("sp_OrderItem_Insert",
                        new MySqlParameter("@p_OrderId", newOrderId),
                        new MySqlParameter("@p_MenuItemId", item.MenuItemId),
                        new MySqlParameter("@p_Quantity", item.Quantity),
                        new MySqlParameter("@p_UnitPrice", item.UnitPrice));
                }
            });

            return newOrderId;
        }

        public void UpdateOrderStatus(int orderId, OrderStatus status)
        {
            Order existing = GetOrderById(orderId);
            if (existing == null)
            {
                throw new InvalidOperationException("Order #" + orderId + " was not found.");
            }
            if (existing.Status == OrderStatus.Completed || existing.Status == OrderStatus.Cancelled)
            {
                throw new InvalidOperationException(
                    "Order #" + orderId + " is already " + existing.Status + " and can no longer be changed.");
            }

            _dbHelper.ExecuteNonQuery("sp_Order_UpdateStatus",
                new MySqlParameter("@p_OrderId", orderId),
                new MySqlParameter("@p_Status", status.ToString()));

            // A finished or cancelled order frees its table again.
            if (status == OrderStatus.Completed || status == OrderStatus.Cancelled)
            {
                _tableService.UpdateTableStatus(existing.TableId, TableStatus.Available, 0);
            }
        }

        private static Order MapRowToOrder(DataRow row)
        {
            Order order = new Order
            {
                OrderId = Convert.ToInt32(row["OrderID"]),
                TableId = Convert.ToInt32(row["TableID"]),
                StaffId = Convert.ToInt32(row["StaffID"]),
                OrderTime = Convert.ToDateTime(row["OrderTime"]),
                Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), row["Status"].ToString())
            };

            if (row.Table.Columns.Contains("Total") && row["Total"] != DBNull.Value)
            {
                order.SummaryTotal = Convert.ToDecimal(row["Total"]);
            }

            return order;
        }
    }
}
