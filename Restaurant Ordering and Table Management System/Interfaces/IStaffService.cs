using System.Collections.Generic;
using Restaurant_Ordering_and_Management_System.Models;

namespace Restaurant_Ordering_and_Management_System.Interfaces
{
    public interface IStaffService
    {
        List<Staff> GetAllStaff();
        Staff GetStaffById(int staffId);
        void AddStaff(Staff staff);
        void UpdateStaff(Staff staff);
        void DeleteStaff(int staffId);
    }
}
