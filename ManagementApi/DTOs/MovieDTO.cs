namespace ManagementApi.DTOs
{
    public class MovieDTO : BaseDTO
    {
        public required string Title { get; set; }
        public required int Year { get; set; }
        public string? Director { get; set; }
    }
}
