namespace DisasterManagement.Dtos
{
    public class InventoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Type { get; set; } = "";
        public int Quantity { get; set; }
    }

    public class InventoryCreateDto
    {
        public string Name { get; set; } = "";
        public string Type { get; set; } = "";
        public int Quantity { get; set; }
    }
}
