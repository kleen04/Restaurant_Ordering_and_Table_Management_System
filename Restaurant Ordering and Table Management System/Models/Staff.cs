using System;

namespace Restaurant_Ordering_and_Management_System.Models
{
    public class Staff
    {
        public int StaffId { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string ContactNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateHired { get; set; }
    }
}
