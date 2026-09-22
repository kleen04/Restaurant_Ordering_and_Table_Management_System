using System.Collections.Generic;
using Restaurant_Ordering_and_Management_System.Models;

namespace Restaurant_Ordering_and_Management_System.Interfaces
{
    public interface IInventoryService
    {
        List<InventoryItem> GetAllItems();
        void AddItem(InventoryItem item);
        void UpdateItem(InventoryItem item);
        void DeleteItem(int itemId);
    }
}
