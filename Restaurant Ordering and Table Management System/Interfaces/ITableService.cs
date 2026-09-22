using System.Collections.Generic;
using Restaurant_Ordering_and_Management_System.Models;

namespace Restaurant_Ordering_and_Management_System.Interfaces
{
    public interface ITableService
    {
        List<RestaurantTable> GetAllTables();
        void UpdateTableStatus(int tableId, TableStatus status, int currentGuests);
    }
}
