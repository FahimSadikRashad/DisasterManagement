namespace DisasterManagement.Models
{
    public class VolunteerCrisis
    {
        public int Id { get; set; }
        public int VolunteerId { get; set; } // Foreign key for Volunteer
        public int CrisisId { get; set; } // Foreign key for Crisis

        // Navigation Properties
        public Volunteer? Volunteer { get; set; }
        public Crisis? Crisis { get; set; }
    }
}
