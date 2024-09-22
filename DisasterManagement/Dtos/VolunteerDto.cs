namespace DisasterManagement.Dtos
{
    // VolunteerDto.cs
    public class VolunteerDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = "";
        public int Age { get; set; }
        public string PhoneNumber { get; set; } = "";
        public string AssignedTask { get; set; } = "";
    }

    // VolunteerCreateDto.cs
    public class VolunteerCreateDto
    {
        public string FullName { get; set; } = "";
        public int Age { get; set; }
        public string PhoneNumber { get; set; } = "";
        public string AssignedTask { get; set; } = "";
    }
 

}