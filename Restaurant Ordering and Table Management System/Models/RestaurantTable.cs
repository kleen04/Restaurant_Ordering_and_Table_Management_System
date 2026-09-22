namespace Restaurant_Ordering_and_Management_System.Models
{
    public enum TableStatus
    {
        Available,
        Occupied,
        Reserved
    }

    
    public class RestaurantTable
    {
        public int TableId { get; set; }
        public int Capacity { get; set; }
        public TableStatus Status { get; set; }
        public int CurrentGuests { get; set; }
    }
}
