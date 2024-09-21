namespace DisasterManagement.Dtos
{
    public class CrisisDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Severity { get; set; }
        public bool IsApproved { get; set; }
    }

    public class CrisisCreateDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Severity { get; set; }
    }
}
