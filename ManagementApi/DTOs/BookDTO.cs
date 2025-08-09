namespace ManagementApi.DTOs
{
    public class BookDTO : BaseDTO
    {
        public required string Title { get; set; }
        public required string Author { get; set; }
    }
}
