namespace Lab3.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int? AuthorId { get; set; }
        public Author? Author { get; set; }
        public int? YearId { get; set; }
        public Year? Year { get; set; }
    }
}