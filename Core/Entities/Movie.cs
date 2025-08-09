namespace Core.Entities
{
    public class Movie : BaseEntity
    {
        public required string Title { get; set; }
        public required int Year { get; set; }
        public string? Director { get; set; }
    }
}
