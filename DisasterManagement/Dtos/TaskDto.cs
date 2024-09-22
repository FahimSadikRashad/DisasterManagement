namespace DisasterManagement.Dtos
{
    // TaskDto.cs
    public class TaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
    
        public int CrisisId { get; set; }
    }

    public class TaskCreateDto
    {
        public string Name { get; set; } = "";
        
        public int CrisisId { get; set; }
    }

}