namespace Lab3.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

       
        public int AuthorId { get; set; }
        public Author Author { get; set; } = null!;

       
        public int YearId { get; set; }
        public Year Year { get; set; } = null!;
    }
}
