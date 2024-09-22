namespace DisasterManagement.Models
{
    public class Crisis
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Help { get; set; }
        public string Severity { get; set; }
        public bool IsApproved { get; set; } = false;

        public DateTime Date { get; set; } = DateTime.UtcNow;
    }
}