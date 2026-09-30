using Restaurant_Ordering_and_Management_System.DBContext;
using Restaurant_Ordering_and_Management_System.Helper;
using Restaurant_Ordering_and_Management_System.Interfaces;

namespace Restaurant_Ordering_and_Management_System.Service
{
    /// <summary>
    /// The one place where concrete classes are created and wired together
    /// (composition root). Forms receive the INTERFACES they need through their
    /// constructors and never create a DatabaseConnection, DbHelper or service
    /// themselves, so each form depends on abstractions only.
    /// </summary>
    public class AppServices
    {
        public ITableService Tables { get; }
        public IOrderService Orders { get; }
        public IMenuService Menu { get; }
        public IStaffService Staff { get; }
        public IInventoryService Inventory { get; }
        public IReportService Reports { get; }

        public AppServices(ITableService tables, IOrderService orders, IMenuService menu,
                           IStaffService staff, IInventoryService inventory, IReportService reports)
        {
            Tables = tables;
            Orders = orders;
            Menu = menu;
            Staff = staff;
            Inventory = inventory;
            Reports = reports;
        }

        /// <summary>Builds the real, database-backed services once at start-up.</summary>
        public static AppServices CreateDefault()
        {
            DbHelper dbHelper = new DbHelper(new DatabaseConnection());
            TableService tables = new TableService(dbHelper);

            return new AppServices(
                tables,
                new OrderService(dbHelper, tables),
                new MenuService(dbHelper),
                new StaffService(dbHelper),
                new InventoryService(dbHelper),
                new ReportService(dbHelper));
        }
    }
}
