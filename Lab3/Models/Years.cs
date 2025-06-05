namespace Lab3.Models
{
    public class Year
    {
        public int Id { get; set; }
        public int YearValue { get; set; }

        
        public List<Book> Books { get; set; } = new List<Book>();
    }
}
