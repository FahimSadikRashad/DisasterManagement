namespace DisasterManagement.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string ? Name { get; set; } // Description of the task
        public int VolunteerId { get; set; } // Foreign key for Volunteer
        public int CrisisId { get; set; } // Foreign key for Crisis

        // Navigation Properties
        public Volunteer ? Volunteer { get; set; }
        public Crisis ? Crisis { get; set; }
    }
}
