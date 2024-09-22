namespace DisasterManagement.Models
{
    public class Inventory
    {
        public int Id { get; set; }
        public string ? Name { get; set; }
        public string ? Type { get; set; } // Relief or Expense
        public int Quantity { get; set; }
    }
}