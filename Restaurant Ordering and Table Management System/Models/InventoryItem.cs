namespace Restaurant_Ordering_and_Management_System.Models
{
    public class InventoryItem
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string Category { get; set; }
        public decimal Quantity { get; set; }
        public string Unit { get; set; }
        public decimal ReorderLevel { get; set; }
        public decimal UnitCost { get; set; }

        /// <summary>True once Quantity drops to or below ReorderLevel — this is what
        /// InventoryForm's "Low Stock" / "In Stock" column should be driven from,
        /// instead of hand-typing the status per row.</summary>
        public bool IsLowStock => Quantity <= ReorderLevel;
    }
}
