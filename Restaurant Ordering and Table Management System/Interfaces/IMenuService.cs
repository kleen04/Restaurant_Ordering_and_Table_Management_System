using System.Collections.Generic;
using Restaurant_Ordering_and_Management_System.Models;

namespace Restaurant_Ordering_and_Management_System.Interfaces
{
    public interface IMenuService
    {
        List<MenuItem> GetAllMenuItems();
    }
}