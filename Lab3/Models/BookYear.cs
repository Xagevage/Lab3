namespace Lab3.Models
{
    public class BookYear
    {
        public int Id { get; set; }

       
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        public int YearId { get; set; }
        public Year Year { get; set; } = null!;
    }
}
