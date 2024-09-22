namespace DisasterManagement.Models
{
    public class Volunteer
    {
        public int Id { get; set; }
        public string ?  FullName { get; set; }
        public int Age { get; set; }
        public string ? PhoneNumber { get; set; }
        public string ? AssignedTask { get; set; } // Optional, can relate to Task if necessary

        // Navigation Property
        public ICollection<VolunteerCrisis> VolunteerCrises { get; set; } = new List<VolunteerCrisis>();
    }
}
