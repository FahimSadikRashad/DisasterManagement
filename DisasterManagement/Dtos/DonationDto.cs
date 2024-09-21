namespace DisasterManagement.Dtos
{
    public class DonationDto
    {
        public int Id { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
    }

    public class DonationCreateDto
    {
        public double Amount { get; set; }
    }
}
