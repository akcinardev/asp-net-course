namespace IActionResultExample.Models
{
    public class Book
    {
        public int? BookId { get; set; }
        public string? Author { get; set; }

        public override string ToString()
        {
            return $"Book ID: {BookId}, Author: {Author}";
        }
    }
}
