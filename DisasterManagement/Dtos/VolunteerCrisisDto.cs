namespace DisasterManagement.Dtos
{
    // VolunteerCrisisDto.cs
    public class VolunteerCrisisDto
    {
        public int CrisisId { get; set; }
        public int VolunteerId { get; set; }
        public string VolunteerName { get; set; } = "";
        public string CrisisTitle { get; set; } = "";
    }

    public class VolunteerCrisisDetailDto
    {
        public int VolunteerId { get; set; }
        public VolunteerDto Volunteer { get; set; }= new VolunteerDto();
        public int CrisisId { get; set; }
        public CrisisDto Crisis { get; set; }= new CrisisDto();
    }

}